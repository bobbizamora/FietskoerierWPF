using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fietskoerier_DAL.DomainModels
{
    [Table("Leveringstermijnen")]
    public class Leveringstermijn
    {
        [Key]
        public int LeveringstermijnID { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal? Bedrag { get; set; }

        //Navigatieproperties
        public ICollection<Zending> Zendingen { get; set; }
    }
}
