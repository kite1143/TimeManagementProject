using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementProject.Models
{
    public class TodoLabel
    {
		[PrimaryKey,AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsFavorite { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}
}
