using Asu.Core.Domain.Msi;

namespace Asu.Data.Mapping.Msi
{
    public partial class SprRazdIzdMap : NopEntityTypeConfiguration<Spr_Razd_Izd>
    {
        public SprRazdIzdMap()
        {
            this.ToTable("Spr_Razd_Izd");
            this.HasKey(l => l.Id);

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
