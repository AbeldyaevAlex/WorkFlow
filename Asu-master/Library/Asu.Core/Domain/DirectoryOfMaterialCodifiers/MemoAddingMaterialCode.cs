using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Localization;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.Security;
using Asu.Core.Domain.Seo;
using Asu.Core.Domain.StatusDirectory;
using Asu.Core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asu.Core.Domain.DirectoryOfMaterialCodifiers
{
    public partial class MemoAddingMaterialCode : BaseEntity, ILocalizedEntity, ISlugSupported, IAclSupported
    {
        //private ICollection<DocumentStatus> _statusCollection;

        public int NoMemorandumLine { get; set; }

        public string Km { get; set; }

        public string Dbt { get; set; }

        public string Dsh { get; set; }

        public decimal? Ves { get; set; }

        public int? NmSkmId { get; set; }

        public int? MarkaId { get; set; }

        public int? GostId { get; set; }

        public int? EizmId { get; set; }

        public int? KgrId { get; set; }

        public int? OtsId { get; set; }
        /// <summary>
        /// ///OGT
        /// </summary>
        public int? OgtId { get; set; }

        public string NaimOgt { get; set; }

        public int? OGT { get; set; }
        //////////////////////////////////
        ///
        /// 
        /// <summary>
        /// GrMaterId
        /// </summary>
        ////public int GrMaterId { get; set; }

        public int? NomerGrMater { get; set; }

        public string NmGrMater { get; set; }

        ///////////////////////////////////
        
        public int? SortMaterId { get; set; }

        public int? KsimKm { get; set; }

        public int? SortamMaterId { get; set; }

        public int? BalschId { get; set; }

        /// <summary>
        /// GrMaterId
        /// </summary>
        public int? PrkmId { get; set; }

        public string PrKm { get; set; }

        public string NmPrkm { get; set; }

        //////////////////////////////////

        public int? DocumentStatusId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int? GRMaterId { get; set; }

        public string NomenklNomer { get; set; }

        public int? SortOGT { get; set; }

        public int? Spr_pviId { get; set; }

        public string OpisanCherteg { get; set; }

        public string DopolnNomProfil { get; set; }

        public string PriznTto { get; set; }

        public string Comment { get; set; }

        public bool AtWork { get; set; }

        public int? InTheUsersWorkId { get; set; }



        public virtual Spr_pvi Spr_pvi { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DirectoryOfMaterialName DirectoryOfMaterialName { get; set; }

        public virtual MarkMater MarkMater { get; set; }

        public virtual GostMater GostMater { get; set; }

        public virtual SprBalSch SprBalSch { get; set; }

        public virtual SprPrKm SprPrKm { get; set; }

        public virtual SprGrMater SprGrMater { get; set; }

        public bool SubjectToAcl { get; set; }




        //public virtual ICollection<DocumentStatus> Statuses
        //{
        //    get { return _statusCollection ?? (_statusCollection = new List<DocumentStatus>()); }
        //    protected set { _statusCollection = value; }
        //}
    }
}
