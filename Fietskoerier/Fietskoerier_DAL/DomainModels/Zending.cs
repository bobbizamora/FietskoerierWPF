using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fietskoerier_DAL.Basis;

namespace Fietskoerier_DAL.DomainModels
{
    [Table("Zendingen")]
    public class Zending : Basisklasse
    {
        [Key]
        public int ZendingID { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Aanmaakdatum { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Afhaaldatum is een verplicht veld")]
        public DateTime Afhaaldatum { get; set; }

        [Required(ErrorMessage = "Het aantal paketten is een numeriek veld")]
        public int AantalPaketten { get; set; }

        [Required]
        public bool Status { get; set; }
        public int Klantnummer { get; set; }
        public int OntvangerID { get; set; }
        public int LeveringstermijnID { get; set; }
        public int GewichtID { get; set; }
        public int? KoerierID { get; set; }

        //Navigatieproperties
        public Klant Klant { get; set; }
        public Gewicht Gewicht { get; set; }
        public Koerier Koerier { get; set; }
        public Ontvanger Ontvanger { get; set; }
        public Leveringstermijn Leveringstermijn { get; set; }
        public ICollection<KlantAfrekening> KlantAfrekeningen { get; set; }

    }
}
