using Asu.Core.Domain.Msi;

namespace Asu.Data.Mapping.Msi
{
    public partial class RazDetMap : NopEntityTypeConfiguration<Raz_det>
    {
        public RazDetMap()
        {
            this.ToTable("Raz_det");
            this.HasKey(l => l.Id);

            this.HasRequired(a => a.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.PviId).WillCascadeOnDelete(false);
        }
    }
}
