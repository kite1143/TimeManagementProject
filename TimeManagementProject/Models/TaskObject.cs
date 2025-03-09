using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementProject.Models
{
    public class TaskObject
    {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime? DueDate { get; set; }
		public DateTime? StartDate { get; set; }
		public bool isCompleted { get; set; }
		public TimeSpan Timer { get; set; }
		public string Label { get; set; }
		public TaskObject()
		{
			DueDate = System.DateTime.Now;
			StartDate = System.DateTime.Now;
			Label = "None";
		}

	}
}
