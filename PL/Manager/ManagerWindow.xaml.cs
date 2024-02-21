﻿using PL.Engineer;
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




}
