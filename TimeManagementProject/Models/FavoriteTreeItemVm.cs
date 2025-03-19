using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementProject.Models
{
    public class FavoriteTreeItemVm
    {
		public TodoLabel todoLabel { get; set; }
		public List<TaskObject> listTask { get; set; }
		public FavoriteTreeItemVm(TodoLabel label, List<TaskObject> listTask)
		{
			todoLabel = label;
			this.listTask = listTask;
		}
	}
}
