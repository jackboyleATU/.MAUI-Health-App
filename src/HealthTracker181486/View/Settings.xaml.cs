namespace HealthTracker181486.View;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();

		UnitsPicker.SelectedIndex = 0;
		ThemePicker.SelectedIndex = 0;
    }

	async void OnConnectDeviceTapped(object sender, TappedEventArgs e)
	{
		await DisplayAlertAsync("Connect a device", "This feature is not yet implemented.", "OK"); 
	}

	async void OnExportTapped(object sender, TappedEventArgs e)
	{
		bool export = await DisplayAlertAsync("Export Data", "Are you sure you want to export your data?", "Yes", "No");
		if (export)
		{
			await DisplayAlertAsync("Export Data", "Your data has been exported successfully.", "OK");
		}
    }

	async void OnDeleteDataTapped(object sender, TappedEventArgs e)
	{
		bool delete = await DisplayAlertAsync("Delete Data", "Are you sure you want to delete your data? This action cannot be undone.", "Yes", "No");
		if (delete)
		{
			await DisplayAlertAsync("Delete Data", "Your data has been deleted successfully.", "OK");
		}
	}
}