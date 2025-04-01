using Asu.Core.Domain.Metrology;

namespace Asu.Data.Mapping.Metrology
{
    public partial class NmPribMap : NopEntityTypeConfiguration<Nm_prib>
    {
        public NmPribMap()
        {
            this.ToTable("Nm_prib");
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
