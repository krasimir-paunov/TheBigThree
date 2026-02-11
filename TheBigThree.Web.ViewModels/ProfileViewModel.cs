namespace TheBigThree.Web.ViewModels
{
    public class ProfileViewModel
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Rank { get; set; } = null!;
        public int TotalStarsEarned { get; set; }

        public IEnumerable<CollectionAllViewModel> FavoriteCollections { get; set; } = new List<CollectionAllViewModel>();
    }
}
