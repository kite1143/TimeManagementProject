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
using TimeManagementProject.ViewModel;

namespace TimeManagementProject.Views.DashBoard
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            InitializeComponent();
            InitComboBox();
        }

        private void InitComboBox()
        {
            DateTime now = DateTime.Now;
            int currentMonth = now.Month;
            int currentYear = now.Year;

            List<int> monthList = new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
            };

            List<int> yearList = new List<int>();
            for(int i = currentYear-50; i <= currentYear; i++)
            {
                yearList.Add(i);
            }

            monthComboBox.ItemsSource = monthList;
            yearComboBox.ItemsSource = yearList;
			monthComboBox.SelectedItem = currentMonth;
			yearComboBox.SelectedItem = currentYear;
        }

		private void monthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            if(yearComboBox.SelectedItem != null && monthComboBox.SelectedItem != null)
                this.DataContext = new PieChartVM(int.Parse(yearComboBox.SelectedItem.ToString()), int.Parse(monthComboBox.SelectedItem.ToString()));
		}

		private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (yearComboBox.SelectedItem != null && monthComboBox.SelectedItem != null)
				this.DataContext = new PieChartVM(int.Parse(yearComboBox.SelectedItem.ToString()), int.Parse(monthComboBox.SelectedItem.ToString()));
		}
	}
}
