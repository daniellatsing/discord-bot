using System;
using Discord.Commands;
using System.Threading.Tasks;

namespace BirthdayBot.Commands
{
  public class BirthdayCommands : ModuleBase<SocketCommandContext>
  {
    [Command("set")] // set user's birthday
    public async Task SetBirthday()
    {
      await Context.Channel.SendMessageAsync("test");
    }

    [Command("birth")] // get specific user's birthday
    public async Task GetBirthday()
    {
      await Context.Channel.SendMessageAsync("test");
    }

    [Command("next")] // next user's birthday in server
    public async Task NextBirthday()
    {
      await Context.Channel.SendMessageAsync("test");
    }

    [Command("remove")] // remove user's birthday in server
    public async Task RemoveBirthday()
    {
      await Context.Channel.SendMessageAsync("test");
    }


    [Command("all")] // lists all user's birthday for a given month in server
    public async Task AllBirthdays()
    {
      await Context.Channel.SendMessageAsync("test");
    }


    [Command("count")] // show's how birthday in server
    public async Task CountBirthdaysInServer()
    {
      await Context.Channel.SendMessageAsync("test");
    }

    [Command("help")] // show user all commands
    public async Task Help()
    {
      var helpMessage = "Available commands:\n" +
                        "`!set` - Set your birthday\n" +
                        "`!next` - Show next birthday\n" +
                        "`!remove` - Remove your birthday\n" +
                        "`!all` - List all birthdays\n" +
                        "`!count` - Count birthdays in server\n" +
                        "`!help` - Show this help message";
      await Context.Channel.SendMessageAsync(helpMessage);
    }
  }
}