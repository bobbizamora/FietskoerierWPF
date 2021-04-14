using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fietskoerier_DAL.DomainModels
{
    [Table("Ontvangers")]
    public class Ontvanger
    {
        [Key]
        public int OntvangerID { get; set; }
        [Required]
        [MaxLength(250)]
        public string Naam { get; set; }
        [Required]
        [MaxLength(250)]
        public string Adres { get; set; }
        [Required]
        [MaxLength(250)]
        public string Gemeente { get; set; }
        [Required]
        [MaxLength(100)]
        public string Telefoon { get; set; }
        [Required]
        [MaxLength(250)]
        public string Email { get; set; }

        //navigatieproperties
        public ICollection<Zending> Zendingen { get; set; }
    }
}
