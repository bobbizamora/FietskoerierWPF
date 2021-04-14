using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Fietskoerier_DAL;
using Fietskoerier_WPF.Views;
using Fietskoerier_DAL.Data;
using Fietskoerier_DAL.Data.UnitOfWorks;
using Fietskoerier_DAL.DomainModels;

namespace Fietskoerier_WPF.ViewModels
{
    public class MainViewModel : BasisViewModel, ICommand
    {
        IUnitOfWork unitOfWork = new UnitOfWork(new ZendingBeheerEntities());


        public Klant Klant { get; set; }


        public override string this[string columnName]
        {
            get
            {
                return "";
            }
        }


        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenWindowLogin": return true;
                case "OpenRegistreerWindow": return true;
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
                case "OpenWindowLogin": OpenLoginWindow(); break;
                case "OpenRegistreerWindow": OpenRegistreerWindow(); break;
            }
        }

        private void OpenLoginWindow()
        {
            
            LoginViewModel vm = new LoginViewModel();
            LoginView view = new LoginView();
            view.DataContext = vm;
            view.Show();
            System.Windows.Application.Current.Windows[0].Close();
        }

        private void OpenRegistreerWindow()
        {

            RegistreerViewModel vm = new RegistreerViewModel();
            RegistreerView view = new RegistreerView();
            view.DataContext = vm;
            view.Show();
            System.Windows.Application.Current.Windows[0].Close();
        }
    }
}
