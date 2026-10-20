using System.Text.RegularExpressions;

namespace HealthTracker181486.View;

public partial class EditProfile : ContentPage
{
	static readonly Color NormalStroke = Color.FromArgb("#3A3A3C");
	static readonly Color ErrorStroke = Color.FromArgb("#FF5C5C");

	public EditProfile()
	{
		InitializeComponent();

		NameEntry.Text = "John Smith";
		EmailEntry.Text = "john.smith@gmail.com";
		DobPicker.MaximumDate = DateTime.Today;
		DobPicker.Date = new DateTime(2004, 10, 19);
		SexPicker.SelectedIndex = 0;
		HeightEntry.Text = "180";
		WeightEntry.Text = "75";
	}

	void OnNameChanged(object? sender, TextChangedEventArgs e)
    {
		string name = e.NewTextValue?.Trim() ?? "";
		DisplayNameLabel.Text = name.Length > 0 ? name : "Your name";
    }

	async void OnSaveClicked(object? sender, EventArgs e)
	{
		bool nameOk = !string.IsNullOrWhiteSpace(NameEntry.Text);
		bool emailOk = Regex.IsMatch(EmailEntry.Text ?? "", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
		bool dobOk = DobPicker.Date <= DateTime.Today.AddYears(-13);
		bool sexOk = SexPicker.SelectedIndex >= 0;
		bool heightOk = double.TryParse(HeightEntry.Text, out double h) && h is >= 50 and <= 250;
		bool weightOk = double.TryParse(WeightEntry.Text, out double w) && w is >= 20 and <= 300;

		ShowError(NameBox, NameError, nameOk);
		ShowError(EmailBox, EmailError, emailOk);
		ShowError(DobBox, DobError, dobOk);
		ShowError(SexBox, SexError, sexOk);
		ShowError(HeightBox, HeightError, heightOk);
		ShowError(WeightBox, WeightError, weightOk);

		if(nameOk && emailOk && dobOk && sexOk && heightOk && weightOk)
		{
			await DisplayAlertAsync("Profile saved", "Your details have been updated", "OK");
			await Shell.Current.GoToAsync("..");
		}
    }

	async void OnCancelClicked(object? sender, EventArgs e)
    {
        bool confirm = await DisplayAlertAsync("Discard changes?", "Are you sure you want to discard your changes?", "Yes", "No");
        if (confirm)
            await Shell.Current.GoToAsync("..");
    }

    // Shows or hides the error message and changes the border color of the box based on the validity of the input.
    static void ShowError(Border box, Label error, bool isValid)
	{
		error.IsVisible = !isValid;
		box.Stroke = isValid ? NormalStroke : ErrorStroke;
    }
}