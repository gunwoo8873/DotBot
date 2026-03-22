// Standard library
using System;
// Discord.NET library
using Discord;
using Discord.WebSocket;
using Discord.Shared;

namespace Bot;

class RunTime
{
  private static DiscordSocketClient _client = null!;
  
  private string GetToken(string t)
  {
    EnvLoader.LoadFromSolutionRelativePath("Bot/.env");
    return EnvLoader.GetRequired(t);
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