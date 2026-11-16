using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace EduTrack.Mobile
{
    public partial class LoginPage : ContentPage
    {
		
        public const string TEXT_AUTH_BADGE = "🔒 AUTHENTICATION";
        public const string TEXT_WELCOME_TITLE = "WELCOME BACK";
        public const string TEXT_WELCOME_SUBTITLE = "Sign in to manage student directory records";
        public const string TEXT_USERNAME_LABEL = "👤 USERNAME";
        public const string TEXT_PASSWORD_LABEL = "🔑 PASSWORD";
        public const string TEXT_SHOW_PASSWORD = "Show Password";
        public const string TEXT_SIGN_IN_BTN = "➔  SIGN IN";
        public const string TEXT_CLOSE_APP_BTN = "✖  CLOSE APPLICATION";

        public const string DEFAULT_USERNAME = "admin";
        public const string DEFAULT_PASSWORD = "admin123";
        public const bool DEFAULT_SHOW_PASSWORD_CHECKED = true;

        public static readonly Color STATUS_CONNECTED_COLOR = Color.FromArgb("#10B981");
        public static readonly Color STATUS_OFFLINE_COLOR = Color.FromArgb("#F43F5E");

        private readonly MobileStudentCRUD crud = new MobileStudentCRUD();
        private bool isSigningIn = false;

        public LoginPage()
        {
            InitializeComponent();
            ApplyConfiguration();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await UpdateDatabaseStatusBadgeAsync();
        }

        private void ApplyConfiguration()
        {
            lblAuthBadge.Text = TEXT_AUTH_BADGE;
            lblWelcomeTitle.Text = TEXT_WELCOME_TITLE;
            lblWelcomeSubtitle.Text = TEXT_WELCOME_SUBTITLE;
            lblUsernameHeader.Text = TEXT_USERNAME_LABEL;
            lblPasswordHeader.Text = TEXT_PASSWORD_LABEL;
            lblShowPassword.Text = TEXT_SHOW_PASSWORD;
            btnSignIn.Text = TEXT_SIGN_IN_BTN;
            btnCloseApp.Text = TEXT_CLOSE_APP_BTN;

            txtUsername.Text = DEFAULT_USERNAME;
            txtPassword.Text = DEFAULT_PASSWORD;
            chkShowPassword.IsChecked = DEFAULT_SHOW_PASSWORD_CHECKED;
            txtPassword.IsPassword = !DEFAULT_SHOW_PASSWORD_CHECKED;
        }

        private async Task UpdateDatabaseStatusBadgeAsync()
        {
            bool connected = await MobileDatabase.TestConnectionAsync();
            if (connected)
            {
                lblDbDot.TextColor = STATUS_CONNECTED_COLOR;
                lblDbStatusText.TextColor = STATUS_CONNECTED_COLOR;
                lblDbStatusText.Text = $"MySQL Status: Connected ({MobileDatabase.MYSQL_HOST})";
            }
            else
            {
                lblDbDot.TextColor = STATUS_OFFLINE_COLOR;
                lblDbStatusText.TextColor = STATUS_OFFLINE_COLOR;
                lblDbStatusText.Text = $"MySQL Status: Offline ({MobileDatabase.MYSQL_HOST})";
            }
        }

        private void chkShowPassword_CheckedChanged(object? sender, CheckedChangedEventArgs e)
        {
            txtPassword.IsPassword = !e.Value;
        }

        private void lblShowPassword_Tapped(object? sender, TappedEventArgs e)
        {
            chkShowPassword.IsChecked = !chkShowPassword.IsChecked;
        }

        private async void btnSignIn_Clicked(object? sender, EventArgs e)
        {
            if (isSigningIn) return;

            string username = txtUsername.Text?.Trim() ?? string.Empty;
            string password = txtPassword.Text?.Trim() ?? string.Empty;

            if (!await MobileErrorHandler.ValidateLoginInputsAsync(this, username, password))
            {
                return;
            }

            isSigningIn = true;
            btnSignIn.IsEnabled = false;
            btnSignIn.Text = "Signing In...";

            try
            {
                bool isValid = await crud.AuthenticateUserAsync(username, password, this);
                if (isValid)
                {
                    if (Application.Current != null && Application.Current.Windows.Count > 0)
                    {
                        Application.Current.Windows[0].Page = new MainPage();
                    }
                }
                else
                {
                    await DisplayAlert(
                        "Authentication Failed",
                        "Invalid username or password. Please try again.",
                        "OK");
                }
            }
            finally
            {
                isSigningIn = false;
                btnSignIn.IsEnabled = true;
                btnSignIn.Text = TEXT_SIGN_IN_BTN;
            }
        }

        private void btnCloseApp_Clicked(object? sender, EventArgs e)
        {
            Application.Current?.Quit();
        }
    }
}
