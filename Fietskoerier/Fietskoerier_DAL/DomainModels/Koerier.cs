using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fietskoerier_DAL.DomainModels
{
    [Table("Koeriers")]
    public class Koerier
    {       
        [Key]
        public int KoerierID { get; set; }
        [Required]
        [MaxLength(250)]
        public string Achternaam { get; set; }
        [Required]
        [MaxLength(250)]
        public string Voornaam { get; set; }
        [Required]
        [MaxLength(100)]
        public string Telefoon { get; set; }
        [NotMapped]
        public string VolledigekoerierNaam
        {
            get { return Voornaam + " " + Achternaam; }
        }

        //Navigatieproperties
        public ICollection<Zending> Zendingen { get; set; }
    }
}
