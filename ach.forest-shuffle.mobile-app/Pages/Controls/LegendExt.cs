using Syncfusion.Maui.Toolkit.Charts;

namespace ach.forest_shuffle.mobile_app.Pages.Controls;

public class LegendExt : ChartLegend
{
	protected override double GetMaximumSizeCoefficient()
	{
		return 0.5;
	}
}
