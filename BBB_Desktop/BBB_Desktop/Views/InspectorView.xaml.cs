using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using BBB_Desktop.Auth;
using BBB_Desktop.Data;

namespace BBB_Desktop.Views
{
    /// <summary>
    /// Interaction logic for InspectorContol.xaml
    /// </summary>
    public partial class InspectorView : UserControl
    {
        private string filename = string.Empty;
        private string message = string.Empty;
        private string fullOutput = string.Empty;

        public InspectorView()
        {
            InitializeComponent();
        }

        private void Upload_Button_Click(object sender, RoutedEventArgs e)
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

                //message = process.ExitCode switch
                //{
                //    1 => "Uh oh, Stinky",
                //    0 => "Yippee!",
                //    _ => "Unknown: " + process.ExitCode,
                //};

                string messageType = process.ExitCode switch
                {
                    1 => "error",
                    0 => "success",
                    _ => "other",
                };
                message = BBBDBContext.GetMessageAsync(
                    (int)App.Current.Properties["userID"]!,
                    (string)App.Current.Properties["access_token"]!,
                    messageType
                    ).Result;
                fullOutput = process.StandardOutput.ReadToEnd();
            }

            txtOutput.Text = (cbxFullError.IsChecked == true) 
                ? $"{message}\n\n====================Full Output====================\n{fullOutput}" 
                :  message;
        }

        private void cbxFullError_Checked(object sender, RoutedEventArgs e)
        {
            if (message != string.Empty)
                txtOutput.Text = $"{message}\n\n====================Full Output====================\n{fullOutput}";
        }

        private void cbxFullError_Unchecked(object sender, RoutedEventArgs e)
        {
            if (message != string.Empty) 
                txtOutput.Text = message;
        }
    }
}
