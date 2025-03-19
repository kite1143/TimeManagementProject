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

namespace TimeManagementProject.Views.LabelList
{
    /// <summary>
    /// Interaction logic for FavouriteWindow.xaml
    /// </summary>
    public partial class FavouriteWindow : Page
    {

        List<FavoriteTreeItemVm> favoriteTreeItemVms;
		MainWindow mainWindow;
		public FavouriteWindow()
        {
            InitializeComponent();
			if (Application.Current.Properties.Contains("MainWindowInstance"))
			{
				mainWindow = Application.Current.Properties["MainWindowInstance"] as MainWindow;
			}
			LoadFavouriteLabels();
        }

		private void LoadFavouriteLabels()
		{
			favoriteTreeItemVms = new List<FavoriteTreeItemVm>();
			using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
			{
				connection.CreateTable<TodoLabel>();
				List<TodoLabel> favLabelList = connection.Table<TodoLabel>().Where(e => e.IsFavorite == true).ToList();

				foreach (var favLabel in favLabelList)
				{
					connection.CreateTable<TaskObject>();
					List<TaskObject> taskFavList = connection.Table<TaskObject>().Where(e => e.Label.Equals(favLabel.Name)).Where(e => e.isCompleted!=true).ToList();

					FavoriteTreeItemVm ftiVM = new FavoriteTreeItemVm(favLabel, taskFavList);
					favoriteTreeItemVms.Add(ftiVM);
				}
			}
			favTaskTreeView.ItemsSource = favoriteTreeItemVms;
		}

		public void CompletedTask(TaskObject task)
		{
			task.isCompleted = true;
			using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
			{
				connection.CreateTable<TaskObject>();
				connection.Update(task);
			}
		}
		public void DisplaySuccess(String message)
		{
			if (mainWindow != null)
			{
				mainWindow.DisplaySuccess(message);
			}
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is CheckBox checkBox && checkBox.DataContext is TaskObject checkedItem)
			{
				CompletedTask(checkedItem);
			}
			DisplaySuccess("Task completed");
			LoadFavouriteLabels();
		}

	}
}
