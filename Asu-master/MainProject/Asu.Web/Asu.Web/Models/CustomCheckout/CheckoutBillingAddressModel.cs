namespace Asu.Web.Models.Checkout
{
    public partial class CheckoutBillingAddressModel
    {
        private bool billToThisAddress = true;

        public bool CurrentCustomerIsGuest { get; set; }
        public bool BillToThisAddress 
        { 
            get
            {
                return billToThisAddress;
            }
            set
            {
                this.billToThisAddress = value;
            } 
        }
    }
}