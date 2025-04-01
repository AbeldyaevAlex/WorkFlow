namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ChargeTypeMap : NopEntityTypeConfiguration<ChargeType>
    {
        public ChargeTypeMap()
        {
            this.ToTable("vw_crm_ChargeTypes");
            this.HasKey(m => m.Id);
        }
    }
}
