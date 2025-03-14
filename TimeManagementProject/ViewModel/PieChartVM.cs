using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementProject.ViewModel.Helpers;

namespace TimeManagementProject.ViewModel
{
    public class PieChartVM
    {
		public PlotModel PieChartModel { get; private set; }

		public PieChartVM(int year, int month)
		{
			PieChartModel = new PlotModel { Title = $"Time Spent per Label - {month}/{year}" };
			LoadData(year, month);
		}

		private void LoadData(int year, int month)
		{
			Dictionary<string, TimeSpan> timeSpentPerLabel = DatabaseVM.GetTimeSpentPerLabel(year, month);

			var pieSeries = new PieSeries
			{
				StrokeThickness = 2.0,
				InsideLabelPosition = 0.5,
				AngleSpan = 360,
				StartAngle = 0
			};

			foreach (var entry in timeSpentPerLabel)
			{
				double hours = entry.Value.TotalHours; // Convert TimeSpan to hours
				if (hours > 0) // Avoid zero-value slices
				{
					pieSeries.Slices.Add(new PieSlice(entry.Key, hours));
				}
			}

			PieChartModel.Series.Clear();
			PieChartModel.Series.Add(pieSeries);
		}

	}
}
