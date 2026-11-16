using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace EduTrack.Mobile
{
    public static class ThemeManager
    {

        public const bool DEFAULT_DARK_MODE = false;
        public const string ICON_DARK_MODE = "🌙";
        public const string ICON_LIGHT_MODE = "☀️";

        public static readonly Color LIGHT_PAGE_BG = Color.FromArgb("#F1F5F9");
        public static readonly Color LIGHT_LOGIN_BG = Color.FromArgb("#FFFFFF");
        public static readonly Color LIGHT_CARD_BG = Color.FromArgb("#FFFFFF");
        public static readonly Color LIGHT_CARD_BORDER = Color.FromArgb("#E2E8F0");
        public static readonly Color LIGHT_INPUT_BG = Color.FromArgb("#F8FAFC");
        public static readonly Color LIGHT_INPUT_BORDER = Color.FromArgb("#CBD5E1");
        public static readonly Color LIGHT_TEXT_PRIMARY = Color.FromArgb("#0F172A");
        public static readonly Color LIGHT_TEXT_SECONDARY = Color.FromArgb("#475569");
        public static readonly Color LIGHT_TEXT_MUTED = Color.FromArgb("#94A3B8");
        public static readonly Color LIGHT_AVATAR_BG = Color.FromArgb("#DBEAFE");
        public static readonly Color LIGHT_AVATAR_BORDER = Color.FromArgb("#BFDBFE");
        public static readonly Color LIGHT_AVATAR_TEXT = Color.FromArgb("#2563EB");

        public static readonly Color DARK_PAGE_BG = Color.FromArgb("#0B1120");
        public static readonly Color DARK_LOGIN_BG = Color.FromArgb("#0F172A");
        public static readonly Color DARK_CARD_BG = Color.FromArgb("#1E293B");
        public static readonly Color DARK_CARD_BORDER = Color.FromArgb("#334155");
        public static readonly Color DARK_INPUT_BG = Color.FromArgb("#0F172A");
        public static readonly Color DARK_INPUT_BORDER = Color.FromArgb("#475569");
        public static readonly Color DARK_TEXT_PRIMARY = Color.FromArgb("#F8FAFC");
        public static readonly Color DARK_TEXT_SECONDARY = Color.FromArgb("#CBD5E1");
        public static readonly Color DARK_TEXT_MUTED = Color.FromArgb("#64748B");
        public static readonly Color DARK_AVATAR_BG = Color.FromArgb("#1E3A8A");
        public static readonly Color DARK_AVATAR_BORDER = Color.FromArgb("#2563EB");
        public static readonly Color DARK_AVATAR_TEXT = Color.FromArgb("#93C5FD");

        public static bool IsDarkMode { get; private set; } = DEFAULT_DARK_MODE;

        public static string CurrentThemeButtonIcon => IsDarkMode ? ICON_LIGHT_MODE : ICON_DARK_MODE;

        public static void InitializeDefaultTheme()
        {
            ApplyTheme(DEFAULT_DARK_MODE);
        }

        public static void ToggleTheme()
        {
            ApplyTheme(!IsDarkMode);
        }

        public static void ApplyTheme(bool darkMode)
        {
            IsDarkMode = darkMode;

            if (Application.Current == null) return;

            Application.Current.UserAppTheme = darkMode ? AppTheme.Dark : AppTheme.Light;
            var res = Application.Current.Resources;

            res["PageBackgroundColor"] = darkMode ? DARK_PAGE_BG : LIGHT_PAGE_BG;
            res["LoginBackgroundColor"] = darkMode ? DARK_LOGIN_BG : LIGHT_LOGIN_BG;
            res["CardBackgroundColor"] = darkMode ? DARK_CARD_BG : LIGHT_CARD_BG;
            res["CardBorderColor"] = darkMode ? DARK_CARD_BORDER : LIGHT_CARD_BORDER;
            res["InputBackgroundColor"] = darkMode ? DARK_INPUT_BG : LIGHT_INPUT_BG;
            res["InputBorderColor"] = darkMode ? DARK_INPUT_BORDER : LIGHT_INPUT_BORDER;
            res["PrimaryTextColor"] = darkMode ? DARK_TEXT_PRIMARY : LIGHT_TEXT_PRIMARY;
            res["SecondaryTextColor"] = darkMode ? DARK_TEXT_SECONDARY : LIGHT_TEXT_SECONDARY;
            res["MutedTextColor"] = darkMode ? DARK_TEXT_MUTED : LIGHT_TEXT_MUTED;
            res["AvatarBackgroundColor"] = darkMode ? DARK_AVATAR_BG : LIGHT_AVATAR_BG;
            res["AvatarBorderColor"] = darkMode ? DARK_AVATAR_BORDER : LIGHT_AVATAR_BORDER;
            res["AvatarTextColor"] = darkMode ? DARK_AVATAR_TEXT : LIGHT_AVATAR_TEXT;
        }
    }
}
