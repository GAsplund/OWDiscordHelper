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
        public static long userNo = 1;
        public static string[] infoText = {"Basic info about my bot:","", "Config file: First line is your bot token, Second line is your guild/server ID, Third line is the debug setting.", "", "Example of config file:", "","", "(Very secret bot token);", "176689618194792448;", "false;","","", "Users file: Each line is one user, and looks like this:", "","","<BATTLETAG NAME>-<BATTLETAG ID>,<DISCORD USER ID>,<DISCORD NAME/NICK>;","", "", "Example of users file:", "", "", "L33tHax0r-1337,233892072606662658,Good Username;", "Bert-65536,233892072606662658,Unnamed;"};


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
            if (!File.Exists("users.txt"))
            {
                File.Create("users.txt");
            }
            if (!File.Exists("info.txt"))
            {
                File.Create("info.txt");
                //File.WriteAllLines("info.txt", infoText);
            }
            if (!File.Exists("config.txt"))
            {
                File.Create("config.txt");
            }
            try
            { // Getting the users file
                using (StreamReader usersStream = new StreamReader("users.txt"))
                {
                    line = usersStream.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Users file could not be read:");
                Console.WriteLine(e.Message);
            }

            try
            { // Getting the config file
                using (StreamReader configStream = new StreamReader("config.txt"))
                {
                    configFile = configStream.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Users file could not be read:");
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

            for (i = 0; i <= lineCount-1; i++) { // Getting and setting user data
                string userdat = usrdata[i];
                string[] udata = userdat.Split(',');

                string battleTag = udata[0];
                ulong userID = Convert.ToUInt64(udata[1]);
                string username = udata[2];

if (debug) { Console.WriteLine("User no. " + userNo + " with BattleTag: " + battleTag + " userID: " + userID + " username: " + username); };
if (debug) { Console.WriteLine("Now getting web data..."); }
                userNo++;
                var clientResponse = await httpClient.GetAsync("https://owapi.net/api/v3/u/" + battleTag + "/stats");
                var returnData = await clientResponse.Content.ReadAsStringAsync();
                string source = returnData;
if (debug) { Console.WriteLine("Parsing data for " + username); };
                dynamic info = JObject.Parse(source);

                long level = info.eu.stats.quickplay.overall_stats.prestige * 100 + 1 + info.eu.stats.quickplay.overall_stats.level;
                Console.WriteLine(username + "'s level is " + level);
                await client.ModifyMember(Convert.ToUInt64(config[1]), userID, nickname = username + " [Lv. " + level + "]");
                await Task.Delay(1000);
            };

            string successMessage = "Successfully updated levels!";
            Console.WriteLine("");
            Console.WriteLine(successMessage);
            //await client.SendMessage(319473403725873153, successMessage);

            Console.ReadKey();
            await Task.Delay(-1);
        }
    }

}
