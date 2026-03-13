namespace TheBigThree.Web.ViewModels
{
    public class ProfileViewModel
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Rank { get; set; } = null!;
        public int TotalStarsEarned { get; set; }
        public string? AvatarUrl { get; set; }
        public string? OwnCollectionTitle { get; set; }
        public int? OwnCollectionId { get; set; }
        public List<string> OwnCollectionGameImages { get; set; } = new List<string>();
        public IEnumerable<CollectionAllViewModel> FavoriteCollections { get; set; } = new List<CollectionAllViewModel>();
    }
}
