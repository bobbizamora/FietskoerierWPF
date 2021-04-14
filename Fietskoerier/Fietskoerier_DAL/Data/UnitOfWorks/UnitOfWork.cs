using Fietskoerier_DAL.Data.Repositories;
using Fietskoerier_DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fietskoerier_DAL.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {

        #region attributen
        private IRepository<Zending> _zendingrepo;
        private IRepository<Klant> _klantrepo;
        private IRepository<Ontvanger> _ontvangerrepo;
        private IRepository<Koerier> _koerierrepo;
        private IRepository<Gewicht> _gewichtrepo;
        private IRepository<Leveringstermijn> _leveringstermijnrepo;
        private IRepository<KlantAfrekening> _klantafrekeningrepo;
        #endregion

        public UnitOfWork(ZendingBeheerEntities zendingBeheerEntities)
        {
            this.ZendingBeheerEntities = zendingBeheerEntities;
        }

        private ZendingBeheerEntities ZendingBeheerEntities { get; }


        #region repositories
        public IRepository<Zending> ZendingRepo
        {
            get
            {
                if (_zendingrepo == null)
                {
                    _zendingrepo = new Repository<Zending>(this.ZendingBeheerEntities);
                }
                return _zendingrepo;
            }
        }

        public IRepository<Klant> KlantRepo
        {
            get
            {
                if (_klantrepo == null)
                {
                    _klantrepo = new Repository<Klant>(this.ZendingBeheerEntities);
                }
                return _klantrepo;
            }
        }

        public IRepository<Ontvanger> OntvangerRepo
        {
            get
            {
                if (_ontvangerrepo == null)
                {
                    _ontvangerrepo = new Repository<Ontvanger>(this.ZendingBeheerEntities);
                }
                return _ontvangerrepo;
            }
        }


        public IRepository<Koerier> KoerierRepo
        {
            get
            {
                if (_koerierrepo == null)
                {
                    _koerierrepo = new Repository<Koerier>(this.ZendingBeheerEntities);
                }
                return _koerierrepo;
            }
        }

        public IRepository<Gewicht> GewichtRepo
        {
            get
            {
                if (_gewichtrepo == null)
                {
                    _gewichtrepo = new Repository<Gewicht>(this.ZendingBeheerEntities);
                }
                return _gewichtrepo;
            }
        }

        public IRepository<Leveringstermijn> LeveringstermijnRepo
        {
            get
            {
                if (_leveringstermijnrepo == null)
                {
                    _leveringstermijnrepo = new Repository<Leveringstermijn>(this.ZendingBeheerEntities);
                }
                return _leveringstermijnrepo;
            }
        }

        public IRepository<KlantAfrekening> KlantAfrekeningRepo
        {
            get
            {
                if (_klantafrekeningrepo == null)
                {
                    _klantafrekeningrepo = new Repository<KlantAfrekening>(this.ZendingBeheerEntities);
                }
                return _klantafrekeningrepo;
            }
        }
        #endregion

        public void Dispose()
        {
            ZendingBeheerEntities.Dispose();
        }

        public int Save()
        {
            return ZendingBeheerEntities.SaveChanges();
        }
    }
}
