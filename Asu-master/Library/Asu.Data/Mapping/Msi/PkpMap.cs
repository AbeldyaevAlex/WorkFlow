using Asu.Core.Domain.Msi;

namespace Asu.Data.Mapping.Msi
{
    public partial class PkpMap : NopEntityTypeConfiguration<Spr_pkp>
    {
        public PkpMap()
        {
            this.ToTable("Spr_pkp");
            this.HasKey(l => l.Id);
            this.Property(pkp => pkp.Pkp).IsRequired();
            this.Property(nm_pkp => nm_pkp.NmPkp).IsRequired();
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
