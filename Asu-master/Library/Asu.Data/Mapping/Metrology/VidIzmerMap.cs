using Asu.Core.Domain.Metrology;

namespace Asu.Data.Mapping.Metrology
{
    public partial class VidIzmerMap : NopEntityTypeConfiguration<Vid_izmer>
    {
        public VidIzmerMap()
        {
            this.ToTable("Vid_izmer");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.DocumentStatus)
                .WithMany()
                .HasForeignKey(x => x.DocumentStatusId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.Nm_vidiz)
                .WithMany()
                .HasForeignKey(x => x.link_nmvidiz)
                .WillCascadeOnDelete(false);
        }
    }
}
