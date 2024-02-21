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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Engineer;
using BlApi;
namespace PL
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void engineerWindow_Closed(object sender, EventArgs e)
        {
 
           // InitializeEngineerList();
        }

        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        { new EngineerListWindow().Show(); }
        private void btnInitialization_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to initialize?", "Confirmation", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
            {
                s_bl.InitializeDB();
            }
            // You can add more conditions for No and Cancel if needed
            else if (result == MessageBoxResult.No)
            {
                // Do something else or show another message
            }
            else if (result == MessageBoxResult.Cancel)
            {
                // Do something else or show another message
            }

        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to reset?", "Confirmation", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
            {
                s_bl.ResetDB();
            }
            // You can add more conditions for No and Cancel if needed
            else if (result == MessageBoxResult.No)
            {
                // Do something else or show another message
            }
            else if (result == MessageBoxResult.Cancel)
            {
                // Do something else or show another message
            }
        }
    }
}