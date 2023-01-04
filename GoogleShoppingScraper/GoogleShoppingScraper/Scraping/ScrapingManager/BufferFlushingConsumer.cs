namespace GoogleShoppingScraper.Scraping.ScrapingManager
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    using Errors;

    using GoogleShoppingScraper.Repository.UserDefinedTypes;

    using Proxy;

    using Repository;

    public class BufferFlushingConsumer
    {
        private readonly Synchronizer synchronizer;
        private readonly WaitHandle[] eventArray;
        private readonly Logger logger;
        private readonly Settings settings;

        public BufferFlushingConsumer(Synchronizer synchronizer, Logger logger, Settings settings)
        {
            this.synchronizer = synchronizer;
            this.logger = logger;
            this.settings = settings;
            this.eventArray = new WaitHandle[2];
            this.eventArray[0] = this.synchronizer.ExitEvent;
            this.eventArray[1] = this.synchronizer.TaskCompletedEvent;
            this.TotalSaved = 0;
            this.TotalFailed = 0;
        }

        public int TotalSaved { get; private set; }

        public int TotalFailed { get; private set; }

        public void Run()
        {
            while (WaitHandle.WaitAny(this.eventArray) != 0)
            {
                if (this.synchronizer.BufferLoading || this.synchronizer.BufferFlushing)
                {
                    Thread.Sleep(500);
                    continue;
                }

                this.synchronizer.BufferFlushing = true;

                if (this.synchronizer.CompletedBuffer.Count >= this.settings.KeywordsFlushingBufferSize)
                {
                    this.SaveKeywords();
                }

                this.synchronizer.BufferFlushing = false;
            }

            this.synchronizer.BufferFlushing = true;
            this.SaveKeywords();
            this.synchronizer.BufferFlushing = false;
        }

        private void SaveKeywords()
        {
            var keywords = new List<Keyword>();
            while (!this.synchronizer.CompletedBuffer.IsEmpty)
            {
                Keyword keyword;
                while (!this.synchronizer.CompletedBuffer.TryDequeue(out keyword))
                {
                    Thread.Sleep(10);
                }

                keywords.Add(keyword);
            }

            while (!this.SaveResults(keywords.Where(i => i.Completed && !i.IsFailed).ToTableType()))
            {
                Thread.Sleep(600000);
            }

            this.TotalSaved += keywords.Count(i => i.Completed && !i.IsFailed);
            this.TotalFailed += keywords.Count(i => i.Completed && i.IsFailed);

            this.synchronizer.LastSavingFailedPercent = (int)((decimal)keywords.Count(i => i.Completed && i.IsFailed) * 100 / keywords.Count);
            this.ChangeFailedKeywordsRank(keywords.Where(i => i.Completed && i.IsFailed).Select(i => new KeywordRankReducingInfo { ProductId = i.ProductId, SearchTerm = i.SearchTerm, NoMatchingResults = i.NoMatchingResults }).ToDataTable());
            ProxyHealthCollector.FlushHealthStatistic();
        }

        private bool SaveResults(IEnumerable<SearchResult> results)
        {
            using (var context = new DatabaseContext(this.settings.ConnectionString))
            {
                if (!results.Any())
                {
                    this.logger.SendMail("Error when save results. No items to save.");
                    return true;
                }

                string errorMessage;
                //var dataResults = results.ToDataTable();
                //this.BuildInsertSrcipt(dataResults);
                if (context.SaveSearchResults(results.ToDataTable(), out errorMessage, this.settings.SqlRetryAttempts))
                {
                    return true;
                }

                this.logger.SendMail($"Error when save results. Message: {errorMessage}");
            }

            return false;
        }

        private void BuildInsertSrcipt(DataTable results)
        {
            var stringBuilder = new StringBuilder("INSERT #TestResults(");
            stringBuilder.AppendLine("SearchTerm, SellerName, Title, SellerUrl, ImageUrl, GooglePartNumber, GoogleGtin, GoogleBrand, GroupId, ProductId, Price, TaxPrice, ShippingPrice, /*TaxShippingPrice*/, TotalPrice, GroupPrice, IsInGroup, OnGroupPageRank, IsFreeShipping, IsTaxFree, IsUsed, IsRefurbished, AvgRating, Ratings, Details, OnSearchPageRank, IsRefilled, MatchingGroup)");
            foreach (DataRow row in results.Rows)
            {
                if (results.Rows.IndexOf(row) == 0)
                {
                    stringBuilder.Append($"VALUES ('{(row["SearchTerm"] == (object)DBNull.Value ? "NULL" : row["SearchTerm"])}', '{(row["SellerName"] == (object)DBNull.Value ? "NULL" : row["SellerName"])}', '{(row["Title"] == (object)DBNull.Value ? "NULL" : row["Title"])}', '{(row["SellerUrl"] == (object)DBNull.Value ? "NULL" : row["SellerUrl"])}', '{(row["ImageUrl"] == (object)DBNull.Value ? "NULL" : row["ImageUrl"])}', '{(row["GooglePartNumber"] == (object)DBNull.Value ? "NULL" : row["GooglePartNumber"])}', '{(row["GoogleGtin"] == (object)DBNull.Value ? "NULL" : row["GoogleGtin"])}', '{(row["GoogleBrand"] == (object)DBNull.Value ? "NULL" : row["GoogleBrand"])}', ");
                    stringBuilder.Append($"'{(row["GroupId"] == (object)DBNull.Value ? "NULL" : row["GroupId"])}', {(row["ProductId"] == (object)DBNull.Value ? "NULL" : row["ProductId"])}, {(row["Price"] == (object)DBNull.Value ? "NULL" : row["Price"])}, {(row["ShippingPrice"] == (object)DBNull.Value ? "NULL" : row["ShippingPrice"])}, {(row["TaxPrice"] == (object)DBNull.Value ? "NULL" : row["TaxPrice"])}, {(row["TotalPrice"] == (object)DBNull.Value ? "NULL" : row["TotalPrice"])}, {(row["GroupPrice"] == (object)DBNull.Value ? "NULL" : row["GroupPrice"])}, {(row["IsInGroup"] == (object)DBNull.Value ? "NULL" : row["IsInGroup"])}, {(row["OnGroupPageRank"] == (object)DBNull.Value ? "NULL" : row["OnGroupPageRank"])}, ");
                    stringBuilder.Append($"{(row["IsFreeShipping"] == (object)DBNull.Value ? "NULL" : row["IsFreeShipping"])}, {(row["IsTaxFree"] == (object)DBNull.Value ? "NULL" : row["IsTaxFree"])}, {(row["IsUsed"] == (object)DBNull.Value ? "NULL" : row["IsUsed"])}, {(row["IsRefurbished"] == (object)DBNull.Value ? "NULL" : row["IsRefurbished"])}, {(row["AvgRating"] == (object)DBNull.Value ? "NULL" : row["AvgRating"])}, {(row["Ratings"] == (object)DBNull.Value ? "NULL" : row["Ratings"])}, '{(row["Details"] == (object)DBNull.Value ? "NULL" : row["Details"])}', {(row["OnSearchPageRank"] == (object)DBNull.Value ? "NULL" : row["OnSearchPageRank"])}, ");
                    stringBuilder.AppendLine($"{(row["IsRefilled"] == (object)DBNull.Value ? "NULL" : row["IsRefilled"])}, {(row["MatchingGroup"] == (object)DBNull.Value ? "NULL" : row["MatchingGroup"])}),");

                    continue;
                }

                stringBuilder.Append($"('{(row["SearchTerm"] == (object)DBNull.Value ? "NULL" : row["SearchTerm"])}', '{(row["SellerName"] == (object)DBNull.Value ? "NULL" : row["SellerName"])}', '{(row["Title"] == (object)DBNull.Value ? "NULL" : row["Title"])}', '{(row["SellerUrl"] == (object)DBNull.Value ? "NULL" : row["SellerUrl"])}', '{(row["ImageUrl"] == (object)DBNull.Value ? "NULL" : row["ImageUrl"])}', '{(row["GooglePartNumber"] == (object)DBNull.Value ? "NULL" : row["GooglePartNumber"])}', '{(row["GoogleGtin"] == (object)DBNull.Value ? "NULL" : row["GoogleGtin"])}', '{(row["GoogleBrand"] == (object)DBNull.Value ? "NULL" : row["GoogleBrand"])}', ");
                stringBuilder.Append($"'{(row["GroupId"] == (object)DBNull.Value ? "NULL" : row["GroupId"])}', {(row["ProductId"] == (object)DBNull.Value ? "NULL" : row["ProductId"])}, {(row["Price"] == (object)DBNull.Value ? "NULL" : row["Price"])}, {(row["ShippingPrice"] == (object)DBNull.Value ? "NULL" : row["ShippingPrice"])}, {(row["TaxPrice"] == (object)DBNull.Value ? "NULL" : row["TaxPrice"])}, {(row["TotalPrice"] == (object)DBNull.Value ? "NULL" : row["TotalPrice"])}, {(row["GroupPrice"] == (object)DBNull.Value ? "NULL" : row["GroupPrice"])}, {(row["IsInGroup"] == (object)DBNull.Value ? "NULL" : row["IsInGroup"])}, {(row["OnGroupPageRank"] == (object)DBNull.Value ? "NULL" : row["OnGroupPageRank"])}, ");
                stringBuilder.Append($"{(row["IsFreeShipping"] == (object)DBNull.Value ? "NULL" : row["IsFreeShipping"])}, {(row["IsTaxFree"] == (object)DBNull.Value ? "NULL" : row["IsTaxFree"])}, {(row["IsUsed"] == (object)DBNull.Value ? "NULL" : row["IsUsed"])}, {(row["IsRefurbished"] == (object)DBNull.Value ? "NULL" : row["IsRefurbished"])}, {(row["AvgRating"] == (object)DBNull.Value ? "NULL" : row["AvgRating"])}, {(row["Ratings"] == (object)DBNull.Value ? "NULL" : row["Ratings"])}, '{(row["Details"] == (object)DBNull.Value ? "NULL" : row["Details"])}', {(row["OnSearchPageRank"] == (object)DBNull.Value ? "NULL" : row["OnSearchPageRank"])}, ");
                stringBuilder.AppendLine($"{(row["IsRefilled"] == (object)DBNull.Value ? "NULL" : row["IsRefilled"])}, {(row["MatchingGroup"] == (object)DBNull.Value ? "NULL" : row["MatchingGroup"])}),");
            }

            File.WriteAllText("TestResultsInsertScripts.txt", stringBuilder.ToString());
        }

        private void ChangeFailedKeywordsRank(DataTable keywordRankReducingInfo)
        {
            if (keywordRankReducingInfo.Rows.Count == 0)
            {
                return;
            }

            using (var context = new DatabaseContext(this.settings.ConnectionString))
            {
                string errorMessage;
                if (context.ReduceSearchRank(keywordRankReducingInfo, out errorMessage, this.settings.SqlRetryAttempts))
                {
                    return;
                }

                this.logger.SendMail($"Error when change keywords search rank. Message: {errorMessage}");
            }
        }
    }
}
