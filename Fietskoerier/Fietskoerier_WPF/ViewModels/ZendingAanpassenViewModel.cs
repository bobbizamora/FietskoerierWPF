using Fietskoerier_DAL.Data;
using Fietskoerier_DAL.Data.UnitOfWorks;
using Fietskoerier_DAL.DomainModels;
using Fietskoerier_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fietskoerier_WPF.ViewModels
{
    public class ZendingAanpassenViewModel : BasisViewModel
    {

        IUnitOfWork unitOfWork = new UnitOfWork(new ZendingBeheerEntities());

        private DateTime _aanmaakDatum = DateTime.Now;

        public Zending Zending { get; set; }
        public Klant klant { get; set; }
        public Ontvanger Ontvanger { get; set; }

        #region OntvangerGegevens
        public string OntvangerNaam { get; set; }
        public string OntvangerAdres { get; set; }
        public string OntvangerGemeente { get; set; }
        public string OntvangerTelefoon { get; set; }
        public string OntvangerEmail { get; set; }
        #endregion

        #region KlantGegevens
        public string KlantVoornaam { get; set; }
        public string KlantAchternaam { get; set; }
        public string KlantStraat { get; set; }
        public string KlantHuisnummer { get; set; }
        public string KlantPostcode { get; set; }
        public string KlantGemeente { get; set; }
        public string KlantEmail { get; set; }
        public string KlantTelefoonnummer { get; set; }
        #endregion
        public int ZendingAantalPaketten { get; set; }
        public Leveringstermijn GeselecteerdLeveringstermijn { get; set; }
        public Gewicht GeselecteerdGewicht { get; set; }
        public Koerier GeselecteerdKoerier { get; set; }
        public ObservableCollection<Leveringstermijn> Leveringstermijnen { get; set; }
        public ObservableCollection<Gewicht> Gewichten { get; set; }
        public ObservableCollection<Koerier> Koeriers { get; set; }
        public DateTime Aanmaakdatum
        {
            get { return _aanmaakDatum; }
            set { _aanmaakDatum = value; }
        }


        public ZendingAanpassenViewModel(int? ZendingID)
        {
            Leveringstermijnen = new ObservableCollection<Leveringstermijn>(unitOfWork.LeveringstermijnRepo.Ophalen());
            Gewichten = new ObservableCollection<Gewicht>(unitOfWork.GewichtRepo.Ophalen());
            Koeriers = new ObservableCollection<Koerier>(unitOfWork.KoerierRepo.Ophalen());

            if (ZendingID != null)
            {
                Zending = unitOfWork.ZendingRepo.Ophalen(x=>x.ZendingID == ZendingID, x => x.Ontvanger, x => x.Koerier, x => x.Klant, x => x.Leveringstermijn, x => x.Gewicht).SingleOrDefault();

                KlantVoornaam = Zending.Klant.Voornaam;
                KlantAchternaam = Zending.Klant.Achternaam;
                KlantStraat = Zending.Klant.Straat;
                KlantHuisnummer = Zending.Klant.Huisnummer;
                KlantPostcode = Zending.Klant.Postcode;
                KlantGemeente = Zending.Klant.Gemeente;
                KlantEmail = Zending.Klant.Email;
                KlantTelefoonnummer = Zending.Klant.Telefoonnummer;
                OntvangerNaam = Zending.Ontvanger.Naam;
                OntvangerNaam = Zending.Ontvanger.Naam;
                OntvangerAdres = Zending.Ontvanger.Adres;
                OntvangerGemeente = Zending.Ontvanger.Gemeente;
                OntvangerEmail = Zending.Ontvanger.Email;
                OntvangerTelefoon = Zending.Ontvanger.Telefoon;
                GeselecteerdLeveringstermijn = Leveringstermijnen.FirstOrDefault(x => x.LeveringstermijnID == Zending.LeveringstermijnID) ;
                GeselecteerdGewicht = Gewichten.FirstOrDefault(x => x.GewichtID == Zending.GewichtID);
                GeselecteerdKoerier = Koeriers.FirstOrDefault(x => x.KoerierID == Zending.KoerierID);
            }
            else
            {
                Zending = new Zending();
            }
           
            Aanmaakdatum = DateTime.Now;
        }
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "KlantVoornaam" && string.IsNullOrEmpty(KlantVoornaam))
                {
                    return "De voornaam van de klant is verplicht!";
                }
                if (columnName == "KlantAchternaam" && string.IsNullOrEmpty(KlantAchternaam))
                {
                    return "De achternaam van de klant is verplicht!";
                }
                if (columnName == "KlantStraat" && string.IsNullOrEmpty(KlantStraat))
                {
                    return "De straat van de klant is verplicht!";
                }
                if (columnName == "KlantHuisnummer" && string.IsNullOrEmpty(KlantHuisnummer))
                {
                    return "De huisnummer van de klant is verplicht!";
                }
                if (columnName == "KlantPostcode" && string.IsNullOrEmpty(KlantPostcode))
                {
                    return "De postcode van de klant is verplicht!";
                }
                if (columnName == "KlantGemeente" && string.IsNullOrEmpty(KlantGemeente))
                {
                    return "De gemeente van de klant is verplicht!";
                }
                if (columnName == "KlantEmail" && string.IsNullOrEmpty(KlantEmail))
                {
                    return "De e-mail van de klant is verplicht!";
                }
                if (columnName == "KlantTelefoonnummer" && string.IsNullOrEmpty(KlantTelefoonnummer))
                {
                    return "De telefoon van de klant is verplicht!";
                }
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
                if (columnName == "ZendingAantalPaketten" && ZendingAantalPaketten == 0)
                {
                    return "Aantal Paketten moet een ingevuld veld zijn!";
                }
                return "";
            }
        }

        public override bool CanExecute(object parameter)
        {
            
                return true;
            
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Dashboard": NaarDasboard(); break;
                case "Bewaren": ZendingBewaren(); break;
                case "Home":  Uitloggen(); break;
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

        private void ZendingBewaren()
        {
            if (Zending.ZendingID > 0)
            {
                Zending.Klant.Voornaam = KlantVoornaam;
                Zending.Klant.Achternaam = KlantAchternaam;
                Zending.Klant.Straat = KlantStraat;
                Zending.Klant.Huisnummer = KlantHuisnummer;
                Zending.Klant.Postcode = KlantPostcode;
                Zending.Klant.Gemeente = KlantGemeente;
                Zending.Klant.Email = KlantEmail;
                Zending.Klant.Telefoonnummer = KlantTelefoonnummer;
                unitOfWork.KlantRepo.Aanpassen(Zending.Klant);

                Zending.Ontvanger.Naam = OntvangerNaam;
                Zending.Ontvanger.Adres = OntvangerAdres;
                Zending.Ontvanger.Gemeente = OntvangerGemeente;
                Zending.Ontvanger.Email = OntvangerEmail;
                Zending.Ontvanger.Telefoon = OntvangerTelefoon;
                unitOfWork.OntvangerRepo.Aanpassen(Zending.Ontvanger);

                Zending.AantalPaketten = ZendingAantalPaketten;
                Zending.Afhaaldatum = Zending.Afhaaldatum;
                Zending.Leveringstermijn = GeselecteerdLeveringstermijn;
                Zending.Gewicht = GeselecteerdGewicht;
                Zending.Koerier = GeselecteerdKoerier;
                if (Zending.IsGeldig())
                {
                    unitOfWork.ZendingRepo.Aanpassen(Zending);
                }
                int ok = unitOfWork.Save();
                if (ok > 0)
                {
                    AdminDashboardView view = new AdminDashboardView();
                    AdminViewModel vm = new AdminViewModel();
                    view.DataContext = vm;
                    view.Show();
                    System.Windows.Application.Current.Windows[0].Close();
                }
            }
            else
            {
                Zending zending = new Zending();
                zending.Aanmaakdatum = Aanmaakdatum;
                zending.Afhaaldatum = Zending.Afhaaldatum;
                zending.AantalPaketten = ZendingAantalPaketten;
                zending.Status = false;
                zending.Leveringstermijn = GeselecteerdLeveringstermijn;
                zending.Gewicht = GeselecteerdGewicht;
                zending.Koerier = GeselecteerdKoerier;

                Klant klant = new Klant();
                klant.Voornaam = KlantVoornaam;
                klant.Achternaam = KlantAchternaam;
                klant.Straat = KlantStraat;
                klant.Huisnummer = KlantHuisnummer;
                klant.Postcode = KlantPostcode;
                klant.Gemeente = KlantGemeente;
                klant.Email = KlantEmail;
                klant.Telefoonnummer = KlantTelefoonnummer;
                klant.WachtwoordHash = "Geenwachtwoord";// Dit is een work around. Elke klant heeft een wachtwoord nodig.
                unitOfWork.KlantRepo.Toevoegen(klant);

                Ontvanger ontvanger = new Ontvanger();
                ontvanger.Naam = OntvangerNaam;
                ontvanger.Adres = OntvangerAdres;
                ontvanger.Gemeente = OntvangerGemeente;
                ontvanger.Telefoon = OntvangerTelefoon;
                ontvanger.Email = OntvangerEmail;
                unitOfWork.OntvangerRepo.Toevoegen(ontvanger);

                zending.Klant = klant;
                zending.Ontvanger = ontvanger;

                if (zending.IsGeldig())
                {
                    unitOfWork.ZendingRepo.Toevoegen(zending);
                }
                int ok = unitOfWork.Save();
                if (ok > 0)
                {
                    AdminDashboardView view = new AdminDashboardView();
                    AdminViewModel vm = new AdminViewModel();
                    view.DataContext = vm;
                    view.Show();
                    System.Windows.Application.Current.Windows[0].Close();
                }
            }
            
        }

        private void NaarDasboard()
        {
            AdminDashboardView view = new AdminDashboardView();
            AdminViewModel vm = new AdminViewModel();
            view.DataContext = vm;
            view.Show();
            System.Windows.Application.Current.Windows[0].Close();
        }

    }
}
