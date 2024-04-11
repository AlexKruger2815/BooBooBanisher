using BBB_Desktop.Data;
using BBB_Desktop.Models;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BBB_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private BBBDBContext context = new BBBDBContext();
        public UserModel? user;
        private string? access_token = GetToken();

        public MainWindow()
        {
            InitializeComponent();
            user = BBBDBContext.GetUserAsync(access_token).Result;
            lblUsername.Content = $"Hello, {user.username}!";
            App.Current.Properties["userID"] = user.userID;
            App.Current.Properties["access_token"] = access_token;
        }

        private static string GetToken()
        {
            var pairs = new StreamReader("token.txt").ReadToEnd()?.Split("&");
            foreach (var pair in pairs ?? [])
            {
                var keyValue = pair.Split("=");
                if (keyValue[0] == "access_token")
                {
                    return keyValue[1];
                }
            }
            return string.Empty;
        }
    }
}