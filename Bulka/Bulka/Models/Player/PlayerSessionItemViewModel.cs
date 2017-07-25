namespace Bulka.Models.Player
{
    public class PlayerSessionItemViewModel
    {
        public long Id { get; set; }
        public int PlayerId { get; set; }
        public int ClubId { get; set; }

        public string DateTime { get; set; }
        public string Duration { get; set; }

        public string ClubName { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
    }
}