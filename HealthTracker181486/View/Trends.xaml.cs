
using HealthTracker181486.Resources.Services;

namespace HealthTracker181486.View;
public partial class Trends : ContentPage
{
	static readonly Color Selected = Color.FromArgb("#3A3A3C");
	static readonly Color Unselected = Color.FromArgb("#01000000");
	static readonly Color Grey = Color.FromArgb("#8E8E93");

	static readonly string[] PeriodText =
	{
		"This week vs last week",
		"This month vs last month",
		"This year vs last year",
	};

	static readonly int[][] MetricData =
	{
		new[] { 4200, 6800, 3100, 8900, 5200, 7400, 5240 },   // Steps
        new[] { 420, 610, 380, 720, 450, 560, 300 },          // Calories
        new[] { 410, 455, 390, 470, 430, 520, 425 },          // Sleep
	};

    static readonly Color[] MetricColors =
	{
		Color.FromArgb("#44D211"),   // Steps: green
		Color.FromArgb("#D10000"),   // Calories: red
		Color.FromArgb("#7B61FF"),   // Sleep: purple
	};
    static readonly string[] MetricNames = { "Steps", "Calories burned", "Sleep" };

	Border[] _rangeTabs = default!;
	Label[] _rangeLabels = default!;
	Border[] _pills = default!;
	Label[] _pillLabels = default!;


	public Trends()
	{
		InitializeComponent();
		_rangeTabs = new[] { WeekTab, MonthTab, YearTab };
		_rangeLabels = new[] { WeekLabel, MonthLabel, YearLabel };
		_pills = new[] { StepsPill, CaloriesPill, SleepPill };
		_pillLabels = new[] { StepsPillLabel, CaloriesPillLabel, SleepPillLabel };
    }

	void OnRangeTapped(object sender, TappedEventArgs e)
	{
		if (e.Parameter is string p && int.TryParse(p, out int index))
		{
			Highlight(_rangeTabs, _rangeLabels, index);
			PeriodLabel.Text = PeriodText[index];
		}
	}

	void OnMetricTapped(object sender, TappedEventArgs e)
	{
		if (e.Parameter is string p && int.TryParse(p, out int index))
		{
			Highlight(_pills, _pillLabels, index);
			ChartTitle.Text = MetricNames[index];
			var drawable = (TrendDrawable)TrendChart.Drawable;
			drawable.Data = MetricData[index];
			drawable.BarColor = MetricColors[index];
			TrendChart.Invalidate();
		}
	}

    static void Highlight(Border[] borders, Label[] labels, int index)
	{
		for (int i = 0; i < borders.Length; i++)
		{
			bool on = i == index;
			borders[i].BackgroundColor = on ? Selected : Unselected;
			labels[i].TextColor = on ? Colors.White: Grey;
        }
	}
}