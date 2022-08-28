namespace ExpSystem {
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;
    using ExpSystem.Common;
    using ExpSystem.Services;

    public class General : ModuleBase<SocketCommandContext> {
        [Command("TopUsers")]
        [Alias("top")]
        public async Task TopUsersAsync(int list = 1) {
            List<string> username = new List<string>();
            List<double> experience = new List<double>();

            var guild = (this.Context.Channel as SocketGuildChannel)?.Guild;

            await foreach(var el in guild.GetUsersAsync()) {
                foreach( var item in el) {
                    LevelHandler lh = new LevelHandler(item, guild);

                    string key = $"{item.Nickname ?? item.Username}#{item.Discriminator}";
                    double exp = lh.experience;
                    username.Add(key);
                    experience.Add(exp);
                }
            }

            Algorithm.SortListsOfValue(ref username, ref experience);

            EmoteHandler emote = new EmoteHandler(guild);

            Dictionary<ulong, string> emotes = new Dictionary<ulong, string>();

            string path = @"Logs/" + guild.Name;
            FileInfo file = new FileInfo(path + @"/UsersEmoji.txt");

            using (StreamReader sr = file.OpenText()) {
                string s;
                while ((s = sr.ReadLine()) != null)  {
                    string[] line = s.Split(' ');
                    emotes.Add(ulong.Parse(line[0]), line[1]);
                }
            }

            int maxOnList = 10;
            string topList = @"";

            try {
                for(int i = (list - 1) * maxOnList; i < list * maxOnList; i++) {
                    await foreach(var el in guild.GetUsersAsync()) {
                        foreach( var item in el) {
                            string nickname = $"{item.Nickname ?? item.Username}#{item.Discriminator}";
                            if (username[i] == nickname) {
                                topList += $"{i + 1}.   **{username[i]}** {emotes[item.Id]}\n  Experience: {experience[i]}\n";
                            }
                        }
                    }

                }
                
            }
            catch {
                int memberCount = guild.MemberCount - 1;
                int count = 0;

                while(count != 0 && count < maxOnList) {
                    await foreach(var el in guild.GetUsersAsync()) {
                        foreach( var item in el) {
                            string nickname = $"{item.Nickname ?? item.Username}#{item.Discriminator}";
                            if (username[memberCount - count] == nickname) {
                                topList += $"{memberCount - count + 1}.   **{username[memberCount - count]}** {emotes[item.Id]}\n  Experience: {experience[memberCount - count]}\n";
                                count++;                            
                            }
                        }
                    }
                }
            }

            var embed = new BotEmbedBuilder()
                .WithTitle($"Top server users (list [{list}]): ")
                .WithDescription(topList)
                .WithCurrentTimestamp()
                .Build();
            await this.ReplyAsync(embed: embed);

            ConsoleReport.Report("The list of top users is displayed");
        }
    }
}