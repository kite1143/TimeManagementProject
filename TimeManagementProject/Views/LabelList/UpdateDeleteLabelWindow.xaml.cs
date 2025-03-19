using SQLite;
using System;
using System.Collections;
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

namespace BTL_CNPM.View
{
    
    public partial class UpdateDeleteLabelWindow : Window
    {
        private TodoLabel _todoLabel;
        public UpdateDeleteLabelWindow(TodoLabel label)
        {
			InitializeComponent();
			_todoLabel = label;
			if (label != null)
			{
				textBoxName.Text = label.Name.ToString();
			}
		}

		private void ButtonEdit_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult mbr = MessageBox.Show("Are you sure to update this task?", "Update Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (mbr == MessageBoxResult.No)
			{
				return;
			}
			_todoLabel.Name = textBoxName.Text;
			using (SQLiteConnection conn = new SQLiteConnection(DatabaseVM.databasePath))
			{
				conn.CreateTable<TodoLabel>();
				conn.Update(_todoLabel);

			}
			this.Close();
		}

		private void ButtonDelete_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult mbr = MessageBox.Show("Are you sure to delete this label?", "Delete Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (mbr == MessageBoxResult.No)
			{
				return;
			}
			if (_todoLabel != null)
			{
				using (SQLiteConnection conn = new SQLiteConnection(DatabaseVM.databasePath))
				{
					string delLabel = _todoLabel.Name;
					conn.CreateTable<TodoLabel>();
					conn.Delete(_todoLabel);

					conn.CreateTable<TaskObject>();
					List<TaskObject> taskObjects = conn.Table<TaskObject>().Where(e => e.Label.Equals(delLabel)).ToList();
					foreach(var task in taskObjects)
					{
						task.Label = "None";
					}
					conn.UpdateAll(taskObjects);
				}
			}
			this.Close();
		}
	}
}
