using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementProject.ViewModel
{
	public class TimerDisplayVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private string _timerString;

		public TimerDisplayVM(string timerString)
		{
			TimerString = timerString;
		}

		public string TimerString
		{
			get { return _timerString; }
			set 
			{ 
				_timerString = value;
				OnPropertyChanged();
			}
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
