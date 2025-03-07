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

namespace BTL_CNPM
{

	public partial class LabelListWindow : Window
	{
		public LabelListWindow()
		{
			InitializeComponent();
			ReadDataBase(); // Gọi hàm lấy dữ liệu từ SQLite
		}

		private void ReadDataBase()
		{
			using (SQLiteConnection conn = new SQLiteConnection(DatabaseVM.databasePath))
			{
				conn.CreateTable<TodoLabel>();
				List<TodoLabel> listTodoLabel = conn.Table<TodoLabel>().ToList();
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
				ReadDataBase(); // Cập nhật lại danh sách sau khi sửa/xóa
			}
		}

		
	}
}
