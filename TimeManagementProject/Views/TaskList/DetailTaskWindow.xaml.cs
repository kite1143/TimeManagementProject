using BTL_CNPM.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			List<TodoLabel> listLabel = ReadTodoLabelTable();
			labelComboBox.ItemsSource = listLabel;
			task = selectedTask;
			titleTextBox.Text = task.Title;
			descriptionTextBox.Text = task.Description;
			startDatePicker.SelectedDate = task.StartDate;
			dueDatePicker.SelectedDate = task.DueDate;
			Debug.Print(task.Label);
			if (String.IsNullOrWhiteSpace(task.Label) || task.Label.Equals("Non Label") || task.Label.Equals("None"))
			{
				labelComboBox.SelectedIndex = 0;
			}
			else
			{
				for (int i = 0; i < listLabel.Count; i++)
				{
					if (task.Label.Equals(listLabel[i].Name))
					{
						labelComboBox.SelectedIndex = i;
						break;
					}
				}
			}
		}
		private List<TodoLabel> ReadTodoLabelTable()
		{
			List<TodoLabel> listLabel = new List<TodoLabel>();
			listLabel.Add(new TodoLabel()
			{
				Id = -1,
				Name = "None"
			});
			using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
			{
				connection.CreateTable<TodoLabel>();
				listLabel.AddRange(connection.Table<TodoLabel>().ToList());
			}
			return listLabel;
		}
		private void Button_Update_Click(object sender, RoutedEventArgs e)
		{
            task.Title = titleTextBox.Text;
            task.Description = descriptionTextBox.Text;
            task.StartDate = startDatePicker.SelectedDate;
            task.DueDate = dueDatePicker.SelectedDate;
			task.Label = labelComboBox.SelectedValue.ToString();
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
