using System.Diagnostics;
using BBB_CLI;


if (args.Length < 1) throw new ArgumentException("Missing required position argument 1: compiler");
Authorization? authorization;
try
{
    using var reader = new StreamReader("token.txt");
    authorization = new Authorization(reader.ReadToEnd());
    if (authorization.Expired) throw new FileNotFoundException();
}
catch (FileNotFoundException)
{
    var codeServer = new AuthServer();
    await codeServer.StartServerAsync(5000);
    authorization = await codeServer.GetTokenAsync();
    await codeServer.StopServerAsync();
}
try
{
    await using var writer = new StreamWriter("token.txt");
    writer.WriteLine(authorization);
}
catch
{
    // ignored
}

var startInfo = new ProcessStartInfo
{
    WindowStyle = ProcessWindowStyle.Hidden,
    FileName = args[0],
    Arguments = string.Join(" ",args[1..]),
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
Console.WriteLine(
    process.ExitCode switch
    {
        1 => "Uh oh, Stinky",
        0 => "Yippee!",
        _ => "Unknown: " + process.ExitCode,
    });
Console.WriteLine("Do you want see the real response? (Y/N):");
var response = Console.ReadLine();
if (response?.ToLower() == "y" || response?.ToLower() == "yes") Console.WriteLine(process.StandardOutput.ReadToEnd());