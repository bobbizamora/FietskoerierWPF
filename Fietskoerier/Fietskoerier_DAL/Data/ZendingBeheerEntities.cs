using Fietskoerier_DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fietskoerier_DAL.Data
{
    public class ZendingBeheerEntities : DbContext
    {
        public ZendingBeheerEntities() : base("name=ZendingbeheerDBConnectionString")
        {

        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Zending> Zendingen { get; set; }
        public DbSet<Ontvanger> Ontvangers { get; set; }
        public DbSet<Koerier> Koeriers { get; set; }
        public DbSet<Gewicht> Gewichten { get; set; }
        public DbSet<Leveringstermijn> Leveringstermijnen { get; set; }
        public DbSet<KlantAfrekening> KlantAfrekeningen { get; set; }
    }
}
