namespace BotFrameworkAlbion.Models
{
    public class Guild
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AllianceId { get; set; }
        public string AllianceName { get; set; }
        public object KillFame { get; set; }
        public int DeathFame { get; set; }
    }

}
