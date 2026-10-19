namespace HealthTracker181486.View;

public partial class ExerciseList : ContentPage
{
	public ExerciseList()
	{
		InitializeComponent();
		Routing.RegisterRoute("AddExercise", typeof(AddExercise));
    }

	private async void OnAddClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("AddExercise");
    }
}