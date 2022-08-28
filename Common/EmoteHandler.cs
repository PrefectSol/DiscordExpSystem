namespace ExpSystem.Services
{
    using Discord.WebSocket;
    using System.IO;

    public class EmoteHandler
    { 
        private static SocketGuild? guild;

        public EmoteHandler(SocketGuild? guild) {
            EmoteHandler.guild = guild;

            Check();            
        }

        private static async void Check() {
            string path = @"Logs/" + guild.Name;

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            List<string> discrimotanors = new List<string>();
            List<string> emotes = new List<string>();

            await foreach(var el in guild.GetUsersAsync()) {
                foreach( var item in el) {
                    string key = $"{item.Id}";
                    discrimotanors.Add(key);
                }
            }

            foreach (var e in guild.Emotes) { 
                emotes.Add($"<:{e.Name}:{e.Id}>");
            }

            FileInfo file = new FileInfo(path + @"/UsersEmoji.txt");
            Random rnd = new Random();
            
            if (!file.Exists) {
                using (StreamWriter sw = file.CreateText()) { 
                    for(int i = 0; i < discrimotanors.Count; i++) {
                        int aEmoji = rnd.Next(0, emotes.Count);
                        sw.WriteLine($"{discrimotanors[i]} {emotes[aEmoji]}");
                    }
                }   
            }
        }
    }
}