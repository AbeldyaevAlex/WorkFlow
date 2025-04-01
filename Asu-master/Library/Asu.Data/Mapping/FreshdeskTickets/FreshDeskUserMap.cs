namespace Asu.Data.Mapping.FreshdeskTickets
{
    using Asu.Core.Domain.FreshdeskTickets;

    public partial class FreshDeskUserMap : NopEntityTypeConfiguration<FreshDeskUser>
    {
        public FreshDeskUserMap()
        {
            this.ToTable("WCS_FreshDeskUsers");
            this.Ignore(i => i.Id);
            this.HasKey(i => i.FreshDeskUserId);
        }
    }
}
