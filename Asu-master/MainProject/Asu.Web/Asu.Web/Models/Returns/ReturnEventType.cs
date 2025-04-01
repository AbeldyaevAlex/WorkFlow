namespace Asu.Web.Models.Returns
{
    public enum ReturnEventType
    {
        Pending = 0,
        Processing = 1,
        Cancel = 2,
        Rma = 3,
        Refusal = 4,
        Shipment = 5,
        CancelCredit = 6,
        NewRequest = 7
    }
}