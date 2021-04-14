using Fietskoerier_DAL.Data;
using Fietskoerier_DAL.Data.UnitOfWorks;
using Fietskoerier_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fietskoerier_WPF.Views;
using System.Security.Cryptography.X509Certificates;
using System.Data.Entity;
using System.Collections.ObjectModel;
using Fietskoerier_DAL.DomainModels;
using Fietskoerier_DAL.Data.Helpers;

namespace Fietskoerier_WPF.ViewModels
{
    public class ZendingViewModel : BasisViewModel, IDisposable
    {

        IUnitOfWork unitOfWork = new UnitOfWork(new ZendingBeheerEntities());

        private DateTime _aanmaakDatum = DateTime.Now;

        public Zending Zending { get; set; }
        public Klant Klant { get; set; }
        public Ontvanger Ontvanger { get; set; }

        public string OntvangerNaam { get; set; }
        public string OntvangerAdres { get; set; }
        public string OntvangerGemeente { get; set; }
        public string OntvangerTelefoon { get; set; }
        public string OntvangerEmail { get; set; }

        public Leveringstermijn GeselecteerdeLeveringstermijn { get; set; }
        public Gewicht GeselecteerdGewicht { get; set; }
        public DateTime Aanmaakdatum
        {
            get { return _aanmaakDatum; }
            set { _aanmaakDatum = value; }
        }

        public ObservableCollection<Leveringstermijn> Leveringstermijnen { get; set; }
        public ObservableCollection<Gewicht> Gewichten { get; set; }


        public ZendingViewModel(int? zendingID)
        {
            if (zendingID != null)
            {
                Zending = unitOfWork.ZendingRepo.Ophalen(x => x.ZendingID == zendingID).SingleOrDefault();
            }
            else
            {
                Zending = new Zending();
               

            }
            Ontvanger = new Ontvanger();

            Klant = unitOfWork.KlantRepo.Ophalen(x => x.Klantnummer == GebruikerInfo.Id).SingleOrDefault();

            Leveringstermijnen = new ObservableCollection<Leveringstermijn>(unitOfWork.LeveringstermijnRepo.Ophalen());
            Gewichten = new ObservableCollection<Gewicht>(unitOfWork.GewichtRepo.Ophalen());
            Aanmaakdatum = DateTime.Now;
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName == "OntvangerNaam" && string.IsNullOrEmpty(OntvangerNaam))
                {
                    return "Voornaam en naam zijn verplicht!";
                }
                if (columnName == "OntvangerAdres" && string.IsNullOrEmpty(OntvangerAdres))
                {
                    return "Straat en huisnummer zijn verplicht!";
                }
                if (columnName == "OntvangerGemeente" && string.IsNullOrEmpty(OntvangerGemeente))
                {
                    return "Postcode en Gemeente zijn verplicht!";
                }
                if (columnName == "OntvangerEmail" && string.IsNullOrEmpty(OntvangerEmail))
                {
                    return "E-mail is verplicht!";
                }
                if (columnName == "OntvangerTelefoon" && string.IsNullOrEmpty(OntvangerTelefoon))
                {
                    return "Telefoon is verplicht!";
                }
                if (columnName == "Zending.AantalPaketten" && Zending.AantalPaketten == 0)
                {
                    return "Aantal Paketten moet een ingevuld veld zijn!";
                }
                return "";
            }
        }

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Dashboard": return true;
                case "ZendingBewaren":
                    if (!IsGeldig())
                    {
                        return false;
                    }
                    return true;
            }
            return true;
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "ZendingBewaren": ZendingOpslaan(); break;
                case "Dashboard": NaarDasboard(); break;
                case "Home": Uitloggen(); break;

            }
        }

        private void Uitloggen()
        {
            MainView view = new MainView();
            MainViewModel mainViewModel = new MainViewModel();
            view.DataContext = mainViewModel;
            view.Show();
            System.Windows.Application.Current.Windows[0].Close();
        }

        private void NaarDasboard()
        {
            KlantDashboardView view = new KlantDashboardView();
            KlantDashBoardViewModel dashBoardViewModel = new KlantDashBoardViewModel();
            view.DataContext = dashBoardViewModel;
            view.Show();
            System.Windows.Application.Current.Windows[0].Close();
        }


        private void ZendingOpslaan()
        {
            
            Zending zending = new Zending();
            zending.Klantnummer = GebruikerInfo.Id;
            zending.Aanmaakdatum = Aanmaakdatum;
            zending.Afhaaldatum = Zending.Afhaaldatum;
            zending.AantalPaketten = Zending.AantalPaketten;
            zending.Status = false;
            zending.Leveringstermijn = GeselecteerdeLeveringstermijn;
            zending.Gewicht = GeselecteerdGewicht;

            Ontvanger ontvanger = new Ontvanger();
            ontvanger.Naam = OntvangerNaam;
            ontvanger.Adres = OntvangerAdres;
            ontvanger.Gemeente = OntvangerGemeente;
            ontvanger.Telefoon = OntvangerTelefoon;
            ontvanger.Email = OntvangerEmail;

            unitOfWork.OntvangerRepo.Toevoegen(ontvanger);

            if (zending.IsGeldig())
            {
                unitOfWork.ZendingRepo.Toevoegen(zending);
            }
            int ok = unitOfWork.Save();
            if (ok > 0)
            {
                  KlantDashboardView view = new KlantDashboardView();
                  KlantDashBoardViewModel dashBoardViewModel = new KlantDashBoardViewModel();
                  view.DataContext = dashBoardViewModel;
                  view.Show();
                  System.Windows.Application.Current.Windows[0].Close();
            }
            
        }

    }
}
