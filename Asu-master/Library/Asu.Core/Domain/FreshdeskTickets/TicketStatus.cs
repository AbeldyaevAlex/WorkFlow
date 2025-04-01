namespace Asu.Core.Domain.FreshdeskTickets
{
    public enum TicketStatus
    {
        Open = 2,
        Pending = 3,
        Resolved = 4,
        Closed = 5,
        WaitingOnCustomer = 6,
        WaitingOnThirdParty = 7,
        WaitingOnAccounting = 8,
        WaitingOnRmaDept = 9,
        WaitingOnManager = 10,
        ItTaskInProgress = 11,
        FollowUpRequired24Hrs = 12
    }
}
