using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fietskoerier_DAL.DomainModels
{
    [Table("Gewichten")]
    public class Gewicht
    {
        [Key]
        public int GewichtID { get; set; }
        [Required]
        public string Gewichtscategorie { get; set; }
        [Required]
        [Column(TypeName ="money")]
        public decimal? Bedrag{ get; set; }
        //Navigatieproperties
        public ICollection<Zending> Zendingen { get; set; }
    }
}
