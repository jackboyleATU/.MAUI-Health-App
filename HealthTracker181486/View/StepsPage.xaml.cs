namespace HealthTracker181486.View;

public partial class StepsPage : ContentPage
{
	DateTime _selectedDate = DateTime.Today;

	public StepsPage()
	{
		InitializeComponent();
		UpdateDay();
	}

	void UpdateDay()
	{
		DayLabel.Text = _selectedDate == DateTime.Today ? "Today" :
			_selectedDate == DateTime.Today.AddDays(-1) ? "Yesterday" :
            _selectedDate.ToString("dddd, MMMM dd, yyyy");
		if(_selectedDate == DateTime.Today)
		{
			NextDayButton.IsEnabled = false;
			NextDayButton.Opacity = 0.5;
		}
		else
		{
			NextDayButton.IsEnabled = true;
			NextDayButton.Opacity = 1;
		}
    }

	void OnPrevDayClicked(object? sender, EventArgs e)
    {
        _selectedDate = _selectedDate.AddDays(-1);
        UpdateDay();
    }

	void OnNextDayClicked(object? sender, EventArgs e)
    {
		if (_selectedDate >= DateTime.Today) return;
        _selectedDate = _selectedDate.AddDays(1);
        UpdateDay();
    }

	void OnMenuClicked(object? sender, EventArgs e)
    {
    }

}