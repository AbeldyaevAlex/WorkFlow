namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ReturnMap : NopEntityTypeConfiguration<Return>
    {
        public ReturnMap()
        {
            this.ToTable("vw_crm_Returns");
            this.HasKey(i => i.Id);
        }
    }
}