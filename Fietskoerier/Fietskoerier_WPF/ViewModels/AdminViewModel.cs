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
    public class AdminViewModel : BasisViewModel
    {

        IUnitOfWork unitOfWork = new UnitOfWork(new ZendingBeheerEntities());

        
        public string Foutmelding { get; set; }

        public ObservableCollection<Zending> AfgewerkteZendingen { get; set; }
        public ObservableCollection<Klant> Klanten { get; set; }
        public ObservableCollection<Leveringstermijn> Leveringstermijnen { get; set; }
        public ObservableCollection<Gewicht> Gewichten { get; set; }
        public ObservableCollection<Koerier> Koeriers { get; set; }
        public ObservableCollection<Ontvanger> Ontvangers { get; set; }
        public ObservableCollection<Zending> Zendingen { get; set; }
        public Zending GeselecteerdeZending  { get; set; }

        public AdminViewModel()
        {
            Zendingen = new ObservableCollection<Zending>(unitOfWork.ZendingRepo.Ophalen(x => x.Status == false, x => x.Ontvanger, x => x.Koerier, x => x.Klant, x => x.Leveringstermijn, x => x.Gewicht));
            AfgewerkteZendingen = new ObservableCollection<Zending>(unitOfWork.ZendingRepo.Ophalen(x => x.Status == true));

            Ontvangers = new ObservableCollection<Ontvanger>(unitOfWork.OntvangerRepo.Ophalen());
            Koeriers = new ObservableCollection<Koerier>(unitOfWork.KoerierRepo.Ophalen());
            Leveringstermijnen = new ObservableCollection<Leveringstermijn>(unitOfWork.LeveringstermijnRepo.Ophalen());
            Gewichten = new ObservableCollection<Gewicht>(unitOfWork.GewichtRepo.Ophalen());
            Klanten = new ObservableCollection<Klant>(unitOfWork.KlantRepo.Ophalen());
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

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Home": naarHome(); break;
                case "Verwijderen": ZendingVerwijderen(); break;
                case "Afgewerkt": ZendingAfgewerkt(); break;
                case "ZendingInfo": ZendingAanpassenAanmaken(); break;
            }
        }

        private void ZendingAanpassenAanmaken()
        {
            if (GeselecteerdeZending != null)
            {
                ZendingAanpassenViewModel vm = new ZendingAanpassenViewModel(GeselecteerdeZending.ZendingID);
                ZendingAanpassenView view = new ZendingAanpassenView();
                view.DataContext = vm;
                view.Show();
                System.Windows.Application.Current.Windows[0].Close();
            }
            else
            {
                ZendingAanpassenViewModel vm = new ZendingAanpassenViewModel(null);
                ZendingAanpassenView view = new ZendingAanpassenView();
                view.DataContext = vm;
                view.Show();
                System.Windows.Application.Current.Windows[0].Close();
            }
                
        }

        private void ZendingAfgewerkt()
        {
            if (GeselecteerdeZending != null)
            {  
                GeselecteerdeZending.Status = true;
                unitOfWork.ZendingRepo.Aanpassen(GeselecteerdeZending);
                unitOfWork.Save();                
                VernieuwData();
            }
            else
            {
                Foutmelding = "Selecteer eerst een zending!";
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

        private void ZendingVerwijderen()
        {
            if (GeselecteerdeZending !=null)
            {
                unitOfWork.ZendingRepo.Verwijderen(GeselecteerdeZending.ZendingID);
                int ok = unitOfWork.Save();
                if (ok > 0)
                {
                    VernieuwData();
                }
            }
            else
            {
                Foutmelding = "Selecteer eerst een zending!";
            }
        }

        private void VernieuwData()
        {
            Zendingen = new ObservableCollection<Zending>(unitOfWork.ZendingRepo.Ophalen(x => x.Status == false, x => x.Ontvanger, x => x.Koerier, x => x.Klant));
            AfgewerkteZendingen = new ObservableCollection<Zending>(unitOfWork.ZendingRepo.Ophalen(x => x.Status == true));
        }
    }
}
