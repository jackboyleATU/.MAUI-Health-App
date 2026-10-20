using HealthTracker181486.View;

namespace HealthTracker181486
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            
        }

        private async void OnTileTapped(object? sender, TappedEventArgs e)
        {
            if (e.Parameter is string route)
            {
                await Shell.Current.GoToAsync(route);
            }
        }

        private async void OnExerciseTapped(object? sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync("ExerciseActive");
        }

        private async void OnMoreTapped(object? sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync("AddExercise");
        }

        private async void OnProfileTapped(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ProfilePage");
        }
    }
}
