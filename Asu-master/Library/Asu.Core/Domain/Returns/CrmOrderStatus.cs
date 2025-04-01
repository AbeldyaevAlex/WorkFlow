namespace Asu.Core.Domain.Returns
{
    public enum CrmOrderStatus
    {
        New = 1,
        Scheduled = 2,
        NotCompleted = 3,
        Completed = 4,
        Shipped = 5,
        Archived = 6,
        CancelPending = 15,
        Cancelled = 16,
        SwapPending = 39,
        PartialCancel = 47,
        LockedForMaintenance = 56,
        SwapCancel = 59,
        Postponed = 74
    }
}
