using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class SubjectAndProductsModel
    {
        public int SubjecId { get; set; }
        public int ProductId { get; set; }
        public string Series { get; set; }
        public int WorkShopId { get; set; }
        public int srez_sostoyanieId { get; set; }
    }
}