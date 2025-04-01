namespace Asu.Data.Mapping.Returns
{
    using Asu.Core.Domain.Returns;

    public class ReturnRequestMap : NopEntityTypeConfiguration<ReturnRequest>
    {
        public ReturnRequestMap()
        {
            this.ToTable("WCS_ReturnRequest");
            this.HasKey(r => r.Id);
            this.HasMany(i => i.Items).WithRequired(i => i.ReturnRequest).HasForeignKey(i => i.ReturnId);
            this.HasOptional(m => m.Import).WithRequired(m => m.ReturnRequest);
        }
    }
}