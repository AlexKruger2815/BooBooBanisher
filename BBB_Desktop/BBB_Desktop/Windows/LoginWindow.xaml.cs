using BBB_Desktop.Auth;
using System.Windows;

namespace BBB_Desktop.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private bool loginSuccessful = false;
        private AuthServer codeServer = new();
        public LoginWindow()
        {
            InitializeComponent();
            this.Closing += LoginWindow_Closing;
        }
        protected void LoginWindow_Closing(object? sender, EventArgs e)
        {
            if (loginSuccessful)
            {
                MainWindow mainWindow = new MainWindow();
                Application.Current.MainWindow = mainWindow;
                mainWindow.Show();
            }
        }

        private async void Authorize_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            Authorization? authorization;

            if (!codeServer.IsStarted) await codeServer.StartServerAsync(5000);
            authorization = await codeServer.GetTokenAsync();
            await codeServer.StopServerAsync();

            if (authorization != null && !String.IsNullOrEmpty(authorization.Bearer))
            {
                App.Current.Properties["access_token"] = authorization.Bearer;
                loginSuccessful = true;
                this.Close();
            }

        }
    }
}
