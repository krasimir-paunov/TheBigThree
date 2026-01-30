namespace TheBigThree.ViewModels
{
    public class CollectionAllViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Publisher { get; set; } = null!;

        public int TotalStars { get; set; }

        public List<string> GameImages { get; set; } = new List<string>();
    }
}
