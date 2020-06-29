using DSharpPlus;
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
    [Hidden] //Hides commands from everyone else lol
    class ModerationCommands : BaseCommandModule
    {
        #region MiniMod

        [Command("addminimod")]
        [RequirePermissions(Permissions.KickMembers)]
        public async Task addMini(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(718761005479624745);
            await member.GrantRoleAsync(role, "Promotion").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been promoted to Mini-Moderator!").ConfigureAwait(false);
        }

        [Command("removeminimod")]
        [RequirePermissions(Permissions.KickMembers)]
        public async Task removeMini(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(718761005479624745);
            await member.RevokeRoleAsync(role, "Demotion.").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been demoted from Mini-Moderator.").ConfigureAwait(false);
        }

        #endregion

        #region Mod

        [Command("addMod")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task addMod(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(718761442739879956);
            await member.GrantRoleAsync(role, "Promotion").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been promoted to Moderator!").ConfigureAwait(false);
        }

        [Command("removeMod")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task removeMod(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(718761442739879956);
            await member.RevokeRoleAsync(role, "Demotion.").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been demoted from Moderator.").ConfigureAwait(false);
        }

        #endregion

        #region Admin

        [Command("addAdmin")]
        [RequireOwner]
        public async Task addAdmin(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(718761682263998464);
            await member.GrantRoleAsync(role, "Promotion").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been promoted to Admin!").ConfigureAwait(false);
        }

        [Command("removeAdmin")]
        [RequireOwner]
        public async Task removeAdmin(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(718761682263998464);
            await member.RevokeRoleAsync(role, "Demotion.").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been demoted from Admin.").ConfigureAwait(false);
        }

        #endregion

        #region Hunter

        [Command("addHunter")]
        [RequirePermissions(Permissions.KickMembers)]
        public async Task addSupport(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(720356390031458377);
            await member.GrantRoleAsync(role, "Promotion").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " just been givin the Noticed Hunter role.").ConfigureAwait(false);
        }

        [Command("removeHunter")]
        [RequirePermissions(Permissions.KickMembers)]
        public async Task removeSupport(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(720356390031458377);
            await member.RevokeRoleAsync(role, "Demotion.").ConfigureAwait(false);
        }

        #endregion

        #region Noticed

        [Command("notice")]
        [RequirePermissions(Permissions.KickMembers)]
        public async Task addNoticed(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(718760796200501258);
            await member.GrantRoleAsync(role, "Promotion").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " is now noticed by staff!").ConfigureAwait(false);
        }

        [Command("removenotice")]
        [RequirePermissions(Permissions.KickMembers)]
        public async Task removeNoticed(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(718760796200501258);
            await member.RevokeRoleAsync(role, "Demotion.").ConfigureAwait(false);
        }

        #endregion

        #region ResetRoles
        [Command("resetRoles")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task demoteFully(CommandContext ctx, DiscordMember member)
        {
            var noticed = ctx.Guild.GetRole(718760796200501258);
            var supporter = ctx.Guild.GetRole(720356390031458377);
            var admin = ctx.Guild.GetRole(718761682263998464);
            var mod = ctx.Guild.GetRole(718761442739879956);
            var mini = ctx.Guild.GetRole(718761005479624745);

            await member.RevokeRoleAsync(mini, "Demotion.").ConfigureAwait(false);
            await member.RevokeRoleAsync(mod, "Demotion.").ConfigureAwait(false);
            await member.RevokeRoleAsync(admin, "Demotion.").ConfigureAwait(false);
            await member.RevokeRoleAsync(supporter, "Demotion.").ConfigureAwait(false);
            await member.RevokeRoleAsync(noticed, "Demotion.").ConfigureAwait(false);


            await ctx.Channel.SendMessageAsync(member.Mention + " has been reset.").ConfigureAwait(false);
        }

        #endregion



        #region Warn

        [Command("warn")]
        [RequirePermissions(Permissions.ManageMessages)]
        public async Task warn(CommandContext ctx, DiscordMember member, string reason)
        {
            var role = ctx.Guild.GetRole(720426211918217357);
            await member.GrantRoleAsync(role, "Promotion").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been warned for: " + reason).ConfigureAwait(false);
        }

        [Command("unwarn")]
        [RequirePermissions(Permissions.ManageMessages)]
        public async Task removeWarn(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(720426211918217357);
            await member.RevokeRoleAsync(role, "Demotion.").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " is no longer warned.").ConfigureAwait(false);
        }

        #endregion

        #region Mute

        [Command("mute")]
        [RequirePermissions(Permissions.ManageMessages)]
        public async Task mute(CommandContext ctx, DiscordMember member, string reason)
        {
            var role = ctx.Guild.GetRole(720426284701843478);
            await member.GrantRoleAsync(role, "Promotion").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been muted for: " + reason).ConfigureAwait(false);
        }

        [Command("unmute")]
        [RequirePermissions(Permissions.ManageMessages)]
        public async Task removeMute(CommandContext ctx, DiscordMember member)
        {
            var role = ctx.Guild.GetRole(720426284701843478);
            await member.RevokeRoleAsync(role, "Demotion.").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(member.Mention + " is no longer muted.").ConfigureAwait(false);
        }

        #endregion

        #region Ban

        [Command("ban")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task ban(CommandContext ctx, DiscordMember member, string reason)
        {
            await member.BanAsync(0, reason);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been banned for: " + reason).ConfigureAwait(false);
        }

        [Command("unban")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task unban(CommandContext ctx, DiscordUser user)
        {
            await user.UnbanAsync(ctx.Guild);
            await ctx.Channel.SendMessageAsync(user.Mention + " has been unbanned.").ConfigureAwait(false);
        }

        #endregion

        #region Kick

        [Command("kick")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task kick(CommandContext ctx, DiscordMember member, string reason)
        {
            await member.RemoveAsync(reason);
            await ctx.Channel.SendMessageAsync(member.Mention + " has been kicked for: " + reason).ConfigureAwait(false);
        }

        #endregion

    }
}
