using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Msi;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;

namespace Asu.Core.Domain.Work
{
    public partial class DirectiveWork : BaseEntity
    {
        public int PkpId { get; set; }

        public int OboznId { get; set; }

        public int CexIzgId { get; set; }

        public int CexPotrId { get; set; }

        public int link_uch { get; set; }

        public decimal? Directive_work_sdeln_izg { get; set; }

        public decimal? Directive_work_povr_izg { get; set; }

        public decimal? Directive_work_sdeln_usl { get; set; }

        public decimal? Directive_work_povr_usl { get; set; }

        public int? DirectoryOfTypesOfWorkId { get; set; }

        public string Prim { get; set; }

        public string NomDok { get; set; }

        public int SprPviId { get; set; }

        public int? CustomerId { get; set; }

        public int DocumentStatusId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int ExceptionForWorkId { get; set; }



        public virtual DirectoryOfTypesOfWork DirectoryOfTypesOfWork { get; set; }

        public virtual Spr_pvi Spr_pvi { get; set; }

        public virtual Spr_cex Spr_cex { get; set; }

        public virtual Spr_obozn Spr_obozn { get; set; }

        public virtual Spr_pkp Spr_pkp { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual ExceptionForWork ExceptionForWork { get; set; }
    }
}
