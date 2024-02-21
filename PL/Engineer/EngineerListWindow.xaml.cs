using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PL.Engineer;
using BlApi;

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly IBl s_bl = Factory.Get();
        public BO.EngineerExperience? Experience { get; set; } = BO.EngineerExperience.none;
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        public void Handle_Add_Click(object sender, RoutedEventArgs e)
        {
            //BO.Engineer? clickedEngineer= ((ListView)sender).SelectedItem as BO.Engineer;
            EngineerWindow addEngineerWindow = new EngineerWindow(0);
            addEngineerWindow.Closed += AddEngineerWindow_Closed; // Subscribe to the Closed event
             addEngineerWindow.ShowDialog();
        }

    private void EngineersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the selected engineer item
            BO.Engineer? selectedEngineer = (sender as ListView)?.SelectedItem as BO.Engineer ?? null;
            if (selectedEngineer != null)
             {
                EngineerWindow addEngineerWindow = new EngineerWindow(selectedEngineer.ID);
                addEngineerWindow.Closed += AddEngineerWindow_Closed; // Subscribe to the Closed event
                addEngineerWindow.ShowDialog();
            }
        }

        private void AddEngineerWindow_Closed(object sender, EventArgs e)
        {
            InitializeEngineerList();
        }

        private void InitializeEngineerList()
        {
            EngineerList = (Experience == BO.EngineerExperience.none) ?
                s_bl?.Engineer.ReadAllEngineer()! : s_bl?.Engineer.ReadAllEngineer(item => item.Level ==Experience!)!;
        }

        private void cbExperienceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (Experience == BO.EngineerExperience.none) ?
                s_bl?.Engineer.ReadAllEngineer()! : s_bl?.Engineer.ReadAllEngineer(item => item.Level ==Experience!)!;
        }

        public EngineerListWindow()
        {
            InitializeComponent();
            EngineerList = s_bl?.Engineer.ReadAllEngineer()!;
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows;
//using System.Windows.Controls;
//using PL.Engineer;
//using BlApi;

//namespace PL.Engineer
//{
//    /// <summary>
//    /// Interaction logic for EngineerListWindow.xaml
//    /// </summary>
//    public partial class EngineerListWindow : Window
//    {
//        static readonly IBl s_bl = Factory.Get();
//        public BO.EngineerExperience experiance { get; set; } = BO.EngineerExperience.none;
//        // Define the Dependency Property for EngineerList
//        public IEnumerable<BO.Engineer> EngineerList
//        {
//            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
//            set { SetValue(EngineerListProperty, value); }
//        }

//        public static readonly DependencyProperty EngineerListProperty =//הגדרת משתנה תלויות
//            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

//        public EngineerListWindow()
//        {
//            InitializeComponent();
//            InitializeEngineerList();
//        }//בנאי

//        private void InitializeEngineerList()
//        {
//            // Use s_bl to get the data for EngineerList
//            EngineerList = s_bl.Engineer.ReadAllEngineer(); // Replace with your actual method to get the data
//        }//פונקציית אתחול נתונים
//        private void cbEngineerExperianceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            EngineerList = (experiance == BO.EngineerExperience.none) ?
//                s_bl?.Engineer.ReadAllEngineer()! : s_bl?.Engineer.ReadAllEngineer(item => item.Level == experiance)!;
//        }//מטודה
//        private void AddEngineerButton_Click(object sender, RoutedEventArgs e)
//        {
//            // Create a new instance of EngineerWindow in Add mode
//            EngineerWindow addEngineerWindow = new EngineerWindow();
//            addEngineerWindow.ShowDialog();

//            // After the EngineerWindow is closed, refresh the EngineerList if needed
//            InitializeEngineerList();
//        }
//    }

//}
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Windows;
////using System.Windows.Controls;
////using PL.Engineer;
////using BlApi; // Make sure to add the appropriate using statement for your BlApi namespace

////namespace PL.Engineer
////{
////    /// <summary>
////    /// Interaction logic for EngineerListWindow.xaml
////    /// </summary>
////    public partial class EngineerListWindow : Window
////    {
////        static readonly IBl s_bl = Factory.Get();
////        public BO.Engineer engineer { get; set; } = BO.TaskInEn;/*BO.SemesterNames.None;*/

////        public IEnumerable<BO.Engineer> EngineerList
////        {
////            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
////            set { SetValue(EngineerListProperty, value); }
////        }

////        public static readonly DependencyProperty EngineerListProperty =
////            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

////        public EngineerListWindow()
////        {
////            InitializeComponent();
////            InitializeEngineerList();
////        }

////        private void InitializeEngineerList()
////        {
////            //EngineerList = s_bl?.Engineer.ReadAll()!;
////            EngineerList = s_bl.Engineer.ReadAllEngineer();
////        }
////    }
////}