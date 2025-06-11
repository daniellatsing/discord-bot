
using Microsoft.Extensions.Hosting;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using System.Reflection;
using BirthdayBot.Config;

namespace BirthdayBot.Services
{
  public class BotService : IHostedService
  {
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly IConfiguration _config;
    private readonly IServiceProvider _services;
    private readonly ILogger<BotService> _logger;
    private string _prefix;
    private readonly string _token;

    public BotService(
      DiscordSocketClient client,
      CommandService commands,
      IServiceProvider services,
      DiscordSettings settings,
      ILogger<BotService> logger,
      IConfiguration config)
    {
      _config = config;
      _client = new DiscordSocketClient(new DiscordSocketConfig
        {
          GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent
        });
      _commands = commands;
      _services = services;
      _prefix = settings.CommandPrefix;
      _token = settings.Token;
      _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      _client.Log += msg =>
      {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
      };

      _client.MessageReceived += HandleCommands;

      string? token = _config["Discord:Token"];

      if (string.IsNullOrWhiteSpace(token))
      {
        throw new InvalidOperationException("Discord bot token is missing from configuration.");
      }

      await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
      await _client.LoginAsync(TokenType.Bot, token);
      await _client.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return _client.StopAsync();
    }

    private async Task HandleCommands(SocketMessage msg)
    {
      if (msg is not SocketUserMessage message || message.Author.IsBot) return;

      var ctx = new SocketCommandContext(_client, message);
      int argPos = 0;

      if (!message.HasStringPrefix(_prefix, ref argPos)) return;

      var result = await _commands.ExecuteAsync(ctx, argPos, _services);

      if (!result.IsSuccess)
      {
        await ctx.Channel.SendMessageAsync($"Error: {result.ErrorReason}");
      }
    }
  }
}