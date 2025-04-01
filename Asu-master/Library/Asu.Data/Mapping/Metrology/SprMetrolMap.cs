using Asu.Core.Domain.Metrology;

namespace Asu.Data.Mapping.Metrology
{
    public partial class SprMetrolMap : NopEntityTypeConfiguration<Spr_metrol>
    {
        public SprMetrolMap()
        {
            this.ToTable("Spr_metrol");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Rod_poverk).WithMany().HasForeignKey(x => x.link_rod_poverk).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Podgr_prib).WithMany().HasForeignKey(x => x.link_podgrupp).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Nazn_prib).WithMany().HasForeignKey(x => x.link_naznach).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_cex).WithMany().HasForeignKey(x => x.link_cex).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Period_pover).WithMany().HasForeignKey(x => x.link_period_poverk).WillCascadeOnDelete(false);
            this.HasRequired(a => a.MestoPoverk).WithMany().HasForeignKey(x => x.MestoPoverkId).WillCascadeOnDelete(false);
        }
    }
}
