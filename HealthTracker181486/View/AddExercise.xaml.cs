namespace HealthTracker181486.View;

public partial class AddExercise : ContentPage
{
	public AddExercise()
	{
		InitializeComponent();

        
    }

    private async void OnExerciseTapped(object sender, TappedEventArgs e)
    {
        if(e.Parameter is string name)
        {
            await Shell.Current.GoToAsync($"ExerciseActive?exercise={Uri.EscapeDataString(name)}");
        }
    }
}