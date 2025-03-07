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
    
    public partial class UpdateDeleteLabelWindow : Window
    {
        private TodoLabel _todoLabel;
        public UpdateDeleteLabelWindow(TodoLabel label)
        {
            InitializeComponent();
            _todoLabel = label;
            if (label != null)
            {
                textBoxId.Text = label.Id.ToString();
                textBoxName.Text = label.Name.ToString();
            }
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{

        }

		private void ButtonEdit_Click(object sender, RoutedEventArgs e)
		{
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
           
            using(SQLiteConnection conn = new SQLiteConnection(DatabaseVM.databasePath))
            {
                conn.CreateTable<TodoLabel>();
                conn.Delete(_todoLabel);
            }
            this.Close();
		}
	}
}
