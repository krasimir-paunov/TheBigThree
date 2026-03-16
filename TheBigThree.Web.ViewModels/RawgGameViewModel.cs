namespace TheBigThree.Web.ViewModels
{
    public class RawgGameViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string BackgroundImage { get; set; } = string.Empty;
        public string Released { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int? MetacriticScore { get; set; }
        public string Developers { get; set; } = string.Empty;
        public string Platforms { get; set; } = string.Empty;
        public string Genres { get; set; } = string.Empty;
        public List<string> Screenshots { get; set; } = new();
    }

    public class RawgSearchResultViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BackgroundImage { get; set; } = string.Empty;
        public string Released { get; set; } = string.Empty;
        public double Rating { get; set; }

        public List<RawgGenreItem> Genres { get; set; } = new();

        public class RawgGenreItem
        {
            public string Name { get; set; } = string.Empty;
        }
    }


}