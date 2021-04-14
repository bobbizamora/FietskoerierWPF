using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using Fietskoerier_WPF.ViewModels;
using System.Windows;

namespace Fietskoerier_WPF.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly LoginViewModel _viewModel;

        public LoginCommand(LoginViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.Email) &&
                !string.IsNullOrEmpty(_viewModel.Wachtwoord);
        }

        public void Execute(object parameter)
        {
            //MessageBox.Show($"Username: {_viewModel.Username}\nPassword: {_viewModel.Password}", "Info",
            //    MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
