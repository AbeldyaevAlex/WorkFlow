namespace GoogleShoppingScraper.Scraping
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using GoogleShoppingScraper.Repository.UserDefinedTypes;

    using Proxy;

    public static class Extensions
    {
        private static readonly Random Rnd = new Random();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(Rnd);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rnd)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (rnd == null)
            {
                throw new ArgumentNullException(nameof(rnd));
            }

            return source.ShuffleIterator(rnd);
        }

        public static DataTable ToDataTable(this IEnumerable<int> productIds)
        {
            var table = new DataTable("ProductIds");
            table.Columns.Add("Id", typeof(int));
            foreach (var productId in productIds)
            {
                var row = table.NewRow();
                row["Id"] = productId;
                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ToDataTable(this IEnumerable<KeywordRankReducingInfo> productSearchTerm)
        {
            var table = new DataTable("ProductSearchTermPairs");
            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("SearchTerm", typeof(string));
            table.Columns.Add("NoMatchingResults", typeof(bool));
            foreach (var item in productSearchTerm)
            {
                var row = table.NewRow();
                row["ProductId"] = item.ProductId;
                row["SearchTerm"] = item.SearchTerm;
                row["NoMatchingResults"] = item.NoMatchingResults;
                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ToDataTable(this IEnumerable<Repository.UserDefinedTypes.SearchResult> results)
        {
            var table = new DataTable("Results");
            table.Columns.Add("SearchTerm", typeof(string));
            table.Columns.Add("SellerName", typeof(string));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("SellerUrl", typeof(string));
            table.Columns.Add("ImageUrl", typeof(string));
            table.Columns.Add("GooglePartNumber", typeof(string));
            table.Columns.Add("GoogleGtin", typeof(string));
            table.Columns.Add("GoogleBrand", typeof(string));
            table.Columns.Add("GroupId", typeof(string));
            table.Columns.Add("ProductId", typeof(int));
            table.Columns.Add("Price", typeof(decimal));
            //table.Columns.Add("TaxShippingPrice", typeof(decimal));
            table.Columns.Add("TaxPrice", typeof(decimal));
            table.Columns.Add("ShippingPrice", typeof(decimal));
            table.Columns.Add("TotalPrice", typeof(decimal));
            table.Columns.Add("GroupPrice", typeof(decimal));
            table.Columns.Add("IsInGroup", typeof(bool));
            table.Columns.Add("OnGroupPageRank", typeof(short));
            table.Columns.Add("IsFreeShipping", typeof(bool));
            table.Columns.Add("IsTaxFree", typeof(bool));
            table.Columns.Add("IsUsed", typeof(bool));
            table.Columns.Add("IsRefurbished", typeof(bool));
            table.Columns.Add("AvgRating", typeof(decimal));
            table.Columns.Add("Ratings", typeof(int));
            table.Columns.Add("Details", typeof(string));
            table.Columns.Add("OnSearchPageRank", typeof(int));
            table.Columns.Add("IsRefilled", typeof(bool));
            table.Columns.Add("MatchingGroup", typeof(bool));

            foreach (var result in results)
            {
                var row = table.NewRow();
                row["SearchTerm"] = result.SearchTerm;
                row["SellerName"] = result.SellerName;
                row["Title"] = result.Title;
                row["SellerUrl"] = result.SellerUrl;
                row["ImageUrl"] = result.ImageUrl;
                row["GooglePartNumber"] = result.GooglePartNumber;
                row["GoogleGtin"] = result.GoogleGtin;
                row["GoogleBrand"] = result.GoogleBrand;
                row["GroupId"] = result.GroupId;
                row["ProductId"] = result.ProductId;
                row["Price"] = result.Price;
                //row["TaxShippingPrice"] = result.TaxShippingPrice ?? (object)DBNull.Value;
                row["TaxPrice"] = result.TaxPrice ?? (object)DBNull.Value;
                row["ShippingPrice"] = result.ShippingPrice ?? (object)DBNull.Value;
                row["TotalPrice"] = result.TotalPrice;
                row["GroupPrice"] = result.GroupPrice ?? (object)DBNull.Value;
                row["IsInGroup"] = result.IsInGroup;
                row["OnGroupPageRank"] = result.OnGroupPageRank ?? (object)DBNull.Value;
                row["IsFreeShipping"] = result.IsFreeShipping ?? (object)DBNull.Value;
                row["IsTaxFree"] = result.IsTaxFree ?? (object)DBNull.Value;
                row["IsUsed"] = result.IsUsed ?? (object)DBNull.Value;
                row["IsRefurbished"] = result.IsRefurbished ?? (object)DBNull.Value;
                row["AvgRating"] = result.AvgRating ?? (object)DBNull.Value;
                row["Ratings"] = result.Ratings ?? (object)DBNull.Value;
                row["Details"] = string.IsNullOrWhiteSpace(result.Details) ? (object)DBNull.Value : result.Details;
                row["OnSearchPageRank"] = result.OnSearchPageRank;
                row["IsRefilled"] = result.IsRefilled ?? (object)DBNull.Value;
                row["MatchingGroup"] = result.MatchingGroup ?? (object)DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }

        public static IList<Repository.UserDefinedTypes.SearchResult> ToTableType(this IEnumerable<Keyword> keywords)
        {
            var searchResults = new List<Repository.UserDefinedTypes.SearchResult>();
            foreach (var keyword in keywords)
            {
                foreach (var result in keyword.Results.Where(i => i.Sellers.Any()))
                {
                    foreach (var seller in result.Sellers)
                    {
                        var searchResult = new Repository.UserDefinedTypes.SearchResult
                        {
                            IsInGroup = result.IsGroup,
                            SearchTerm = keyword.SearchTerm?.CutString(250),
                            ImageUrl = result.ImageUrl?.CutString(900),
                            OnSearchPageRank = result.SearchPageRank,
                            GooglePartNumber = result.GooglePartNumber?.CutString(250),
                            GoogleGtin = result.GoogleGtin?.CutString(250),
                            GoogleBrand = result.GoogleBrand?.CutString(250),
                            Title = result.Title?.CutString(450),
                            ProductId = keyword.ProductId,
                            GroupPrice = result.GroupPrice,
                            MatchingGroup = result.MatchingGroup,
                            GroupId = result.GroupId
                        };
                    
                        seller.FillSellerData(searchResult);
                        searchResults.Add(searchResult);
                    }
                }
            }

            return searchResults;
        }

        public static DataTable ToDataTable(this IEnumerable<Repository.UserDefinedTypes.Proxy> results)
        {
            var table = new DataTable("Proxies");
            table.Columns.Add("Ip", typeof(string));
            table.Columns.Add("Used", typeof(int));
            table.Columns.Add("Banned", typeof(int));
            table.Columns.Add("WrongCurrency", typeof(int));
            table.Columns.Add("NoMatchingResults", typeof(int));
            table.Columns.Add("ServerError", typeof(int));
            table.Columns.Add("ProductNotFound", typeof(int));
            table.Columns.Add("SpeedSumMilliseconds", typeof(long));
            table.Columns.Add("LastUsed", typeof(DateTime));

            foreach (var result in results)
            {
                var row = table.NewRow();
                row["Ip"] = result.Ip;
                row["Used"] = result.Used;
                row["Banned"] = result.Banned;
                row["WrongCurrency"] = result.WrongCurrency;
                row["NoMatchingResults"] = result.NoMatchingResults;
                row["ServerError"] = result.ServerError;
                row["ProductNotFound"] = result.ProductNotFound;
                row["SpeedSumMilliseconds"] = result.SpeedSumMilliseconds;
                row["LastUsed"] = result.LastUsed ?? Convert.DBNull;
                table.Rows.Add(row);
            }

            return table;
        }

        public static IEnumerable<Repository.UserDefinedTypes.Proxy> ToTableType(this IEnumerable<ProxyHealth> proxies)
        {
            return proxies.Select(i => new Repository.UserDefinedTypes.Proxy
            {
                Ip = i.Ip,
                Used = i.Used,
                Banned = i.Banned,
                WrongCurrency = i.WrongCurrency,
                NoMatchingResults = i.NoMatchingResults,
                SpeedSumMilliseconds = i.SpeedSumMilliseconds,
                LastUsed = i.LastUsed,
                ServerError = i.ServerError,
                ProductNotFound = i.ProductNotFound
            });
        }

        public static string ToReadableString(this TimeSpan span)
        {
            return $"{(span.Duration().Days > 0 ? $"{span.Days:0} day{(span.Days == 1 ? string.Empty : "s")}, " : string.Empty)}{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}";
        }

        private static void FillSellerData(this Seller seller, Repository.UserDefinedTypes.SearchResult result)
        {
            result.AvgRating = seller.AverageRating;
            result.SellerName = seller.SellerName?.CutString(250);
            result.IsFreeShipping = seller.IsFreeShipping;
            result.IsRefilled = seller.IsRefilled;
            result.IsUsed = seller.IsUsed;
            result.IsTaxFree = seller.IsTaxFree;
            result.IsRefurbished = seller.IsRefurbished;
            result.Ratings = seller.Ratings;
            result.TotalPrice = seller.TotalPrice;
            result.OnGroupPageRank = seller.OnGroupPageRank;
            //result.TaxShippingPrice = seller.TaxShippingPrice;
            result.TaxPrice = seller.TaxPrice;
            result.ShippingPrice = seller.ShippingPrice;
            result.Details = seller.Details?.CutString(250);
            result.Price = seller.Price;
            result.SellerUrl = seller.Url?.CutString(900);
            result.IsInGroup = result.IsInGroup;
        }

        private static string CutString(this string value, int length)
        {
            return value.Length > length ? value.Substring(0, length) : value;
        }

        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rnd)
        {
            var buffer = source.ToList();
            for (var i = 0; i < buffer.Count; i++)
            {
                var j = rnd.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}
