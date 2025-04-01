using Asu.Core.Domain.StatusDirectory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.ViewModel
{
    public class GostMaterGridViewModel
    {
        public int Id { get; set; }
        [Required]
        [Remote("IsProductName_Available", "Validation")]
        public string Gost { get; set; }
        [UIHint("TypeEditor")]
        public string StatusDocument { get; set; }
        [UIHint("TypeEditor")]
        public int StatusDocumentId { get; set; }
        [DisplayName("PeriodOpenDate")]
        [DataType(DataType.Date)]
        public DateTime? PeriodOpenDate { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }
    }
}