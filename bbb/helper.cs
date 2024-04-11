using Microsoft.Extensions.Primitives;

namespace bbb.Helpers;

public static class Helper
{
    public static Boolean CheckToken(IHeaderDictionary headers)
    {
        StringValues tokenbearer = "";
        try
        {

            headers.TryGetValue("Authorization", out var temp);
            tokenbearer = temp;
            tokenbearer.First();
            System.Console.WriteLine(tokenbearer + " " + tokenbearer.ToString());
            var ans =  CallGithub(tokenbearer).Result;
            System.Console.WriteLine(ans.ToString()+" is valid");
            return ans;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
    private static async Task<Boolean> CallGithub(StringValues tokenbearer)
    {
        HttpClient client = new HttpClient();
        // client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenbearer.ToString().Trim());
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1521.3 Safari/537.36");
        var response = await client.GetAsync("https://api.github.com/user");
        return response.IsSuccessStatusCode;
    }
}