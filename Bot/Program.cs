// Standard library
using System;
// Discord.NET library
using Discord;
using Discord.WebSocket;
// Read Env library
using DotNetEnv;

namespace Bot;

class RunTime
{
  private static DiscordSocketClient _client;
  
  private string GetToken(string t)
  {
    Env.Load();

    var _token = Environment.GetEnvironmentVariable(t);
    if (string.IsNullOrEmpty(_token))
    {
      Console.WriteLine($"Error: {t} is not set in environment variables.");
      Environment.Exit(1);
    }

    return _token;
  }

  private static Task Log(LogMessage msg)
  {
    Console.WriteLine(msg.ToString());
    return Task.CompletedTask;
  }

  static async Task Main()
  {
    var token = new RunTime().GetToken("DISCORD_CLIENT_TOKEN");

    _client = new DiscordSocketClient();
    _client.Log += Log;


    await _client.LoginAsync(TokenType.Bot, token);
    await _client.StartAsync();

    await Task.Delay(-1);
  }
}