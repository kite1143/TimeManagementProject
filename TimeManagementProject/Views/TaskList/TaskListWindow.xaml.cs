using SQLite;
using System.ComponentModel;
using System.Diagnostics;
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
using TimeManagementProject.Models;
using TimeManagementProject.ViewModel.Helpers;
using TimeManagementProject.Views;

namespace TimeManagementProject;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class TaskListWindow : Window
{
    List<TaskObject> listTasks;
    public TaskListWindow()
    {
        InitializeComponent();
		this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        ReadDatabase();
    }

    public void ReadDatabase()
    {
        using(SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
        {
            connection.CreateTable<TaskObject>();
            listTasks = connection.Table<TaskObject>().Where(e => e.isCompleted == false).ToList();
        }
        taskListView.ItemsSource = listTasks;
    }
	private void Button_Click(object sender, RoutedEventArgs e)
	{
        NewTaskWindow newTaskWindow = new NewTaskWindow();
		newTaskWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
		newTaskWindow.ShowDialog();
        ReadDatabase();
    }

    public void CompletedTask(TaskObject task)
    {
        task.isCompleted = true;
		using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
		{
			connection.CreateTable<TaskObject>();
            connection.Update(task);
		}
	}

	private void CheckBox_Checked(object sender, RoutedEventArgs e)
	{
		if (sender is CheckBox checkBox && checkBox.DataContext is TaskObject checkedItem)
		{
            CompletedTask(checkedItem);
		}
        ReadDatabase();
	}


	private void taskListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		TaskObject selectedTask = taskListView.SelectedItem as TaskObject;

		if (selectedTask == null)
		{
			return;
		}

		DetailTaskWindow detailTaskWindow = new DetailTaskWindow(selectedTask);
		detailTaskWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
		detailTaskWindow.ShowDialog();

		ReadDatabase();
	}

	private void Button_Timer_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button timerBtn && timerBtn.DataContext is TaskObject selectedItem)
		{
			TaskTimerWindow taskTimerWindow = new TaskTimerWindow(selectedItem);
			taskTimerWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			taskTimerWindow.ShowDialog();
			ReadDatabase();
		}
	}
}