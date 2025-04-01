using Asu.Core;
using Asu.Core.Domain.Customers;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.Pvi;
using Asu.Core.Domain.StatusDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class MemoAddingMaterialCodeViewModel
    {
        public int NoMemorandumLine { get; set; }

        [DataType(DataType.Text)]
        public int Id { get; set; }

        [UIHint("TextBoxEditor")]
        public string Km { get; set; }

        [UIHint("TextBoxEditorForDbt")]
        public string Dbt { get; set; }

        [UIHint("TextBoxEditorForDsh")]
        public string Dsh { get; set; }

        [UIHint("EditorVesMaterial")]
        [Column(TypeName = "decimal(38, 7)")]
        public decimal? Ves { get; set; }

        public int? NmSkmId { get; set; }

        public int? MarkaId { get; set; }

        public int? GostId { get; set; }

        public int? EizmId { get; set; }

        public int? KgrId { get; set; }

        public int? OtsId { get; set; }

        public int? OgtId { get; set; }

        public int? BalschId { get; set; }

        public int? PrkmId { get; set; }

        public int? DocumentStatusId { get; set; }

        public int? CustomerId { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime? OperationDate { get; set; }

        [UIHint("DatePickerForOpenDate")]
        [DataType(DataType.Date)]       
        public DateTime? PeriodOpenDate { get; set; }

        [UIHint("DatePickerForCloseDate")]        
        [DataType(DataType.Date)]
        public DateTime? PeriodCloseDate { get; set; }

        public int? GRMaterId { get; set; }

        public string NomenklNomer { get; set; }

        public int? SortOGT { get; set; }

        public int? Spr_pviId { get; set; }

        public string OpisanCherteg { get; set; }

        public string DopolnNomProfil { get; set; }

        public string PriznTto { get; set; }

        public MarkMater MarkMaterTest { get; set; }

        //GrMater*************************

        [UIHint("EditorGrMater")]
        public string GrMater { get; set; }
        [UIHint("EditorNoGrMater")]
        public string NomerGrMater { get; set; }
        //****************************

        [UIHint("EditorBalSch")]
        public string BalSch { get; set; }
        //OGT*************************
        [UIHint("EditorPrkm")]
        public string Prkm { get; set; }
        [UIHint("EditorPrkmOgt")]
        public string PrkmOgt { get; set; }
        [UIHint("EditorNmPrkmOgt")]
        public string NmPrkmOgt { get; set; }
        [UIHint("EditorKsimKmOgt")]
        public string KsimKm { get; set; }
        [UIHint("EditorSortamentOgt")]
        public string Sortament { get; set; }
        //****************************

        [UIHint("EditorKgr")]
        public string Kgr { get; set; }
        [UIHint("EditorOts")]
        public string Ots { get; set; }
        
        //OGT*************************

        [UIHint("EditorOgt")]
        public string OGT { get; set; }
        [UIHint("EditorNaimOgt")]
        public string NaimOgt { get; set; }

        //****************************

        [UIHint("EditorGostMaterial")]
        public string Gost { get; set; }
        [UIHint("EditorEizmMaterial")]
        public string KratNaimEizm { get; set; }
        [UIHint("TypeEditor")]
        public string Status { get; set; }
        [UIHint("EditorNameMaterial")]
        public string NameMaterial { get; set; }
        [UIHint("EditorMarkaMaterial")]
        public string MarkaMater { get; set; }

        public string FullCustomerName { get; set; }

        [UIHint("EditorComment")]
        public string Comment { get; set; }

        public bool AtWork { get; set; }

        public int? InTheUsersWorkId { get; set; }
    }
}