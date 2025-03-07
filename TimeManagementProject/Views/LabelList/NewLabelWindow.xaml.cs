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

namespace BTL_CNPM.View
{
    /// <summary>
    /// Interaction logic for NewLabelWindow.xaml
    /// </summary>
    public partial class NewLabelWindow : Window
    {
        public NewLabelWindow()
        {
            InitializeComponent();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            TodoLabel label = new TodoLabel()
            {
                Name = textBoxName.Text,
            };
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseVM.databasePath))
            {
                conn.CreateTable<TodoLabel>();
                conn.Insert(label);
            }  
            this.Close();
		}
	}
}
