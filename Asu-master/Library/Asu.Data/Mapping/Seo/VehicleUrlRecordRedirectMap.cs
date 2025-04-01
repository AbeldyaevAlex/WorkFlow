namespace Asu.Data.Mapping.Seo
{
    using Asu.Core.Domain.Seo;

    public class VehicleUrlRecordRedirectMap : NopEntityTypeConfiguration<VehicleUrlRecordRedirect>
    {
        public VehicleUrlRecordRedirectMap()
        {
            this.ToTable("WCS_VehicleUrlRecordRedirect");
            this.HasKey(m => m.Id);
        }
    }
}
