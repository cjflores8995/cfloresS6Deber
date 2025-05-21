using cfloresS6Jueves.Views;

namespace cfloresS6Jueves
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new vCrud()));
        }
    }
}