using System.Text;

namespace BBB_CLI;

public class Authorization
{
    private readonly Dictionary<string, string?> _auth;

    public new bool Expired => long.Parse(_auth?.GetValueOrDefault("expires_at", null) ?? $"{long.MaxValue}") < DateTime.Now.ToFileTime();

    public new string? Bearer => $"{_auth?.GetValueOrDefault("access_token", null)}";

    public Authorization(string? response)
    {
        var pairs = response?.Split("&");
        var parameters = new Dictionary<string, string?>();
        foreach (var pair in pairs ?? [])
        {
            var keyValue = pair.Split("=");
            parameters[keyValue[0]] = keyValue.Length > 1 ? keyValue[1].Trim() : null;
        }
        _auth = parameters;
    }

    public override string ToString()
    {
        
        var sb = new StringBuilder();

        foreach (var pair in _auth)
        {
            sb.Append(pair.Key);
            sb.Append('=');
            sb.Append(pair.Value);
            if (!pair.Equals(_auth.Last()))
            {
                sb.Append('&');
            }
        }

        return sb.ToString();
    }
}