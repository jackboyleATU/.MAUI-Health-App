namespace HealthTracker181486.View;

public partial class HeartRatePage : ContentPage
{
    Border[] _tabs = default!;
    Label[] _tabLabels = default!;

    public HeartRatePage()
	{
		InitializeComponent();
		_tabs = new[] { HoursTab, DaysTab, WeeksTab, MonthsTab };
        _tabLabels = new[] { HoursLabel, DaysLabel, WeeksLabel, MonthsLabel };
    }

	void OnTabTapped(object? sender, TappedEventArgs e)
    {
        if (e.Parameter is string tab && int.TryParse(tab, out int index))
        {
            SelectTab(index);
        }
    }   

    void SelectTab(int index)
    {
        for (int i = 0; i < _tabs.Length; i++)
        {
            bool isSelected = (i == index);
            _tabs[i].BackgroundColor = isSelected ? Color.FromArgb("#3A3A3C") : Color.FromArgb("#01000000");
            _tabLabels[i].TextColor = isSelected ? Colors.White : Color.FromArgb("#8E8E93");
        }
    }


}