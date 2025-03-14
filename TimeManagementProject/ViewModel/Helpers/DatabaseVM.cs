using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementProject.Models;

namespace TimeManagementProject.ViewModel.Helpers
{
    public class DatabaseVM
    {
        public static string databaseName = "TaskManager.db";
        public static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databasePath = System.IO.Path.Combine(folderPath, databaseName);

		public static Dictionary<string, TimeSpan> GetTimeSpentPerLabel(int year, int month)
		{
			Dictionary<string, TimeSpan> timeSpentPerLabel = new();

			using (SQLiteConnection connection = new SQLiteConnection(DatabaseVM.databasePath))
			{
				connection.CreateTable<TaskObject>();

				DateTime monthStart = new DateTime(year, month, 1);
				DateTime monthEnd = monthStart.AddMonths(1); // First day of next month

				var tasks = connection.Table<TaskObject>()
					.Where(t => t.StartDate >= monthStart && t.StartDate < monthEnd)
					.ToList();

				timeSpentPerLabel = tasks
					.GroupBy(t => t.Label)  // Group by Label
					.ToDictionary(
						g => g.Key ?? "None",  // Label name (default to "None" if null)
						g => g.Aggregate(TimeSpan.Zero, (total, task) => total + task.Timer) // Sum TimeSpan
					);
			}

			return timeSpentPerLabel;
		}

	}
}
