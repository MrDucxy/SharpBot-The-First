using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CubeBotRemastered.Commands
{
    public class MainCommands : BaseCommandModule
    {
        #region Ping
        [Command("ping")]
        [Aliases("pong", "lag", "latency")]
        [Description("Returns Pong and Latency.")]
        public async Task PingPong(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong! | " + "Latency: " + ctx.Client.Ping + "ms | " + $"{ctx.User.Mention}").ConfigureAwait(false);
        }
        #endregion

        #region Invite
        [Command("invite")]
        [Description("Invite the bot to a server.")]
        public async Task inviteBot(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync(ctx.Member.Mention + " | https://discord.com/oauth2/authorize?client_id=717580928683212882&scope=bot&permissions=1").ConfigureAwait(false);
        }
        #endregion

        #region About Bot
        [Command("about")]
        [Description("About SharpBot")]
        public async Task BotCreator(CommandContext ctx)
        {
            var aboutImbed = new DiscordEmbedBuilder
            {
                Description = "I'm SharpBot, I can do many things like give you memes, moderate your server, host game night, etc. I was inspired off of RoundBot!",
                Color = DiscordColor.Black
            };

            await ctx.Channel.SendMessageAsync(embed: aboutImbed).ConfigureAwait(false);
        }
        #endregion

        #region WhoAmI

        [Command("whoami")]
        [Description("Get information about yourself.")]

        public async Task whoAmI(CommandContext ctx)
        {
            var selfInfo = new DiscordEmbedBuilder
            {
                Title = "User Information: " + ctx.Member.DisplayName,
                ThumbnailUrl = ctx.Member.AvatarUrl,
                Color = DiscordColor.Black
            };

            selfInfo.AddField("User Information:",
            "**Username/Mention:** " + ctx.Member.Username + "#" + ctx.Member.Discriminator + Environment.NewLine +
            "**ID: **" + ctx.Member.Id + Environment.NewLine +
            "**Status: **" + ctx.Member.Presence.Status + Environment.NewLine +
            "**Playing: **" + ctx.Member.Presence.Activity.Name + Environment.NewLine +
            "" + Environment.NewLine +
            "**Account Created: **" + ctx.Member.CreationTimestamp.DateTime
            );

            selfInfo.AddField("Guild Information:",
            "**Guild: **" + ctx.Member.Guild.Name + $" ({ctx.Guild.Id})" + Environment.NewLine +
            "**Display Name: **" + ctx.Member.DisplayName + Environment.NewLine +
            "**Join Date: **" + ctx.Member.JoinedAt.DateTime
            );

            await ctx.Channel.SendMessageAsync(ctx.User.Mention, embed: selfInfo).ConfigureAwait(false);
        }

        #endregion

        #region WhoIs

        [Command("whois")]
        [Description("Get information about someone.")]

        public async Task whoIsUser(CommandContext ctx, DiscordMember member)
        {
            
            
            var userInfo = new DiscordEmbedBuilder
            {
                Title = "User Information: " + member.DisplayName,
                ThumbnailUrl = member.AvatarUrl,
                Color = DiscordColor.Black
            };

            userInfo.AddField("User Information:",
            "**Username/Mention: **" + member.Username + "#" + member.Discriminator + Environment.NewLine +
            "**ID: **" + member.Id + Environment.NewLine +
            "**Status: **" + member.Presence.Status + Environment.NewLine +
            "**Playing: **" + member.Presence.Activity.Name + Environment.NewLine +
            "" + Environment.NewLine +
            "**Account Created: **" + member.CreationTimestamp.DateTime
            );

            userInfo.AddField("Guild Information:",
            "**Guild: **" + member.Guild.Name + $" ({ctx.Guild.Id})" + Environment.NewLine +
            "**Display Name: **" + member.DisplayName + Environment.NewLine +
            "**Join Date: **" + member.JoinedAt.DateTime
            );

            await ctx.Channel.SendMessageAsync(ctx.User.Mention, embed: userInfo).ConfigureAwait(false);
        }

        #endregion

        #region Agree
        [Command("agree")]
        [Description("Type this to get access to the rest of the server.")]
        public async Task verify(CommandContext ctx)
        {
            try
            {
                var verifyEmbed = new DiscordEmbedBuilder
                {
                    Title = "Rule Agreement",
                    Description = "Thumbs up to agree to these rules, thumbs down to get kicked.",
                    ThumbnailUrl = ctx.Client.CurrentUser.AvatarUrl,
                    Color = DiscordColor.Black
                };

                if (ctx.Channel.Name == "verification")
                {
                    var verifyMessage = await ctx.Channel.SendMessageAsync(embed: verifyEmbed).ConfigureAwait(false);

                    var thumbsUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
                    var thumbsDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

                    await verifyMessage.CreateReactionAsync(thumbsUpEmoji).ConfigureAwait(false);
                    await verifyMessage.CreateReactionAsync(thumbsDownEmoji).ConfigureAwait(false);

                    var interactivity = ctx.Client.GetInteractivity();

                    var reactResult = await interactivity.WaitForReactionAsync(
                        x => x.Message == verifyMessage &&
                        x.User == ctx.User &&
                        (x.Emoji == thumbsUpEmoji || x.Emoji == thumbsDownEmoji)).ConfigureAwait(false);


                    if (reactResult.Result.Emoji == thumbsUpEmoji)
                    {
                        var role = ctx.Guild.GetRole(707009239809392731);
                        await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
                    }
                    else if (reactResult.Result.Emoji == thumbsDownEmoji)
                    {
                        await ctx.Member.RemoveAsync().ConfigureAwait(false);
                    }

                    await verifyMessage.DeleteAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught at: Verification. Unknown Error.");
            }
        }

        #endregion

        #region ServerInfo

        [Command("guildinfo")]
        [Aliases("serverinfo", "gi", "si")]
        [Description("Get information about the current guild/server.")]

        public async Task guildInfo(CommandContext ctx)
        {

            var serverInfo = new DiscordEmbedBuilder
            {
                Title = "Guild Information: " + ctx.Guild.Name,
                ThumbnailUrl = ctx.Guild.IconUrl,
                Color = DiscordColor.Black
            };

            serverInfo.AddField("Server Information: ",
            "**Name: **" + ctx.Guild.Name + Environment.NewLine +
            "**ID: **" + ctx.Guild.Id + Environment.NewLine +
            "**Owner: **" + ctx.Guild.Owner.Username + "#" + ctx.Guild.Owner.Discriminator + Environment.NewLine +
            "**Members: **" + ctx.Guild.MemberCount + Environment.NewLine +
            "" + Environment.NewLine +
            "**Creation Date: **" + ctx.Guild.CreationTimestamp.DateTime
            );

            await ctx.Channel.SendMessageAsync(ctx.User.Mention, embed: serverInfo).ConfigureAwait(false);
        }

        #endregion
    }
}
