namespace Asu.Web.Models.Returns
{
    using Microsoft.SqlServer.Server;

    public class ReturnRequestHelpModel 
    {
        public string FullName { get; set; }

        public string Phone { get; set; }

        public string OrderNumber { get; set; }

        public string Marketplace { get; set; }

        public long ChannelId { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }
    }
}