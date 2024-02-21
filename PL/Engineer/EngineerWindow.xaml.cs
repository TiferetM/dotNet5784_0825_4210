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
        public static readonly DependencyProperty CurrentEngineerProperty = DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));
        public BO.Engineer? CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
           
        }
        public EngineerWindow(int id = 0)
        {

            InitializeComponent();
            if (Id == 0)
            {
                Mode = Mode.add;
                CurrentEngineer = new BO.Engineer();
            }
            else
            {
                 Mode = Mode.update;
                try
                 {
                    CurrentEngineer = s_bl.Engineer.EngineerDetailsRequest(id);
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging purposes
                    Console.WriteLine($"Error: {ex.Message}");
                    MessageBox.Show("An error occurred. Please check logs for details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        private void addNupdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Mode == Mode.add)//adding
            {
                try
                {
                    s_bl.Engineer.AddEngineer(CurrentEngineer!);
                    MessageBox.Show("The Engineer was added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show("The Engineer was updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }//update


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using BlApi;

//namespace PL.Engineer
//{
//    /// <summary>
//    /// Interaction logic for EngineerWindow.xaml
//    /// </summary>
//    public partial class EngineerWindow : Window
//    {
//        public Mode Mode { get; private set; }
//        static readonly IBl s_bl = Factory.Get();
//        public static readonly DependencyProperty CurrentEngineerProperty =//הגדרת משתנה תלויות
//                     DependencyProperty.Register("CurrentEngineer", typeof(IEnumerable<BO.Engineer>), typeof(EngineerWindow), new PropertyMetadata(null));
//        public BO.Engineer? CurrentEngineer
//        {
//            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
//            set { SetValue(CurrentEngineerProperty, value); }
//        }
//        public EngineerWindow(int id = 0)
//        {
//            InitializeComponent();

//            if (id == 0)
//            {
//                Mode = Mode.add;
//                CurrentEngineer = new BO.Engineer();
//            }
//            else
//            {
//                Mode = Mode.update;

//                try
//                {
//                    CurrentEngineer = s_bl.Engineer.EngineerDetailsRequest(id);
//                }
//                catch (Exception ex)
//                {
//                    // Log the exception for debugging purposes
//                    Console.WriteLine($"Error: {ex.Message}");
//                    MessageBox.Show("An error occurred. Please check logs for details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }
//        }


//        private void addNupdateButton_Click(object sender, RoutedEventArgs e)
//        {
//            if (Mode == Mode.add)//adding
//            {
//                try
//                {
//                    s_bl.Engineer.AddEngineer(CurrentEngineer!);
//                    MessageBox.Show("The Engineer was added successefully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
//                    Close();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }
//            else
//            {
//                try
//                {
//                    s_bl.Engineer.UpdateEngineer(CurrentEngineer!);
//                    MessageBox.Show("The Engineer was updated successefully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
//                    Close();

//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }//update
//        }
//    }
//}