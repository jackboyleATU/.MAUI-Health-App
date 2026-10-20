namespace HealthTracker181486.View;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
	}

	async void OnEditClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("EditProfile");
    }

	async void OnSignOutTapped(object? sender, TappedEventArgs e)
	{
		await DisplayAlertAsync("Signed Out", "You have been signed out.", "OK");
    }
}