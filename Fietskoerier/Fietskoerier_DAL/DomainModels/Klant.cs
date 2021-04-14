using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fietskoerier_DAL.Basis;
using System.ComponentModel;

namespace Fietskoerier_DAL.DomainModels
{
    [Table("Klanten")]
    public class Klant : Basisklasse
    {
        [Key]
        public int Klantnummer{ get; set; }

        [Required]
        [MaxLength(250)]
        public string Achternaam { get; set; }

        [Required]
        [MaxLength(250)]
        public string Voornaam { get; set; }

        [Required]
        [MaxLength(50)]
        public string WachtwoordHash { get; set; }

        [Required]
        [MaxLength(150)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [MaxLength(250)]
        public string Straat { get; set; }

        [Required]
        [MaxLength(10)]
        public string Huisnummer { get; set; }

        [Required]
        [MaxLength(60)]
        public string Gemeente { get; set; }

        [Required]
        [MaxLength(10)]
        public string Postcode { get; set; }

        [Required]
        [MaxLength(50)]
        public string Telefoonnummer { get; set; }

        [Required]
        [DefaultValue("false")]
        public bool IsAdmin { get; set; }

        [NotMapped]
        public string VolledigeKlantNaam
        {
            get { return Voornaam + " " + Achternaam; }
        }


        //navigatieproperties
        public ICollection<Zending> Zendingen { get; set; }
        public ICollection<KlantAfrekening> KlantAfrekeningen { get; set; }
        
    }
}
