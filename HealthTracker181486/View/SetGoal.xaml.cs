namespace HealthTracker181486.View;

[QueryProperty(nameof(GoalName), "goal")]
public partial class SetGoal : ContentPage
{
	static readonly Dictionary<string, (string Unit, int Min, int Max, int Current)> Rules = new()
	{
		["Steps"] = ("steps", 1000, 50000, 6000),
		["Calories"] = ("calories", 100, 3000, 600),
		["Sleep"] = ("hours", 1, 12, 7),
	};

    static readonly Color NormalStroke = Color.FromArgb("#3A3A3C");
    static readonly Color ErrorStroke = Color.FromArgb("#FF5C5C");

    public SetGoal()
	{
		InitializeComponent();
	}

	public string GoalName
	{
		set
		{
			TypePicker.SelectedItem = value;
			Title = $"Edit {value.ToLower()} goal";
			TargetEntry.Text = Rules[value].Current.ToString();
		}
	}

	void OnTypeChanged(object sender, EventArgs e)
	{
		if (TypePicker.SelectedItem is not string type) return;
		var rule = Rules[type];
		UnitLabel.Text = rule.Unit;
		HintLabel.Text = $"Between {rule.Min} and {rule.Max} {rule.Unit}";
    }

	async void OnSaveClicked(object sender, EventArgs e)
	{
		bool typeOk = TypePicker.SelectedItem is string;
		TypeError.IsVisible = !typeOk;
		TypeBox.Stroke = typeOk ? NormalStroke : ErrorStroke;
		if (!typeOk)
		{
			TargetError.IsVisible = false;
			return;
		}

		var type = (string)TypePicker.SelectedItem!;
		var rule = Rules[type];
		string? error = null;

		if (string.IsNullOrWhiteSpace(TargetEntry.Text))
			error = "Please enter a target";
		else if (!int.TryParse(TargetEntry.Text, out int target))
			error = "Target must be a whole number";
		else if (target < rule.Min || target > rule.Max)
			error = $"Target must be between {rule.Min} and {rule.Max} {rule.Unit}";

		TargetError.Text = error;
		TargetError.IsVisible = error is not null;
		TargetBox.Stroke = error is null ? NormalStroke : ErrorStroke;

		if (error is null)
		{
			await DisplayAlertAsync("Goal saved", $"{type} goal set to {TargetEntry.Text} {rule.Unit}.", "OK");
			await Shell.Current.GoToAsync("..");
		}
	}

	void OnCancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}