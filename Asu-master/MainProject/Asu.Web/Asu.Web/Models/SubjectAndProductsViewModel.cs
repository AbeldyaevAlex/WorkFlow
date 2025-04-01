using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Models
{
    public class SubjectAndProductsViewModel
    {
        [Display(Name = "Тема")]
        [Required(ErrorMessage = "{0} обязательное поле")]
        public int SubjecId { get; set; }

        public int ProductId { get; set; }

        public string Series { get; set; }
        [Display(Name = "Цех")]
        [Required(ErrorMessage = "{0} обязательное поле")]
        public int WorkShopId { get; set; }

        public DateTime? start_date { get; set; }

        [Display(Name = "Состояние")]
        [Required(ErrorMessage = "{0} обязательное поле")]
        public int srez_sostoyanieId { get; set; }

        public IList<SelectListItem> AvaliableConditions { get; set; }
        public IList<SelectListItem> AvaliableWorkShop { get; set; }
        public IList<SelectListItem> ThemeList { get; set; }

        public SubjectAndProductsViewModel()
        {
            AvaliableConditions = new List<SelectListItem>();
            AvaliableWorkShop = new List<SelectListItem>();
            ThemeList = new List<SelectListItem>();
        }
    }
}