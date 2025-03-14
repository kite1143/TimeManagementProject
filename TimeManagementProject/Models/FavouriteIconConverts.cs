using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TimeManagementProject.Models
{
    class FavouriteIconConverts:IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? "❤" : "♡";
		}

		// Chuyển đổi ngược lại nếu cần (ở đây không cần dùng)
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.ToString() == "❤";
		}
	}
}
