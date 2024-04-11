using BBB_Desktop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BBB_Desktop.Data;

public static class BBBDBContext
{
    private static string baseAddress = "http://booboobanisher.eba-btqxcacw.eu-west-1.elasticbeanstalk.com/";
    private static HttpClient client = new HttpClient();

    public static async Task<UserModel> GetUserAsync(string token)
    {
        try
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{baseAddress}user")
            };
            request.Headers.Add("Authorization", token);

            var response = await client.SendAsync(request)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            return JsonSerializer.Deserialize<UserModel[]>(responseBody)![0];
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return new UserModel();
    }

    public static async Task<string> GetMessageAsync(int userID, string token, string messageType)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{baseAddress}message/{messageType}?userID={userID}")
        };
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request)
            .ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync()
            .ConfigureAwait(false);

        return responseBody;
    }

    public static async Task<StatsModel[]> GetStatsAsync(int userID, string token, DateTime? start = null, DateTime? end = null)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{baseAddress}session/stats?userID={userID}");
        if (start.HasValue) sb.Append($"&startdate={start}");
        if (end.HasValue) sb.Append($"&enddate={end}");
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(sb.ToString()),
        };
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request)
            .ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync()
            .ConfigureAwait(false);

        return JsonSerializer.Deserialize<StatsModel[]>(responseBody)!;
    }
}
