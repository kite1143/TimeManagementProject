using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeManagementProject.Models;
using TimeManagementProject.ViewModel;
using TimeManagementProject.ViewModel.Helpers;

namespace TimeManagementProject.Views
{
    /// <summary>
    /// Interaction logic for TaskTimerWindow.xaml
    /// </summary>
    public partial class TaskTimerWindow : Window
    {
        TaskObject task;
        TimerDisplayVM timerDisplayVM;
		bool isStarted { get; set; }
		TimeSpan second = new TimeSpan(0, 0, 1);
		
		public TaskTimerWindow(TaskObject selectedTask)
        {
            InitializeComponent();
            isStarted = false;
            task = selectedTask;
            mainGrid.DataContext = task;
            timerDisplayVM = new TimerDisplayVM(task.Timer.ToString());
            timerTextBlock.DataContext = timerDisplayVM;

			System.Timers.Timer aTimer = new System.Timers.Timer();
			aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			aTimer.Interval = 1000; // ~ 1 seconds
			aTimer.Enabled = true;
		}

		private void Button_Back_Click(object sender, RoutedEventArgs e)
		{
            this.Close();
		}

		private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            isStarted = true;
        }

		private void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
            isStarted = false;

            using(SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
            {
                connection.CreateTable<TaskObject>();
                connection.Update(task);
            }
        }

		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			if (!isStarted)
			{
				return;
			}

			task.Timer += second;
			timerDisplayVM.TimerString = task.Timer.ToString();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
			{
				connection.CreateTable<TaskObject>();
				connection.Update(task);
			}
			base.OnClosing(e);
		}

	}
}
