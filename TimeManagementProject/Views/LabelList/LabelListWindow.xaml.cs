using BTL_CNPM.Model;
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
using TimeManagementProject.ViewModel.Helpers;
using TimeManagementProject.Views.LabelList;

namespace BTL_CNPM
{

	public partial class LabelListWindow : Window
	{
		public LabelListWindow()
		{
			InitializeComponent();
			ReadDataBase(); 
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
			NewLabelWindow newLabelWindow = new NewLabelWindow();
			newLabelWindow.ShowDialog();
			ReadDataBase();
		}

		private void listBoxLabels_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (listBoxLabels.SelectedItem is TodoLabel selectedLabel)
			{
				UpdateDeleteLabelWindow updateDeleteWindow = new UpdateDeleteLabelWindow(selectedLabel);
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
		private void OpenFavourites_Click(object sender, RoutedEventArgs e)
		{
			FavouriteWindow favouriteWindow = new FavouriteWindow();
			favouriteWindow.ShowDialog();
		}

	}
}
