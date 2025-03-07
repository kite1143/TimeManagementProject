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
    /// Interaction logic for DetailTaskWindow.xaml
    /// </summary>
    public partial class DetailTaskWindow : Window
    {
        TaskObject task;

        public DetailTaskWindow(TaskObject selectedTask)
        {
            InitializeComponent();
            task = selectedTask;
            titleTextBox.Text = task.Title;
            descriptionTextBox.Text = task.Description;
            startDatePicker.SelectedDate = task.StartDate;
            dueDatePicker.SelectedDate = task.DueDate;
        }

		private void Button_Update_Click(object sender, RoutedEventArgs e)
		{
            task.Title = titleTextBox.Text;
            task.Description = descriptionTextBox.Text;
            task.StartDate = startDatePicker.SelectedDate;
            task.DueDate = dueDatePicker.SelectedDate;
			using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
			{
				connection.CreateTable<TaskObject>();
				connection.Update(task);
			}
            this.Close();
		}
		private void Button_Delete_Click(object sender, RoutedEventArgs e)
		{
			using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
			{
				connection.CreateTable<TaskObject>();
				connection.Delete(task);
			}
            this.Close();
		}
	}
}
