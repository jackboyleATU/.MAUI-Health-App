namespace HealthTracker181486.View;

public partial class Goals : ContentPage
{
	public Goals()
	{
		InitializeComponent();
	}

	async void OnGoalTapped(object sender, TappedEventArgs e)
    {
        if(e.Parameter is string goal)
		{
			await Shell.Current.GoToAsync($"SetGoal?goal={goal}");
		}
    }
}