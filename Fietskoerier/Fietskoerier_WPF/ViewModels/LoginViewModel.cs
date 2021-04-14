using Fietskoerier_DAL.Data;
using Fietskoerier_DAL.Data.Helpers;
using Fietskoerier_DAL.Data.UnitOfWorks;
using Fietskoerier_DAL.DomainModels;
using Fietskoerier_WPF.Commands;
using Fietskoerier_WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;




namespace Fietskoerier_WPF.ViewModels
{
    public class LoginViewModel : BasisViewModel
    {
        IUnitOfWork unitOfWork = new UnitOfWork(new ZendingBeheerEntities());

        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Foutmelding { get; set; }

        public Klant Klant { get; set; }

       

        public override string this[string columnName] => throw new NotImplementedException();

        public LoginViewModel()
        {
            
        }

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Home": return true;
                
                    
            }
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Home": NaarHome();
                    break;
                case "Inloggen": Inloggen();
                    break;
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

        private void Inloggen()
        {
            Klant = unitOfWork.KlantRepo.Ophalen(k => k.Email == Email && k.WachtwoordHash == Wachtwoord).FirstOrDefault();
            if (Klant != null)
            {
                GebruikerInfo.Id = Klant.Klantnummer;
                GebruikerInfo.GebruikerNaam = Klant.Email;
                if (Klant.IsAdmin == true)
                {
                    AdminDashboardView adminDashboardView = new AdminDashboardView();
                    AdminViewModel adminViewModel = new AdminViewModel();
                    adminDashboardView.DataContext = adminViewModel;
                    adminDashboardView.Show();
                    System.Windows.Application.Current.Windows[0].Close();
                }
                else
                {
                    KlantDashboardView klantDashboardView = new KlantDashboardView();
                    KlantDashBoardViewModel klantDashBoardViewModel = new KlantDashBoardViewModel();
                    klantDashboardView.DataContext = klantDashBoardViewModel;
                    klantDashboardView.Show();
                    System.Windows.Application.Current.Windows[0].Close();
                }
            }
            else
            {   
                Foutmelding = "Uw logingegevens zijn niet correct";
            }
        }
    }
}
