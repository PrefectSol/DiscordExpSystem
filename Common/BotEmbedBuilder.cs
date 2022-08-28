namespace ExpSystem.Common {
    /// <summary>
    /// Themes for embed.
    /// </summary>
    
    using Discord;

    internal class BotEmbedBuilder : EmbedBuilder {
        public BotEmbedBuilder() {
            this.WithColor(new Color(105, 142, 186));
            this.WithDescription("Тут пока ничего нет.");
        }
    }
}