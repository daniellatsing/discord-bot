using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BirthdayBot.Data;
using BirthdayBot.Services;
using Microsoft.Extensions.Configuration;

var builder = Host.CreateDefaultBuilder(args)
  .ConfigureAppConfiguration((context, config) =>
  {
    config.AddJsonFile("appsettings.json", optional: false);
  })
  .ConfigureServices((context, services) =>
  {
    services.AddDbContext<AppDbContext>(options =>
      options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection")));
    services.AddSingleton<DiscordSocketClient>();
    services.AddSingleton<GiphyService>();
    services.AddHttpClient();
    services.AddHostedService<BotService>();
  });

await builder.Build().RunAsync();