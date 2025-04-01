namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class CrmReturnImportMap : NopEntityTypeConfiguration<CrmReturnImport>
    {
        public CrmReturnImportMap()
        {
            this.ToTable("vw_crm_ReturnImports");
            this.Ignore(i => i.Id);
            this.HasKey(m => m.ReturnRequestId);

            this.HasRequired(m => m.Return).WithMany().HasForeignKey(m => m.ReturnId);
        }
    }
}