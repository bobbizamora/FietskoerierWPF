using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fietskoerier_DAL.DomainModels
{
    [Table("KlantAfrekeningen")]
    public class KlantAfrekening
    {
        [Key]
        public int KlantAfrekeningID { get; set; }

        public string BetalingsType { get; set; }

        [Index("IX_KlantnummerZendingID", 1, IsUnique=true)]
        public int Klantnummer { get; set; }

        [Index("IX_KlantnummerZendingID", 2, IsUnique=true)]
        public int ZendingID { get; set; }

        //navigatieproperties
        public Klant Klant { get; set; }
        public Zending Zending { get; set; }
    }
}
