using Asu.Core.Domain.Work;

namespace Asu.Data.Mapping.Work
{
    public partial class DirectoryOfTypesOfWorkMap : NopEntityTypeConfiguration<DirectoryOfTypesOfWork>
    {
        public DirectoryOfTypesOfWorkMap() 
        {
            this.ToTable("DirectoryOfTypesOfWork");
            this.HasKey(m => m.Id);

            this.HasRequired(a => a.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.SprPviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_cex).WithMany().HasForeignKey(x => x.SprCexId).WillCascadeOnDelete(false);
        }  
    }
}
