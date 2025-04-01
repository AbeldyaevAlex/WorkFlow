using Asu.Core.Domain.Work;

namespace Asu.Data.Mapping.Work
{
    public partial class ExceptionForWorkMap : NopEntityTypeConfiguration<ExceptionForWork>
    {
        public ExceptionForWorkMap()
        {
            this.ToTable("ExceptionForWork");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.SprPviId).WillCascadeOnDelete(false);
        }
    }
}
