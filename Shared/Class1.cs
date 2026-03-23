namespace Shared;

class Configure
{
    public string GetToken(string t)
    {
        // Env.Load();

        var _token = Environment.GetEnvironmentVariable(t);
        return _token ?? throw new InvalidOperationException($"Environment variable '{t}' not found.");
    }
}