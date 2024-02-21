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
using BlApi;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        public Mode Mode { get; private set; }
        static readonly IBl s_bl = Factory.Get();
        //public static readonly DependencyProperty CurrentEngineerProperty =//הגדרת משתנה תלויות
        //             DependencyProperty.Register("CurrentEngineer", typeof(IEnumerable<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty CurrentEngineerProperty =
    DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));




        public BO.Engineer? CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
           
        }
        public EngineerWindow(int Id = 0)
        {

           // InitializeComponent();
            if (Id == 0)
            {
                CurrentEngineer = new BO.Engineer();
                Mode = Mode.add;
                BO.Engineer newEngineer = new BO.Engineer() { ID = 0 };
                //creat new engineer
            }
            else
            {
                Mode = Mode.update;
                try
                {
                    CurrentEngineer = s_bl.Engineer.EngineerDetailsRequest(Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }//c-tor



        private void addNupdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Mode == Mode.add)//adding
            {
                try
                {
                    s_bl.Engineer.AddEngineer(CurrentEngineer!);
                    MessageBox.Show("The Engineer was added successefully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    s_bl.Engineer.UpdateEngineer(CurrentEngineer!);
                    MessageBox.Show("The Engineer was updated successefully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }//update


        }
    }
}