using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Net.Http;

namespace BBB_Desktop.Auth;
public class AuthServer
{
    private readonly IWebHost _host;
    private readonly SemaphoreSlim _responseCapturedSignal;
    private Authorization? _authorization;
    private const string ClientId = "Iv1.66910d0a02a3522e";
    private const string ClientSecret = "86c52408519c38b06fdd49b3d28b03104e2e44cb";
    private const string AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
    private const string TokenEndpoint = "https://github.com/login/oauth/access_token";
    public bool IsStarted { get; private set; } = false;

    public AuthServer()
    {
        _responseCapturedSignal = new SemaphoreSlim(0);
        _host = new WebHostBuilder()
            .ConfigureServices(services => services.AddRouting())
            .Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", async context =>
                    {
                        string? code = context.Request.Query["code"];
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync("Authorized, you may navigate back");

                        using var client = new HttpClient();
                        var response = await client.GetAsync($"{TokenEndpoint}?client_secret={ClientSecret}&client_id={ClientId}&code={code}");
                        response.EnsureSuccessStatusCode();

                        var responseBody = await response.Content.ReadAsStringAsync()
                            .ConfigureAwait(false);

                        _authorization = new Authorization(responseBody);
                        _responseCapturedSignal.Release();
                    });
                });
            })
            .UseKestrel()
            .Build();
    }

    public async Task StartServerAsync(int port)
    {
        await _host.StartAsync();
        var webDirect = new ProcessStartInfo
        {
            FileName = $"{AuthorizationEndpoint}?client_id={ClientId}&redirect_uri=http://localhost:{port}",
            UseShellExecute = true
        };
        Process.Start(webDirect);
        IsStarted = true;
    }

    public async Task StopServerAsync()
    {
        await _host.StopAsync();
        IsStarted = false;
    }

    public async Task<Authorization?> GetTokenAsync()
    {
        // Wait until a response is captured
        await _responseCapturedSignal.WaitAsync();
        return _authorization;
    }
}