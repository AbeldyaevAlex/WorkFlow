namespace Asu.Core.Domain.Messages
{
    public enum BackorderNotificationType
    {
        OOSWithETA = 1,
        UpdatedETA = 2,
        UpdatedETANoDate = 3,
        UnexpectedBackorderWithETA = 4
    }
}
