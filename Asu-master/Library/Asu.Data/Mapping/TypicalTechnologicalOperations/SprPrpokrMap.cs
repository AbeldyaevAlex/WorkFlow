using Asu.Core.Domain.TypicalTechnologicalOperations;

namespace Asu.Data.Mapping.TypicalTechnologicalOperations
{
    public partial class SprPrpokrMap : NopEntityTypeConfiguration<Spr_prpokr>
    {
        public SprPrpokrMap()
        {
            this.ToTable("Spr_prpokr");
            this.HasKey(a => a.Id);

            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.Spr_pviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
        }
    }
}
