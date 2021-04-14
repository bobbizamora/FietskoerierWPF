using Fietskoerier_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fietskoerier_WPF.Commands
{
    public class RegistreerCommand : ICommand
    {

        private readonly RegistreerViewModel _viewModel;

        public RegistreerCommand(RegistreerViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.GebruikersNaam) &&
                !string.IsNullOrEmpty(_viewModel.WachtwoordHash);
        }

        public void Execute(object parameter)
        {
            
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
