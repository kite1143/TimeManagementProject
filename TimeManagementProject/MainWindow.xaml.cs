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
using TimeManagementProject.Views.Notification;

namespace TimeManagementProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			Application.Current.Properties["MainWindowInstance"] = this;
			navframe.Navigate((sidebar.Items[1] as NavButton).Navlink);
		}

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = sidebar.SelectedItem as NavButton;

            navframe.Navigate(selected.Navlink);
        }
		private SolidColorBrush HextoSolidBrush(string Hex)
		{
			return new SolidColorBrush((Color)ColorConverter.ConvertFromString(Hex));
		}

		public void DisplaySuccess(String message)
		{
			Notification success = new Notification(
				  "Success",
				  message,
				  "/Assets/Resources/success_icon.png",
				  (LinearGradientBrush)this.Resources["GreenGradient"],
				  HextoSolidBrush("#36AE3B")
				  );
			success.Show();
		}

	}
}
  