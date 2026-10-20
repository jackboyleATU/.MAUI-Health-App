namespace HealthTracker181486.View;

public partial class ExerciseList : ContentPage
{
	public ExerciseList()
	{
		InitializeComponent();
		
    }

	private async void OnAddClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("AddExercise");
    }
}