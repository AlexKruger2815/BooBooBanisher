namespace BBB_CLI;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

public class AuthServer
{
    private readonly IWebHost _host;
    private readonly SemaphoreSlim _responseCapturedSignal;
    private Authorization? _authorization;
    private const string ClientId = "Iv1.9fb0839220db756c";
    private const string ClientSecret = "2f802c9ccc0f95543b1a2ad398fca5613d349fe4";
    private const string AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
    private const string TokenEndpoint = "https://github.com/login/oauth/access_token";

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
                        var responseContent = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;

                        _authorization = new Authorization(responseContent);
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
        Console.WriteLine($"Authorize at this URL {AuthorizationEndpoint}?client_id={ClientId}&redirect_uri=http://localhost:{port}...");
    }

    public async Task StopServerAsync()
    {
        await _host.StopAsync();
    }

    public async Task<Authorization?> GetTokenAsync()
    {
        // Wait until a response is captured
        await _responseCapturedSignal.WaitAsync();
        return _authorization;
    }
}