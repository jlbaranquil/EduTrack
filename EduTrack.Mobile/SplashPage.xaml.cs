using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace EduTrack.Mobile
{
    public partial class SplashPage : ContentPage
    {
		
        public const string APP_TITLE = "EDUTRACK";
        public const string APP_SUBTITLE = "Student Information & Mobile Directory";
        public const string LOGO_IMAGE_SOURCE = "edutrack_logo.png";
        public const int SPLASH_DISPLAY_DURATION_MS = 1400;

        public SplashPage()
        {
            InitializeComponent();

            lblSplashTitle.Text = APP_TITLE;
            lblSplashSubtitle.Text = APP_SUBTITLE;
            imgSplashLogo.Source = LOGO_IMAGE_SOURCE;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RunStartupSequenceAsync();
        }

        private async Task RunStartupSequenceAsync()
        {
            stkSplashContent.Scale = 0.9;
            var fadeTask = stkSplashContent.FadeTo(1.0, 500, Easing.CubicOut);
            var scaleTask = stkSplashContent.ScaleTo(1.0, 500, Easing.CubicOut);

            var dbCheckTask = MobileDatabase.TestConnectionAsync();

            await Task.WhenAll(fadeTask, scaleTask);
            await Task.Delay(SPLASH_DISPLAY_DURATION_MS);
            await dbCheckTask;

            if (Application.Current != null && Application.Current.Windows.Count > 0)
            {
                Application.Current.Windows[0].Page = new LoginPage();
            }
        }
    }
}
