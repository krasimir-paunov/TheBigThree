namespace TheBigThree.Web.ViewModels
{
    public class CollectionQueryModel
    {
        public string? SearchTerm { get; set; }
        public string? GenreFilter { get; set; }
        public string? Sorting { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int CollectionsPerPage { get; set; } = 6;
        public int TotalCollections { get; set; }
        public IEnumerable<string> Genres { get; set; } = new List<string>();
        public IEnumerable<CollectionAllViewModel> Collections { get; set; } = new List<CollectionAllViewModel>();
    }
}
