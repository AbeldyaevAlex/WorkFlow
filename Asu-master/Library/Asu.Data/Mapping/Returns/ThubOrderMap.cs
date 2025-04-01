namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ThubOrderMap : NopEntityTypeConfiguration<ThubOrder>
    {
        public ThubOrderMap()
        {
            this.ToTable("vw_thub_Orders");
            this.Ignore(i => i.Id);
            this.HasKey(i => i.OrderId);
        }
    }
}