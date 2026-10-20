namespace HealthTracker181486.View;

public partial class AddExercise : ContentPage
{
	public AddExercise()
	{
		InitializeComponent();

        
    }

    private async void OnExerciseTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("ExerciseActive");
    }
}