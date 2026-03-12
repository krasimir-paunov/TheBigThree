namespace TheBigThree.Web.ViewModels
{
    public class CollectionMineViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int TotalStars { get; set; }
        public List<GameDetailsViewModel> Games { get; set; } = new List<GameDetailsViewModel>();
    }
}
