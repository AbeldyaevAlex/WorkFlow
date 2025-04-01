using Asu.Core.Domain.Msi;

namespace Asu.Data.Mapping.Msi
{
    public partial class SprPrimDseMap : NopEntityTypeConfiguration<Spr_prim_dse>
    {
        public SprPrimDseMap()
        {
            this.ToTable("Spr_prim_dse");
            this.HasKey(l => l.Id);

            this.Property(mas => mas.Masizd).HasPrecision(18, 6);

            this.HasRequired(a => a.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.DocumentStatus)
                .WithMany()
                .HasForeignKey(x => x.DocumentStatusId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.Spr_pvi)
               .WithMany()
               .HasForeignKey(x => x.PviId)
               .WillCascadeOnDelete(false);
        }
    }
}
