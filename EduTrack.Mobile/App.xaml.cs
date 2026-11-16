using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace EduTrack.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ThemeManager.InitializeDefaultTheme();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new SplashPage())
            {
                Title = "EduTrack Mobile"
            };
        }
    }
}
