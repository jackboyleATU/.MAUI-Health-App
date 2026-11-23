using System.Diagnostics;

namespace HealthTracker181486.View;

[QueryProperty(nameof(ExerciseName), "exercise")]
public partial class ExerciseActive : ContentPage
{
    readonly Stopwatch _stopwatch = new();
    readonly IDispatcherTimer _timer;

    public string ExerciseName
    {
        set => TitleLabel.Text = value;
    }

	public ExerciseActive()
	{
		InitializeComponent();

        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(50);
        _timer.Tick += (_, _) => UpdateDisplay();
	}

    void UpdateDisplay()
    {
        var t = _stopwatch.Elapsed;
        TimeLabel.Text = t.TotalHours >= 1 ? t.ToString(@"h\:mm\:ss") : t.ToString(@"mm\:ss");
        HundredthsLabel.Text = "." + (t.Milliseconds / 10).ToString("00");
    }

    void OnStartPauseClicked(object sender, EventArgs e)
    {
        if (_stopwatch.IsRunning)
        {
            _stopwatch.Stop();
            _timer.Stop();
            StartPauseButton.Text = "Resume";
            StartPauseButton.BackgroundColor = Color.FromArgb("#4F9249");
        }
        else
        {
            _stopwatch.Start();
            _timer.Start();
            StartPauseButton.Text = "Pause";
            StartPauseButton.BackgroundColor = Color.FromArgb("#DB6954");
            ResetButton.IsEnabled = true;
            ResetButton.Opacity = 1;
        }
    }

	void OnResetClicked(object sender, EventArgs e)
    {
        _stopwatch.Reset();
        _timer.Stop();
        UpdateDisplay();
        StartPauseButton.Text = "Start";
        StartPauseButton.BackgroundColor = Color.FromArgb("#4F9249");
        ResetButton.IsEnabled = false;
        ResetButton.Opacity = 0.4;
    }

    async void OnFinishClicked(object sender, EventArgs e)
    {
        if (_stopwatch.Elapsed < TimeSpan.FromSeconds(10))
        {
            await DisplayAlertAsync("Error", "Please record at least 10 seconds of exercise before finishing.", "OK");
            return;
        }

        _stopwatch.Stop();
        _timer.Stop();
        await DisplayAlertAsync("Exercise Finished", $"You exercised for {_stopwatch.Elapsed:mm\\:ss\\.ff}.", "OK");
        await Shell.Current.GoToAsync("..");
    }

    protected override void OnDisappearing()
    {

        base.OnDisappearing();
        _timer.Stop();
    }
}