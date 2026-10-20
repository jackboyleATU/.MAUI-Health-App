using HealthTracker181486.View;

namespace HealthTracker181486
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Profile", typeof(Profile));
            Routing.RegisterRoute("EditProfile", typeof(EditProfile));
            Routing.RegisterRoute("Steps", typeof(StepsPage));
            Routing.RegisterRoute("Calories", typeof(CaloriesPage));
            Routing.RegisterRoute("Sleep", typeof(SleepPage));
            Routing.RegisterRoute("HeartRate", typeof(HeartRatePage));
            Routing.RegisterRoute("ExerciseList", typeof(ExerciseList));
            Routing.RegisterRoute("AddExercise", typeof(AddExercise));
            Routing.RegisterRoute("ProfilePage", typeof(Profile));
            Routing.RegisterRoute("ExerciseActive", typeof(ExerciseActive));

        }
    }
}
