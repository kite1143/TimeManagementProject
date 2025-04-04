﻿using SQLite;
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
using Wpf.Ui.Controls;

namespace TimeManagementProject.Views
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        Page main;
        public NewTaskWindow(Page main)
        {
            InitializeComponent();
            startDatePicker.SelectedDate = DateTime.Now;
            dueDatePicker.SelectedDate = DateTime.Now;

			this.main = main;
            labelComboBox.ItemsSource = ReadTodoLabelTable();
            labelComboBox.SelectedIndex = 0;
        }

        private List<TodoLabel> ReadTodoLabelTable()
        {
            List<TodoLabel> listLabel = new List<TodoLabel>();
            listLabel.Add(new TodoLabel()
            {
                Id = -1,
                Name = "None"
            });
            using(SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
            {
                connection.CreateTable<TodoLabel>();
                listLabel.AddRange(connection.Table<TodoLabel>().ToList());
            }
            return listLabel;
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            if (String.IsNullOrWhiteSpace(titleTextBox.Text)) 
            {
                System.Windows.MessageBox.Show("Please enter title for this task");
                return;
			}


            TaskObject task = new TaskObject()
            {
                Title = titleTextBox.Text,
                Description = descriptionTextBox.Text,
                StartDate = (DateTime)startDatePicker.SelectedDate,
                DueDate = (DateTime)dueDatePicker.SelectedDate,
                isCompleted = false,
                Timer = new TimeSpan(0, 0, 0),
                Label = labelComboBox.SelectedValue.ToString()
            };

            using(SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
            {
                connection.CreateTable<TaskObject>();
                connection.Insert(task);
            }

            (main as TaskListWindow).DisplaySuccess("Your task is created successfully");

			this.Close();
		}
	}
}
