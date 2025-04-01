using Asu.Core.Domain.Metrology;

namespace Asu.Data.Mapping.Metrology
{
    public partial class PodgrPribMap : NopEntityTypeConfiguration<Podgr_prib>
    {
        public PodgrPribMap()
        {
            this.ToTable("Podgr_prib");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.DocumentStatus)
                .WithMany()
                .HasForeignKey(x => x.DocumentStatusId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.Nm_prib)
                .WithMany()
                .HasForeignKey(x => x.link_nmprib)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.Vid_izmer)
                .WithMany()
                .HasForeignKey(x => x.link_vidiz)
                .WillCascadeOnDelete(false);
        }
    }
}
