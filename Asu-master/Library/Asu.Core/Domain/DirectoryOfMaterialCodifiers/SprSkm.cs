using Asu.Core.Domain.Catalog;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asu.Core.Domain.DirectoryOfMaterialCodifiers
{
    public partial class SprSkm : BaseEntity
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(12, ErrorMessage = "Максимальная длина кода 12 символов")]
        [RegularExpression("^[0-9]{1,12}$", ErrorMessage = "Поле не может быть отрицательным")]
        public string Km { get; set; }

        public string Dbt { get; set; }

        public string Dsh { get; set; }

        public decimal? Ves { get; set; }
        [Required]
        public int NmSkmId { get; set; }

        public int MarkaId { get; set; }

        public int GostId { get; set; }

        public int EizmId { get; set; }

        public int KgrId { get; set; }

        public int OtsId { get; set; }

        public int OgtId { get; set; }

        public int BalschId { get; set; }

        public int PrkmId { get; set; }

        public int DocumentStatusId { get; set; }

        public int? CustomerId { get; set; }

        public string Operation { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? PeriodOpenDate { get; set; }

        public DateTime? PeriodCloseDate { get; set; }

        public int GRMaterId { get; set; }

        public string NomenklNomer { get; set; }

        public int SortOGT { get; set; }

        public int Spr_pviId { get; set; }

        public string OpisanCherteg { get; set; }

        public string DopolnNomProfil { get; set; }

        public string PriznTto { get; set; }
    



        public virtual Spr_pvi Spr_pvi { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DirectoryOfMaterialName DirectoryOfMaterialName { get; set; }

        public virtual MarkMater MarkMater { get; set; }

        public virtual GostMater GostMater { get; set; }

        //public virtual SprBalSch SprBalSch { get; set; }

        public virtual SprPrKm SprPrKm { get; set; }

        public virtual SprGrMater SprGrMater { get; set; }

        public virtual SprOgt SprOgt { get; set; }

        public virtual SprOts SprOts { get; set; }

        public virtual SprKgr SprKgr { get; set; }

        public virtual SprEizm SprEizm { get; set; }
    }
}
