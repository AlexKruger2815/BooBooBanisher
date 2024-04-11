using System.Diagnostics;
using System.Net.Http.Headers;
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
    FileName = "cmd.exe",
    Arguments = $"/C dotnet build \"{args[0]}\"",
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
mainAsync();
Console.WriteLine("Do you want see the real response? (Y/N):");
var response = Console.ReadLine();
if (response?.ToLower() == "y" || response?.ToLower() == "yes") Console.WriteLine(process.StandardOutput.ReadToEnd());

async Task mainAsync()
{
    using var client = new HttpClient();
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorization?.Bearer ?? string.Empty);
    System.Console.WriteLine(    client.DefaultRequestHeaders.Authorization);
    var webResponse = await client.GetAsync("http://booboobanisher.eba-btqxcacw.eu-west-1.elasticbeanstalk.com/Category");
    System.Console.WriteLine(webResponse.StatusCode);
    var responseContent = webResponse.IsSuccessStatusCode ? await webResponse.Content.ReadAsStringAsync() : null;
    System.Console.WriteLine(responseContent);
    Console.WriteLine(responseContent);
}