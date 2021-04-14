using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fietskoerier_DAL.Data.Repositories;
using Fietskoerier_DAL.DomainModels;

namespace Fietskoerier_DAL.Data.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Zending> ZendingRepo { get; }
        IRepository<Klant> KlantRepo { get; }
        IRepository<Ontvanger> OntvangerRepo { get; }
        IRepository<Koerier> KoerierRepo { get; }
        IRepository<Gewicht> GewichtRepo { get; }
        IRepository<Leveringstermijn> LeveringstermijnRepo { get; }
        IRepository<KlantAfrekening> KlantAfrekeningRepo { get;  }
        int Save();

    }
}
