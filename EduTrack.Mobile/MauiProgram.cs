using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace EduTrack.Mobile
{
    public static class MauiProgram
    {
		
        public const string FONT_HEADING_FILE = "Poppins.ttf";
        public const string FONT_HEADING_ALIAS = "Poppins";
        public const string FONT_BODY_FILE = "Inter.ttf";
        public const string FONT_BODY_ALIAS = "Inter";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont(FONT_HEADING_FILE, FONT_HEADING_ALIAS);
                    fonts.AddFont(FONT_BODY_FILE, FONT_BODY_ALIAS);
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
        }
    }
}
