using BBB_Desktop.Data;
using BBB_Desktop.Models;
using System.Windows;

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
            
        }
    }
}