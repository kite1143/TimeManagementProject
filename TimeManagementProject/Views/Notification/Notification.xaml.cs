﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimeManagementProject.Views.Notification
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : Window
    {

        MainWindow mainWindow;

        Rect _ScreenArea = SystemParameters.WorkArea;

        public string Header { get; set; }
        public string Message { get; set; }
        public string ImagePath { get; set; }
        public LinearGradientBrush Gradient { get; set; }
        public SolidColorBrush RecFill { get; set; }


        public Notification()
        {
			if (Application.Current.Properties.Contains("MainWindowInstance"))
			{
				mainWindow = Application.Current.Properties["MainWindowInstance"] as MainWindow;
			}
			InitializeComponent();
            _ScreenArea = new Rect(mainWindow.Left-20, mainWindow.Top-20, mainWindow.Width, mainWindow.Height);
            this.DataContext = this;
            _Border.MouseEnter += _Border_MouseEnter;
            _Border.MouseLeave += _Border_MouseLeave;
        }

        public Notification(string header, string message, string imagePath, LinearGradientBrush gradient, SolidColorBrush recFill)
            :this()
        {
            Header = header;
            Message = message;
            ImagePath = imagePath;
            Gradient = gradient;
            RecFill = recFill;
        }

        private void _Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Storyboard fadeOUt = (Storyboard)this.Resources["CloseButtonFadeOutAnimation"];
            fadeOUt.Begin();
        }

        private void _Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Storyboard fadeIn = (Storyboard)this.Resources["CloseButtonFadeInAnimation"];
            fadeIn.Begin();
        }

        private void _Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Positioning to the bottom right corner
            this.Left = _ScreenArea.Right - this.Width;
            this.Top = _ScreenArea.Bottom - this.Height - 10;

            // Slide in window
            Storyboard slidein = (Storyboard)this.Resources["WindowSlideInAnimation"];
            slidein.Begin();
        }

       
        private void WindowSlideInAnimation_Completed(object sender, EventArgs e)
        {
            // Slide in Complete then decrease rectangle length
            this.Left = _ScreenArea.Right - this.Width-10;
            Storyboard decreaseWidth = (Storyboard)this.Resources["RectangleWidthDecreaseAnimation"];
            decreaseWidth.Begin();
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            // after decrease width of rectangle slide out window
            Storyboard SlideOut = (Storyboard)this.Resources["WindowSlideOutAnimation"];
            this.Left = _ScreenArea.Right - this.Width;
            SlideOut.Begin();
        }

        private void WindowSlideOutAnimation_Completed(object sender, EventArgs e)
        {
            // after slide out close the window
            this.Close();
        }

		private void _Close_Click(object sender, RoutedEventArgs e)
		{
            this.Close();
		}
	}
}
