namespace ExpSystem.Common
{
    using Discord;
    using Discord.WebSocket;

    public class LevelHandler
    {
        public double experience;
        public double days;

        private static IGuildUser? gUser;
        private static SocketUser? user;
        private static SocketGuild? guild;
        private static SocketGuildUser? guildUser;

        public LevelHandler(SocketUser user, SocketGuild guild) {
            LevelHandler.user = user;
            LevelHandler.guild = guild;
            guildUser = guild.GetUser(user.Id);

            CalculateDnE(ref days, ref experience);
        }  
        public LevelHandler(IGuildUser user, SocketGuild guild) {
            LevelHandler.gUser = user;
            LevelHandler.guild = guild;
            guildUser = guild.GetUser(user.Id);

            CalculateDnE(ref days, ref experience);
        }  

        private static void CalculateDnE(ref double days, ref double experience) {
            days = (DateTime.Now - guildUser.JoinedAt.Value).Days;
            
            double hours = (DateTime.Now - guildUser.JoinedAt.Value).TotalHours;
            double serverDays = (DateTime.Now - guild.CreatedAt).Days;

            experience = Math.Round((hours * 10) * (days / serverDays));
        }
    }
}