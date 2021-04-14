using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fietskoerier_WPF.Components
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        private bool _isPasswordChanging;
        

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WachtwoordProperty =
            DependencyProperty.Register("Wachtwoord", typeof(string), typeof(BindablePasswordBox), 
                new PropertyMetadata(string.Empty, PasswordPropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        public string Wachtwoord
        {
            get { return (string)GetValue(WachtwoordProperty); }
            set { SetValue(WachtwoordProperty, value); }
        }

        public BindablePasswordBox ()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Wachtwoord = CodeerWachtwoord(passwordBox.Password);
            _isPasswordChanging = false;
        }
        private void UpdatePassword()
        {
            if (!_isPasswordChanging)
            {
                passwordBox.Password = Wachtwoord;
            }
            
        }


        public static string CodeerWachtwoord(string wachtwoord)
        {
            SHA1CryptoServiceProvider passwordHash = new SHA1CryptoServiceProvider();
            byte[] wachtwoordBytes = Encoding.ASCII.GetBytes(wachtwoord);
            byte[] encryptedBytes = passwordHash.ComputeHash(wachtwoordBytes);
            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
