using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json; 

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

using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorization?.Bearer ?? string.Empty);
var login = client.GetAsync("http://booboobanisher.eba-btqxcacw.eu-west-1.elasticbeanstalk.com/user")
    .ContinueWith(previous => previous.Result.Content.ReadAsStringAsync().Result);
var user = JsonSerializer.Deserialize<User[]>(await login)?[0];

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
        1 => await Error(),
        0 => await Success(),
        _ => "Unknown: " + process.ExitCode,
    });

Console.WriteLine("Do you want see the real response? (Y/N):");
var response = Console.ReadLine();
if (response?.ToLower() == "y" || response?.ToLower() == "yes") Console.WriteLine(process.StandardOutput.ReadToEnd());
return;

Task<string> Error() =>
    client.GetAsync($"http://booboobanisher.eba-btqxcacw.eu-west-1.elasticbeanstalk.com/Message/error?userID={user?.userID}")
        .ContinueWith(previous => previous.Result.Content.ReadAsStringAsync().Result);

Task<string> Success() =>
    client.GetAsync($"http://booboobanisher.eba-btqxcacw.eu-west-1.elasticbeanstalk.com/Message/success?userID={user?.userID}")
        .ContinueWith(previous => previous.Result.Content.ReadAsStringAsync().Result);
