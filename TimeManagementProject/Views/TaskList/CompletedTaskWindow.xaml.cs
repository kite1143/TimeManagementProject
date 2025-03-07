using SQLite;
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
using TimeManagementProject.Models;
using TimeManagementProject.ViewModel.Helpers;

namespace TimeManagementProject.Views
{
    /// <summary>
    /// Interaction logic for CompletedTaskWindow.xaml
    /// </summary>
    public partial class CompletedTaskWindow : Window
    {
		List<TaskObject> listTasks;
		public CompletedTaskWindow()
        {
			InitializeComponent();
			this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			ReadDatabase();
		}


		public void ReadDatabase()
		{
			using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
			{
				connection.CreateTable<TaskObject>();
				listTasks = connection.Table<TaskObject>().Where(e => e.isCompleted == true).ToList();
			}
			taskListView.ItemsSource = listTasks;
		}

		public void UnCompletedTask(TaskObject task)
		{
			task.isCompleted = false;
			using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
			{
				connection.CreateTable<TaskObject>();
				connection.Update(task);
			}
		}

		private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
		{
			if (sender is CheckBox checkBox && checkBox.DataContext is TaskObject checkedItem)
			{
				UnCompletedTask(checkedItem);
			}
			ReadDatabase();
		}

		private void taskListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			TaskObject selectedTask = taskListView.SelectedItem as TaskObject;

			if (selectedTask == null)
			{
				return;
			}

			DetailTaskWindow detailTaskWindow = new DetailTaskWindow(selectedTask);
			detailTaskWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			detailTaskWindow.ShowDialog();

			ReadDatabase();
		}

	}
}
