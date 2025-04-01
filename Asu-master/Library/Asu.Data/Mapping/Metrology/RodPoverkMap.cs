using Asu.Core.Domain.Metrology;


namespace Asu.Data.Mapping.Metrology
{
    public partial class RodPoverkMap : NopEntityTypeConfiguration<Rod_poverk>
    {
        public RodPoverkMap()
        {
            this.ToTable("Rod_poverk");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.DocumentStatus)
                .WithMany()
                .HasForeignKey(x => x.DocumentStatusId)
                .WillCascadeOnDelete(false);
        }
    }
}
