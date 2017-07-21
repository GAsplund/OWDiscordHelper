using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using DSharpPlus;
using System.IO;
using System.Linq;

namespace DiscordOWHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "GAsplund's Overwatch Discord Helper";
            Console.WriteLine("Welcome to GODH!");
            Console.WriteLine("Now preparing...");
            Task.Run(CheckStuffAsync);
            Console.ReadKey();
        }

        static async Task CheckStuffAsync()
        {
            string line = "";
            string configFile = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr0 = new StreamReader("users.txt"))
                {
                    line = sr0.ReadToEnd();
                }
                using (StreamReader sr1 = new StreamReader("config.txt"))
                {
                    configFile = sr1.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            string[] config = configFile.Split(';');
            bool debug = Convert.ToBoolean(config[2]);



            string nickname = null;
if (debug) { Console.WriteLine("Welcome! Defining bot stuff"); };
            var lineCount = File.ReadLines("users.txt").Count();
if (debug) { Console.WriteLine("Found " + lineCount + " lines/users in users file."); };
            //
            // 
            //
            var client = new DiscordClient(new DiscordConfig
            {
                AutoReconnect = true,
                DiscordBranch = Branch.Stable,
                LargeThreshold = 250,
                LogLevel = LogLevel.Error,
                Token = config[0],
                TokenType = TokenType.Bot,
                UseInternalLogHandler = false
            });
            client.DebugLogger.LogMessageReceived += (o, e) =>
            {
                Console.WriteLine($"[{e.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss")}] [{e.Level}] {e.Message}");
            };
if (debug) { Console.WriteLine("Defined. Attempting connect!"); };
            await client.Connect();
if (debug) { Console.WriteLine("Connected! Now launching httpClient..."); };
if (debug) { Console.WriteLine("Now launching httpClient..."); };
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0)");
            


            var i = 0;
            //if (debug) { Console.WriteLine("Line data is... " + line); };
            string userdata = line;
            string[] usrdata = userdata.Split(';');

if (debug) { Console.WriteLine("Now getting and setting user data..."); };

            Console.WriteLine("Now getting and setting data...");

            for (i = 0; i <= lineCount; i++) {
                string userdat = usrdata[i];
                string[] udata = userdat.Split(',');

                string battleTag = udata[0];
                ulong userID = Convert.ToUInt64(udata[1]);
                string username = udata[2];

if (debug) { Console.WriteLine("BattleTag: " + battleTag + " userID: " + userID + " username: " + username); };
if (debug) { Console.WriteLine("Now getting web data..."); }

                var clientResponse = await httpClient.GetAsync("https://owapi.net/api/v3/u/" + battleTag + "/stats");
                var returnData = await clientResponse.Content.ReadAsStringAsync();
                string source = returnData;
if (debug) { Console.WriteLine("Parsing data for " + username); };
                dynamic info = JObject.Parse(source);

                long level = info.eu.stats.quickplay.overall_stats.prestige * 100 + info.eu.stats.quickplay.overall_stats.level;
                Console.WriteLine(username + "'s level is " + level);
                await client.ModifyMember(Convert.ToUInt64(config[1]), userID, nickname = username + " [Lv. " + level + "]");
                await Task.Delay(1000);
            };

            string successMessage = "Successfully updated levels!";
            Console.WriteLine(successMessage);
            //await client.SendMessage(319473403725873153, successMessage);

            Console.ReadKey();
            await Task.Delay(-1);
        }
    }

}
