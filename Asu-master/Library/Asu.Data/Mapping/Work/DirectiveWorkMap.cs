using Asu.Core.Domain.Work;

namespace Asu.Data.Mapping.Work
{
    public partial class DirectiveWorkMap : NopEntityTypeConfiguration<DirectiveWork>
    {
        public DirectiveWorkMap() 
        {
            this.ToTable("DirectiveWork");
            this.HasKey(m => m.Id);
            this.Property(dwsdizg => dwsdizg.Directive_work_sdeln_izg).HasPrecision(20, 5);
            this.Property(dwpovizg => dwpovizg.Directive_work_povr_izg).HasPrecision(20, 5);
            this.Property(dwsdusl => dwsdusl.Directive_work_sdeln_usl).HasPrecision(20, 5);
            this.Property(dwpovusl => dwpovusl.Directive_work_povr_usl).HasPrecision(20, 5);

            this.HasRequired(a => a.Customer).WithMany().HasForeignKey(x => x.CustomerId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_pkp).WithMany().HasForeignKey(x => x.PkpId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DocumentStatus).WithMany().HasForeignKey(x => x.DocumentStatusId).WillCascadeOnDelete(false);                
            this.HasRequired(a => a.Spr_obozn).WithMany().HasForeignKey(x => x.OboznId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_cex).WithMany().HasForeignKey(x => x.CexIzgId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_cex).WithMany().HasForeignKey(x => x.CexPotrId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.ExceptionForWork).WithMany().HasForeignKey(x => x.ExceptionForWorkId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.Spr_pvi).WithMany().HasForeignKey(x => x.SprPviId).WillCascadeOnDelete(false);
            this.HasRequired(a => a.DirectoryOfTypesOfWork).WithMany().HasForeignKey(x => x.DirectoryOfTypesOfWorkId).WillCascadeOnDelete(false);
        }
    }
}
