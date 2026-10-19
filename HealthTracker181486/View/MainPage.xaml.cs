using HealthTracker181486.View;

namespace HealthTracker181486
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            Routing.RegisterRoute("Steps", typeof(StepsPage));
            Routing.RegisterRoute("Calories", typeof(CaloriesPage));
            Routing.RegisterRoute("Sleep", typeof(SleepPage));
        }

        private async void OnTileTapped(object? sender, TappedEventArgs e)
        {
            if (e.Parameter is string route)
            {
                await Shell.Current.GoToAsync(route);
            }
        }

    }
}
