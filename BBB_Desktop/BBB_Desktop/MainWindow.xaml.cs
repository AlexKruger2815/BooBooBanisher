using BBB_Desktop.Data;
using BBB_Desktop.Models;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace BBB_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private BBBDBContext context = new BBBDBContext();
        public UserModel? user;

        public MainWindow()
        {
            InitializeComponent();
            user = BBBDBContext.GetUserAsync((string)App.Current.Properties["access_token"]!).Result;
            lblUsername.Content = $"Hello, {user.username}!";
            App.Current.Properties["userID"] = user.userID;
            repoLink.RequestNavigate += Hyperlink_RequestNavigate;

        }
 
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var webDirect = new ProcessStartInfo
            {
                FileName = $"{e.Uri.ToString()}",
                UseShellExecute = true
            };
            Process.Start(webDirect);
        }
        
    }
}