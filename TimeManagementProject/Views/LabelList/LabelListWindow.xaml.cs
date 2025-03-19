using BTL_CNPM.View;
using SQLite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeManagementProject;
using TimeManagementProject.Models;
using TimeManagementProject.ViewModel.Helpers;
using TimeManagementProject.Views.LabelList;

namespace BTL_CNPM
{

	public partial class LabelListWindow : Page
	{
		MainWindow mainWindow;
		public LabelListWindow()
		{
			InitializeComponent();
			if (Application.Current.Properties.Contains("MainWindowInstance"))
			{
				mainWindow = Application.Current.Properties["MainWindowInstance"] as MainWindow;
			}
			ReadDataBase(); 
		}
		public void DisplaySuccess()
		{
			if (mainWindow != null)
			{
				mainWindow.DisplaySuccess("Your label is created successfully");
			}
		}
		private void ReadDataBase()
		{
			using (SQLiteConnection conn = new SQLiteConnection(DatabaseVM.databasePath))
			{
				conn.CreateTable<TodoLabel>();
				List<TodoLabel> listTodoLabel = conn.Table<TodoLabel>().OrderBy(t => t.Id).ToList();
				Dispatcher.Invoke(() =>
				{
					listBoxLabels.ItemsSource = null;
					listBoxLabels.ItemsSource = listTodoLabel;
				});
			}
		}

		
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			NewLabelWindow newLabelWindow = new NewLabelWindow(this);
			newLabelWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			newLabelWindow.ShowDialog();
			ReadDataBase();
		}

		private void listBoxLabels_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (listBoxLabels.SelectedItem is TodoLabel selectedLabel)
			{
				UpdateDeleteLabelWindow updateDeleteWindow = new UpdateDeleteLabelWindow(selectedLabel);
				updateDeleteWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				updateDeleteWindow.ShowDialog();
				ReadDataBase(); 
			}
		}
		private void FavouriteButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button button && button.Tag is TodoLabel label)
			{
				label.IsFavorite = !label.IsFavorite; 

				using (SQLiteConnection conn = new SQLiteConnection(DatabaseVM.databasePath))
				{
					conn.Update(label); 
				}

				ReadDataBase(); 
			}
		}
	}
}
