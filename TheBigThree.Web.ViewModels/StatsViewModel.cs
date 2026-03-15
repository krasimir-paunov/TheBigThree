namespace TheBigThree.Web.ViewModels
{
    public class StatsViewModel
    {
        public int TotalCollections { get; set; }
        public int TotalUsers { get; set; }
        public int TotalStars { get; set; }
        public int TotalComments { get; set; }

        public List<GenreStatViewModel> TopGenres { get; set; } = new();
        public List<TopCollectionViewModel> TopThreeCollections { get; set; } = new();

        public string MostCommentedCollectionTitle { get; set; } = string.Empty;
        public int MostCommentedCollectionId { get; set; }
        public int MostCommentedCollectionComments { get; set; }

        public string MostActiveCommenter { get; set; } = string.Empty;
        public int MostActiveCommenterCount { get; set; }

        public string NewestCollectionTitle { get; set; } = string.Empty;
        public int NewestCollectionId { get; set; }
        public string NewestCollectionOwner { get; set; } = string.Empty;
    }

    public class GenreStatViewModel
    {
        public string Genre { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class TopCollectionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public int TotalStars { get; set; }
        public string? AvatarUrl { get; set; }
        public string PublisherRank { get; set; } = string.Empty;
    }
}