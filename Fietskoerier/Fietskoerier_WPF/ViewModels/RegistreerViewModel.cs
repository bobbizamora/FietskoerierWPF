using Fietskoerier_DAL.Data;
using Fietskoerier_DAL;
using Fietskoerier_DAL.Data.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fietskoerier_WPF.Views;
using Fietskoerier_DAL.DomainModels;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows.Input;
using Fietskoerier_WPF.Commands;
using Fietskoerier_DAL.Data.Helpers;

namespace Fietskoerier_WPF.ViewModels
{
    public class RegistreerViewModel : BasisViewModel, IDisposable
    {
        IUnitOfWork unitOfWork = new UnitOfWork(new ZendingBeheerEntities());

        public string GebruikersNaam { get; set; }
        public string WachtwoordHash { get; set; }
        public ICommand LoginCommand { get; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Straat { get; set; }
        public string  Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public string Telefoonnummer { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
       

        public RegistreerViewModel()
        {
            LoginCommand = new RegistreerCommand(this);
            
        }


        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Voornaam" && Voornaam == null)
                {
                    return "Voornaam is een verplicht veld!";
                }
                if (columnName == "Achternaam" && Achternaam == null)
                {
                    return "Achternaam is een verplicht veld!";
                }
                if (columnName == "Straat" && Straat == null)
                {
                    return "Straat is een verplicht veld!";
                }
                if (columnName == "Huisnummer" && Huisnummer == null)
                {
                    return "Huisnummer is een verplicht veld!";
                }
                if (columnName == "Postcode" && Postcode == null)
                {
                    return "Postcode is een verplicht veld!";
                }
                if (columnName == "Gemeente" && Gemeente == null)
                {
                    return "Gemeente is een verplicht veld!";
                }
                if (columnName == "Telefoonnummer" && Telefoonnummer == null)
                {
                    return "Telefoon is een verplicht veld!";
                }
                if (columnName == "Email" && Email == null)
                {
                    return "Email is een verplicht veld!";
                }
                if (columnName =="Wachtwoord" && Wachtwoord == null)
                {
                    return "Wachtwoord is een verplicht veld!";
                }
                return "";
            }
        }

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Home": return true;
                case "Registreren":                   
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
            throw new NotImplementedException();
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Home": NaarHome(); break;
                case "Registreren": Registreren(); break;
            }
        }

        private void NaarHome()
        {
            MainView view = new MainView();
            MainViewModel mainViewModel = new MainViewModel();
            view.DataContext = mainViewModel;
            view.Show();
            System.Windows.Application.Current.Windows[0].Close();

        }
        private void Registreren()
        {
            Klant klant = new Klant();
            klant.Voornaam = Voornaam;
            klant.Achternaam = Achternaam;
            klant.Straat = Straat;
            klant.Huisnummer = Huisnummer;
            klant.Postcode = Postcode;
            klant.Gemeente = Gemeente;
            klant.Telefoonnummer = Telefoonnummer;
            klant.Email = Email;
            klant.WachtwoordHash = Wachtwoord;

            if (klant.IsGeldig()) 
            {
                unitOfWork.KlantRepo.Toevoegen(klant);
                if (unitOfWork.Save() > 0)
                {
                    GebruikerInfo.Id = klant.Klantnummer;
                    GebruikerInfo.GebruikerNaam = klant.Email;

                    KlantDashboardView klantdashboardview = new KlantDashboardView();  
                    KlantDashBoardViewModel dashBoardViewModel = new KlantDashBoardViewModel(); 
                    klantdashboardview.DataContext = dashBoardViewModel;
                    klantdashboardview.Show();
                    System.Windows.Application.Current.Windows[0].Close();
                }
            }

        }

    }
}
