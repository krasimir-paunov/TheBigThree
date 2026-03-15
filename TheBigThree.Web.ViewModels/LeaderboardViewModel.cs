namespace TheBigThree.Web.ViewModels
{
    public class LeaderboardViewModel
    {
        public List<LeaderboardEntryViewModel> TopCollectors { get; set; } = new();
        public List<LeaderboardEntryViewModel> TopCommenters { get; set; } = new();
    }

    public class LeaderboardEntryViewModel
    {
        public int Position { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string Rank { get; set; } = string.Empty;
        public string? CollectionTitle { get; set; }
        public int? CollectionId { get; set; }
        public int Score { get; set; }
    }
}