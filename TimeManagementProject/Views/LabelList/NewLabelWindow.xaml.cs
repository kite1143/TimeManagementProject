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

namespace BTL_CNPM.View
{
	/// <summary>
	/// Interaction logic for NewLabelWindow.xaml
	/// </summary>
	public partial class NewLabelWindow : Window
	{
		Page main;
		public NewLabelWindow(Page main)
		{
			InitializeComponent();
			this.main = main;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			using (SQLiteConnection conn = new SQLiteConnection(DatabaseVM.databasePath))
			{
				conn.CreateTable<TodoLabel>();
				List<TodoLabel> listLabel = conn.Table<TodoLabel>().ToList();
				foreach(var todolabel in listLabel)
				{
					if (todolabel.Name.Equals(textBoxName.Text))
					{
						MessageBox.Show("This name is already exist", "Warning", MessageBoxButton.OK);
						return;
					}
				}
				
				TodoLabel label = new TodoLabel()
				{
					Name = textBoxName.Text,
				};

				conn.Insert(label);
			}

			(main as LabelListWindow).DisplaySuccess();
			this.Close();
		}
	}
}
