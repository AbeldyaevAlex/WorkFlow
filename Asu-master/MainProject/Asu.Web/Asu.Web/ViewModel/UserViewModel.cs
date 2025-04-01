using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asu.Web.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Имя")]
        [StringLength(50, ErrorMessage = "{0} может содержать не более {1} символов.")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Отчество")]
        [StringLength(50, ErrorMessage = "{0} может содержать не более {1} символов.")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(50, ErrorMessage = "{0} может содержать не более {1} символов.")]
        public string LastName { get; set; }
        public string Full_Name
        {
            get
            {
                return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName);
            }
        }
        //[Required]
        //[Display(Name = "Логин")]
        //[StringLength(50, ErrorMessage = "{0} может содержать не более {1} символов.")]
        //public string Email { get; set; }
        //[Required]
        ////[DataType(DataType.Password)]
        //[Display(Name = "Пароль")]
        //public string PasswordHash { get; set; }
        //[Required]
        //[Display(Name = "Телефон")]
        //[StringLength(50, ErrorMessage = "{0} может содержать не более {1} символов.")]
        //public string Phone { get; set; }
        //public int? link_cex { get; set; }
        //[Display(Name = "Дата регистрации")]
        //public DateTime? date_registr { get; set; }
        //[Display(Name = "Дата посещения")]
        //public DateTime? date_visit { get; set; }
        //public byte[] screen { get; set; }
        //public byte[] Photo { get; set; }
        //public string AvatarUrl { get; set; }
    }
}