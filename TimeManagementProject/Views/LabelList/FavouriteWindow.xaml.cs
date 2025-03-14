using BTL_CNPM.Model;
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
using TimeManagementProject.ViewModel.Helpers;

namespace TimeManagementProject.Views.LabelList
{
    /// <summary>
    /// Interaction logic for FavouriteWindow.xaml
    /// </summary>
    public partial class FavouriteWindow : Window
    {
        public FavouriteWindow()
        {
            InitializeComponent();
			LoadFavouriteLabels();
        }
		private void LoadFavouriteLabels()
		{
			using (var conn = new SQLiteConnection(DatabaseVM.databasePath))
			{
				var favorites = conn.Table<TodoLabel>().Where(t => t.IsFavorite).ToList();
				FavouriteListBox.ItemsSource = favorites;
			}
		}
	}
}
