namespace GoogleShoppingScraper.Repository
{
    using System;
    using System.Data.Linq.Mapping;

    [Table(Name = "Log")]
    public class Log
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id { get; set; }

        [Column(Name = "ProductId")]
        public int ProductId { get; set; }

        [Column(Name = "SearchTerm")]
        public string SearchTerm { get; set; }

        [Column(Name = "Message")]
        public string Message { get; set; }

        [Column(Name = "ErrorType")]
        public int ErrorType { get; set; }

        [Column(Name = "CreatedOn", DbType = "datetime", IsDbGenerated = true)]
        public DateTime CreatedOn { get; set; }
    }
}
