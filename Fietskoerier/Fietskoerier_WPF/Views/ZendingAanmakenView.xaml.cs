using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fietskoerier_WPF.Views
{
    /// <summary>
    /// Interaction logic for ZendingAanmakenView.xaml
    /// </summary>
    public partial class ZendingAanmakenView : Window
    {
        public ZendingAanmakenView()
        {
            InitializeComponent();
        }

        private void btnTerugNaarDashboard_Click(object sender, RoutedEventArgs e)
        {
            KlantDashboardView klantDashboardView = new KlantDashboardView();
            klantDashboardView.Show();
            this.Close();
        }

        private void btnZendingBoeken_Click(object sender, RoutedEventArgs e)
        {
            KlantDashboardView klantDashboardView = new KlantDashboardView();
            klantDashboardView.Show();
            this.Close();
        }
    }
}
