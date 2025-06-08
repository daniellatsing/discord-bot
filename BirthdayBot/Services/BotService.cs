
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using BirthdayBot.Services;

namespace BirthdayBot.Services
{
  public class BotService : IHostedService
  {
    public Task StartAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}