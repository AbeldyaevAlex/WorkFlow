namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class CreditTypeMap : NopEntityTypeConfiguration<CreditType>
    {
        public CreditTypeMap()
        {
            this.ToTable("vw_crm_CreditTypes");
            this.HasKey(m => m.Id);
        }
    }
}
