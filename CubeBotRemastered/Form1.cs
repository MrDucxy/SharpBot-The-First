using CubeBotRemastered.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CubeBotRemastered
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Stopwatch sw = Stopwatch.StartNew();

        private async void startBtn_Click(object sender, EventArgs e)
        {
            if(startBtn.Checked == false)
            {
                sw.Stop();
                startBtn.Text = "Start Bot";
                pingBtn.Enabled = false;
                sendMsgBtn.Enabled = false;
                setBtn.Enabled = false;
                Client.DisconnectAsync().ConfigureAwait(false);


            }
            else
            {
                sw.Start();
                startBtn.Text = "Stop Bot";
                pingBtn.Enabled = true;
                sendMsgBtn.Enabled = true;
                setBtn.Enabled = true;
                await Task.Delay(500);
                RunRoundBkpAsync();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TextWriter _writer = new TextBoxStreamWriter(outputTB);
            // Redirect the out Console stream 
            Console.SetOut(_writer);
            Console.WriteLine("Welcome to CubeBot CP! Console output will be redirected here unless disabled.");
        }







        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunRoundBkpAsync()
        {

            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LargeThreshold = 1000,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            Client.UseInteractivity(new InteractivityConfiguration());


            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = false
            };


            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<MainCommands>();
            Commands.RegisterCommands<ModerationCommands>();
            Commands.RegisterCommands<FunCommands>();

            await Client.ConnectAsync();

            await Task.Delay(-1);

        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            Client.UpdateStatusAsync(new DiscordActivity(Client.Guilds.Count + " Servers", ActivityType.Watching));
            return null;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            outputTB.AppendText("Pong! | " + "Latency: " + Client.Ping.ToString() + "ms");
            outputTB.AppendText(Environment.NewLine);
        }


        private void sendMsgBtn_Click(object sender, EventArgs e)
        {
            ulong channnelID = ulong.Parse(channelTB.Text);
            var channel = Client.GetChannelAsync(channnelID);
            Client.SendMessageAsync(channel.Result, msgTB.Text);
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (guna2Button1.Checked == false)
            {
                guna2Button1.Text = "Disable Output (Speeds Things Up)";
                // Instantiate the writer 
                TextWriter _writer = new TextBoxStreamWriter(outputTB);
                // Redirect the out Console stream 
                Console.SetOut(_writer);
                Console.WriteLine("Console output is now set to textbox.");
            }
            else
            {
                guna2Button1.Text = "Enable Output (Useful for debugging)";
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                standardOutput.AutoFlush = true;
                Console.SetOut(standardOutput);
            }
        }

        private void pingBtn_Click(object sender, EventArgs e)
        {
            outputTB.AppendText("Pong! | " + "Latency: " + Client.Ping.ToString() + "ms");
            outputTB.AppendText(Environment.NewLine);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.DisconnectAsync().ConfigureAwait(false);
        }

        private void setBtn_Click(object sender, EventArgs e)
        {
            Client.UpdateStatusAsync(new DiscordActivity(statusTB.Text, ActivityType.Playing));
        }
    }
}

