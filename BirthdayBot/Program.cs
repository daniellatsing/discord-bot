using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BirthdayBot.Data;
using BirthdayBot.Services;
using BirthdayBot.Commands;
using BirthdayBot.Config;

namespace BirthdayBot
{
  internal class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
          config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((context, services) =>
        {

          services.AddSingleton<DiscordSocketClient>();
          services.AddSingleton<CommandService>();

          services.AddHostedService<BotService>();

          services.Configure<DiscordSettings>(context.Configuration.GetSection("Discord"));
          services.AddSingleton(res => res.GetRequiredService<IOptions<DiscordSettings>>().Value);
          services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection")));

          services.AddHttpClient();
          services.AddSingleton<GiphyService>();
        });
        
      await builder.Build().RunAsync();
    }
  }
}
