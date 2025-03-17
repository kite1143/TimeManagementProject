using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_CNPM.Model
{
    public class TodoLabel
    {
		[PrimaryKey]
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsFavorite { get; set; }
		public override string ToString()
		{
			return Name ;
		}
	}
}
