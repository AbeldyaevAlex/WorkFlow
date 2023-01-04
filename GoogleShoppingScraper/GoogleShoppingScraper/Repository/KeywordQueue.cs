namespace GoogleShoppingScraper.Repository
{
    using System.Data.Linq.Mapping;

    [Table(Name = "KeywordQueue")]
    public class KeywordQueue
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "ProductId")]
        public int ProductId { get; set; }

        [Column(Name = "Keywords")]
        public string Keywords { get; set; }

        [Column(Name = "SearchRank")]
        public long SearchRank { get; set; }
    }
}
