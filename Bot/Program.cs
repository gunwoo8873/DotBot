// Standard library
using System;
using System.Net.Sockets;
// Discord.NET library
using Discord;
using Discord.Commands;
using Discord.WebSocket;
// Read to Environment Variables library
using DotNetEnv;

// using Discord.Shared;

namespace Bot;

class Configure
{
  public string GetToken(string t)
  {
    Env.Load();

    var _token = Environment.GetEnvironmentVariable(t);
    return _token ?? throw new InvalidOperationException($"Environment variable '{t}' not found.");
  }

  public string GetChannelTopic(ulong id)
  {
    id = ulong.Parse(GetToken("DISCORD_CHANNEL_ID"));

    var channel = Sockets._client.GetChannel(id) as SocketTextChannel;
    return channel?.Topic ?? "No topic set.";
  }

  public SocketGuildUser GetGuildOwner(ulong id)
  {
    var guild = Sockets._client.GetGuild(id);
    return guild?.Owner ?? throw new InvalidOperationException($"Guild with ID '{id}' not found.");
  }
}

class Sockets
{
  public static DiscordSocketClient _client = null!;
}

class Services
{
  public Services(DiscordSocketClient c, CommandService s)
  {
    c.Log += LogAsync;
    s.Log += LogAsync;
  }

  private Task LogAsync(LogMessage msg)
  {
    if (msg.Exception is CommandException cmdEx)
    {
      Console.WriteLine($"""
      [Command/{msg.Severity}] {cmdEx.Command.Aliases.First()}
      failed to execute in {cmdEx.Context.Channel}
      """);
    }
    else
    {
      Console.WriteLine($"[General/{msg.Severity}] {msg}");
    }

    return Task.CompletedTask;
  }
}

class RunTime
{
  private static Task Log(LogMessage msg)
  {
    Console.WriteLine(msg.ToString());
    return Task.CompletedTask;
  }

  private static async Task MessageUpdate(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
  {
    var message = await before.GetOrDownloadAsync();
    Console.WriteLine($"{message} -> {after}");
  }

  static async Task Main(string[] args)
  {
    var token = new Configure().GetToken("DISCORD_CLIENT_TOKEN");

    Sockets._client = new DiscordSocketClient();
    Sockets._client.Log += Log;

    var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
    Sockets._client = new DiscordSocketClient(_config);

    await Sockets._client.LoginAsync(TokenType.Bot, token);
    await Sockets._client.StartAsync();

    Sockets._client.MessageUpdated += MessageUpdate;
    //// Warring: Terminal inside output if Bot Token, but it is for testing purpose only.
    Sockets._client.Ready += () => 
    {
        Console.WriteLine($"""
        Bot is connected!
        {token}
        """);
        return Task.CompletedTask;
    };

    await Task.Delay(-1);
  }
}