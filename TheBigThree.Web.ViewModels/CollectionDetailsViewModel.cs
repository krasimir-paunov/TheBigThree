namespace TheBigThree.Web.ViewModels
{
    public class CollectionDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Publisher { get; set; } = null!;

        public int TotalStars { get; set; }
        public string? AvatarUrl { get; set; }

        public List<GameDetailsViewModel> Games { get; set; } = new List<GameDetailsViewModel>();

        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();

        public AddCommentViewModel AddComment { get; set; } = new AddCommentViewModel();
    }
}
