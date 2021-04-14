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
using Fietskoerier_DAL;
using Fietskoerier_DAL.Data.Helpers;
using System.Windows.Input;

namespace Fietskoerier_WPF.ViewModels
{
    public class KlantDashBoardViewModel : BasisViewModel, ICommand, IDisposable
    {
        IUnitOfWork unitOfWork = new UnitOfWork(new ZendingBeheerEntities());


        
        public Klant Klant { get; set; }
        public DateTime LokaleAanmaakDatum { get; set; }
        public ObservableCollection<Zending> AfgewerkteZendingen { get; set; }

        public Gewicht GeselecteerdGewicht { get; set; }
        public Koerier GeselecteerdKoerier { get; set; }
        public Leveringstermijn GeselecteerdLeveringstermijn { get; set; }
        public ObservableCollection<Leveringstermijn> Leveringstermijnen { get; set; }
        public ObservableCollection<Gewicht> Gewichten { get; set; }
        public ObservableCollection<Koerier> Koeriers { get; set; }
        public ObservableCollection<Ontvanger> Ontvangers { get; set; }
        public ObservableCollection<Zending> Zendingen { get; set; }

        

        public KlantDashBoardViewModel()
        {
            Klant = unitOfWork.KlantRepo.Ophalen(x => x.Klantnummer == GebruikerInfo.Id).SingleOrDefault();

            Zendingen = new ObservableCollection<Zending>(unitOfWork.ZendingRepo.Ophalen(x => x.Klantnummer == GebruikerInfo.Id && x.Status == false, x => x.Ontvanger, x => x.Koerier));
            AfgewerkteZendingen = new ObservableCollection<Zending>(unitOfWork.ZendingRepo.Ophalen(x => x.Klantnummer == GebruikerInfo.Id && x.Status == true, x => x.Ontvanger, x => x.Koerier));

            Ontvangers = new ObservableCollection<Ontvanger>(unitOfWork.OntvangerRepo.Ophalen());
            Koeriers = new ObservableCollection<Koerier>(unitOfWork.KoerierRepo.Ophalen());
            Leveringstermijnen = new ObservableCollection<Leveringstermijn>(unitOfWork.LeveringstermijnRepo.Ophalen());
            Gewichten = new ObservableCollection<Gewicht>(unitOfWork.GewichtRepo.Ophalen());
            
            
        }

        public override string this[string columnName]
        {
            get
            {
                return "";
            }
        }

        public override bool CanExecute(object parameter)
        {
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
                case "Home":
                    naarHome();
                    break;
                case "NieuweZending":
                    NieuweZending();
                    break;
            }
        }

        private void naarHome()
        {
            MainView view = new MainView();
            MainViewModel vm = new MainViewModel();
            view.DataContext = vm;
            view.Show();
            System.Windows.Application.Current.Windows[0].Close();
        }

        private void NieuweZending(int? zendingID = null)
        {
            GebruikerInfo.Id = Klant.Klantnummer;

            ZendingViewModel vm = new ZendingViewModel(zendingID);
            ZendingAanmakenView view = new ZendingAanmakenView();
            view.DataContext = vm;
            view.Show();
            System.Windows.Application.Current.Windows[0].Close();
        }
    }
}
