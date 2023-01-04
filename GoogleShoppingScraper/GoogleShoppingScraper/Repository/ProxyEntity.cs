namespace GoogleShoppingScraper.Repository
{
    using System;
    using System.Data.Linq.Mapping;

    [Table(Name = "Proxies")]
    public class ProxyEntity 
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "Ip")]
        public string Ip { get; set; }

        [Column(Name = "Port")]
        public int Port { get; set; }

        [Column(Name = "UserName")]
        public string UserName { get; set; }

        [Column(Name = "Password")]
        public string Password { get; set; }

        [Column(Name = "Used")]
        public bool Used { get; set; }

        [Column(Name = "Banned")]
        public bool Banned { get; set; }

        [Column(Name = "WrongCurrency")]
        public int WrongCurrency { get; set; }

        [Column(Name = "ServerError")]
        public int ServerError { get; set; }

        [Column(Name = "ProductNotFound")]
        public int ProductNotFound { get; set; }

        [Column(Name = "LastUsed")]
        public DateTime? LastUsed { get; set; }

        [Column(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [Column(Name = "SpeedSumMilliseconds")]
        public long? SpeedSumMilliseconds { get; set; }

        [Column(Name = "AVGLoadSpeed", DbType = "float", Expression = "SpeedSumMilliseconds / Used")]
        public double? AvgLoadSpeed { get; set; }

        [Column(Name = "IsActive")]
        public bool IsActive { get; set; }
    }
}
