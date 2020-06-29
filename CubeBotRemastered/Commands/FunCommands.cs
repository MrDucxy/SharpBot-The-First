using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CubeBotRemastered.Commands
{
    class FunCommands : BaseCommandModule
    {
        #region Meme

        [Command("meme")]
        [Description("Sends a meme from Ducxy's collection.")]

        public async Task sendMeme(CommandContext ctx)
        {
            string[] responses = { "lmao check this one out", "lul", "stole this one from reddit", "lmao bruh", "best meme 2020", "imagine getting memes from a bot", "fun fact: these memes are shitty", "stop asking me for memes people", "i dont have much memes, but heres one ig.", "here! take a meme!", "who even added these responses? lol", "fun fact: these memes are from my creators person collection.", "© SharpMemes 2020 " };
            string message = responses[new Random().Next(0, responses.Length)];

            string path = "./memes";
            Random rand = new Random();

            // pick a random file
            string[] files = Directory.GetFiles(path);
            string randomFile = files[rand.Next(files.Length)];

            await ctx.Channel.SendMessageAsync(message).ConfigureAwait(false);
            await ctx.Channel.SendFileAsync(randomFile);
        }

        #endregion
    }
}
