namespace Asu.Services.Customization
{
    using Asu.Core.Domain.FreshdeskTickets;
    using Asu.Core.Domain.Orders;

    public interface IFreshdeskTicketService
    {
        CreateTicketResponse CreateTicket(CreateTicket ticket, Channel channel);

        long GetReturnsDepartmentGroupId(Channel channelId);

        long GetSupportEmailId(Channel channel);

        string GetSupportEmail(Channel channel);

        string GetApiKey(Channel channel);

        string GetCreateTicketUrl(Channel channel);
    }
}
