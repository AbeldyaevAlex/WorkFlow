using Asu.Core.Domain.Msi;

namespace Asu.Data.Mapping.Msi
{
    public partial class SprMashSgMap : NopEntityTypeConfiguration<Spr_mash_sg>
    {
        public SprMashSgMap()
        {
            this.ToTable("Spr_mash_sg");
            this.HasKey(l => l.Id);


            this.HasRequired(a => a.Spr_pvi)
                .WithMany()
                .HasForeignKey(x => x.PviId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.DocumentStatus)
                .WithMany()
                .HasForeignKey(x => x.DocumentStatusId)
                .WillCascadeOnDelete(false);
        }
    }
}
