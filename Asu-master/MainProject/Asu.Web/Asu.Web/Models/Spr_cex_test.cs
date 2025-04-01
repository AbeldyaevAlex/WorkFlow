using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asu.Web.Models
{
    [Table("Spr_cex_test", Schema = "dbo")]
    public class Spr_cex_test
    {
        [Key]
        public int Id { get; set; }
        public string cex { get; set; }
    }
}