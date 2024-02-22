using BlApi;
using BO;
using PL.Engineer;
using PL.GanttChart;
using PL.Task;
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


namespace PL.Manager;

/// <summary>
/// Interaction logic for ManagerWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{

    static readonly IBl s_bl = Factory.Get();
    //public BO.EngineerExperience? Experience { get; set; } = BO.EngineerExperience.none;

    public ManagerWindow()
    {
        InitializeComponent();
    }

    private void ListOfEngineers_Click(object sender, RoutedEventArgs e)
    { new EngineerListWindow().Show(); }

    private void TaskForList_Click(object sender, RoutedEventArgs e)
    { new TaskForListWindow().Show(); }

    private void GanttChart_Click(object sender, RoutedEventArgs e)
    { new GanttChartWindow().Show(); }

    
    private void ProjectSchedule_Click(object sender, RoutedEventArgs e)
    {
        s_bl?.Milestone.CreateScheduledProject();//קריאה לפונקציה של לו"ז פרויקט
    }
    private void Milestone_Click(object sender, RoutedEventArgs e)
    {
        if (sender is BO.Milestone milestone)
        {
            int id = milestone.Id;
            s_bl?.Milestone.ReadMilestoneData(id); //קריאה לפונקציה של  רשימת אבני דרך
        }
       
    }
}
