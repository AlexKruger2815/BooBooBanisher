using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BBB_Desktop.Auth;
using Microsoft.AspNetCore.Components.Forms;

namespace BBB_Desktop.Controls
{
    /// <summary>
    /// Interaction logic for InspectorContol.xaml
    /// </summary>
    public partial class InspectorContol : UserControl
    {
        private string filename = string.Empty;
        private string message = string.Empty;
        private string fullOutput = string.Empty;

        public InspectorContol()
        {
            InitializeComponent();
        }

        private async void Upload_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Program"; // Default file name
            dialog.DefaultExt = ".sln"; // Default file extension
            dialog.Filter = "Program files|*.sln;*.cs"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                txtOutput.Text = string.Empty;
                message = string.Empty;
                fullOutput = string.Empty;

                // Open document
                filename = dialog.FileName;
                filename = filename.Substring(0, filename.LastIndexOf("\\"));
                Authentication? authentication;

                //NEW STUFF
                try
                {
                    using var reader = new StreamReader("token.txt");
                    authentication = new Authentication(reader.ReadToEnd());
                    if (authentication.Expired) throw new FileNotFoundException();
                }
                catch (FileNotFoundException)
                {
                    var codeServer = new AuthServer();
                    await codeServer.StartServerAsync(5000);
                    authentication = await codeServer.GetTokenAsync();
                    await codeServer.StopServerAsync();
                }

                try
                {
                    await using var writer = new StreamWriter("token.txt");
                    writer.WriteLine(authentication);
                }
                catch
                {
                    // ignored
                }

                var startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = $"/C dotnet build \"{filename}\"",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                };
                var process = new Process
                {
                    StartInfo = startInfo
                };
                process.Start();

                process.WaitForExit();

                message = process.ExitCode switch
                {
                    1 => "Uh oh, Stinky",
                    0 => "Yippee!",
                    _ => "Unknown: " + process.ExitCode,
                };

                fullOutput = process.StandardOutput.ReadToEnd();

                //Console.WriteLine("Do you want see the real response? (Y/N):");
                //var response = Console.ReadLine();
                //if (response?.ToLower() == "y" || response?.ToLower() == "yes") Console.WriteLine(process.StandardOutput.ReadToEnd());
            }

            txtOutput.Text = (cbxFullError.IsChecked == true) 
                ? $"{message}\n====================Full Output====================\n{fullOutput}" 
                :  message;
        }

        private void cbxFullError_Checked(object sender, RoutedEventArgs e)
        {
            if (message != string.Empty)
                txtOutput.Text = $"{message}\n====================Full Output====================\n{fullOutput}";
        }

        private void cbxFullError_Unchecked(object sender, RoutedEventArgs e)
        {
            if (message != string.Empty) 
                txtOutput.Text = message;
        }
    }
}
