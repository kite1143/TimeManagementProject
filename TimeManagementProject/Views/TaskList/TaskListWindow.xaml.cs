using BTL_CNPM.Model;
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
	String filterLabel = "All";
	DateTime filterDate = DateTime.Now;
    public TaskListWindow()
    {
        InitializeComponent();
		this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        ReadTaskDatabase();
		ReadLabelDatabase();
    }

	public void ReadLabelDatabase()
	{
		
		List<String> labelList = new List<String>()
		{
			"All"
		};

		using(SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
		{
			connection.CreateTable<TodoLabel>();
			foreach(TodoLabel tl in connection.Table<TodoLabel>())
			{
				labelList.Add(tl.Name);
			}
		}

		labelFilterComboBox.ItemsSource = labelList;
		labelFilterComboBox.SelectedIndex = 0;
	}


    public void ReadTaskDatabase()
    {
		bool isFilterByDate = (bool) filterByDateCheckbox.IsChecked;
		if (!isFilterByDate)
		{
			if (filterLabel.Equals("All"))
			{
				using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
				{
					connection.CreateTable<TaskObject>();
					listTasks = connection.Table<TaskObject>().Where(e => e.isCompleted == false).ToList();
				}
			}
			else
			{
				using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
				{
					connection.CreateTable<TaskObject>();
					listTasks = connection.Table<TaskObject>()
						.Where(e => e.isCompleted == false)
						.Where(e => e.Label.Equals(filterLabel))
						.ToList();
				}
			}
		} else
		{
			DateTime startDate = filterDate.Date;
			DateTime endDate = startDate.AddDays(1);

			if (filterLabel.Equals("All"))
			{
				using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
				{
					connection.CreateTable<TaskObject>();
					listTasks = connection.Table<TaskObject>()
						.Where(e => e.isCompleted == false)
						.Where(e => e.DueDate >= startDate && e.DueDate < endDate)
						.ToList();
				}
			}
			else
			{
				using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
				{
					connection.CreateTable<TaskObject>();
					listTasks = connection.Table<TaskObject>()
						.Where(e => e.isCompleted == false)
						.Where(e => e.Label.Equals(filterLabel))
						.Where(e => e.DueDate >= startDate && e.DueDate < endDate)
						.ToList();
				}
			}
		}
		taskListView.ItemsSource = listTasks;
	}
	private void Button_Click(object sender, RoutedEventArgs e)
	{
        NewTaskWindow newTaskWindow = new NewTaskWindow();
		newTaskWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
		newTaskWindow.ShowDialog();
        ReadTaskDatabase();
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
        ReadTaskDatabase();
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

		ReadTaskDatabase();
	}

	private void Button_Timer_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button timerBtn && timerBtn.DataContext is TaskObject selectedItem)
		{
			TaskTimerWindow taskTimerWindow = new TaskTimerWindow(selectedItem);
			taskTimerWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			taskTimerWindow.ShowDialog();
			ReadTaskDatabase();
		}
	}

	private void labelFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		filterLabel = labelFilterComboBox.SelectedItem.ToString();
		ReadTaskDatabase();
	}

	private void dateFilterCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
	{
		filterDate = (DateTime) dateFilterCalendar.SelectedDate;
		ReadTaskDatabase();
	}

	private void filterByDateCheckbox_Checked_And_UnChecked(object sender, RoutedEventArgs e)
	{
		ReadTaskDatabase();
	}
}