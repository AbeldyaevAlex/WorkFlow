using Asu.Core.Domain.TypicalTechnologicalOperations;


namespace Asu.Data.Mapping.TypicalTechnologicalOperations
{
    public partial class SprTtoMap : NopEntityTypeConfiguration<Spr_tto>
    {
        public SprTtoMap()
        {
            this.ToTable("Spr_tto");
            this.HasKey(a => a.Id);
            this.Property(nrm => nrm.Nrm).HasPrecision(38, 7);
            this.Property(nrvp => nrvp.Nrvp).HasPrecision(38, 7);

            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.Spr_pviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_prpokr).WithMany().HasForeignKey(x => x.PrpokrId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_cex).WithMany().HasForeignKey(x => x.CizgId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprPrKm).WithMany().HasForeignKey(x => x.PrkmId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprSkm).WithMany().HasForeignKey(x => x.KodTTOId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.SprSkm).WithMany().HasForeignKey(x => x.KodKompId).WillCascadeOnDelete(false);
        }
    }
}
