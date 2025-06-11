
using Microsoft.Extensions.Hosting;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace BirthdayBot.Services
{
  public class BotService : IHostedService
  {
    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _config;

    public BotService(IConfiguration config)
    {
      _config = config;
      _client = new DiscordSocketClient(new DiscordSocketConfig
      { 
        GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent
      });
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      _client.Log += msg => {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
      };

      string? token = _config["Discord:Token"];

      if (string.IsNullOrWhiteSpace(token))
      {
        throw new InvalidOperationException("Discord bot token is missing from configuration.");
      }

      await _client.LoginAsync(TokenType.Bot, token);
      await _client.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return _client.StopAsync();
    }
  }
}