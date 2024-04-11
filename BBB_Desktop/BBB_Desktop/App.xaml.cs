using System.Configuration;
using System.Data;
using System.Windows;

namespace BBB_Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStart(object sender, StartupEventArgs e)
        {
            Window login = new Windows.LoginWindow();
            login.Show();
        }
    }

}
