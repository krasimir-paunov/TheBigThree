namespace TheBigThree.Web.ViewModels
{
    public class PublicProfileViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string Rank { get; set; } = string.Empty;
        public int TotalStars { get; set; }
        public int TotalComments { get; set; }
        public DateTime MemberSince { get; set; }
        public string? CollectionTitle { get; set; }
        public int? CollectionId { get; set; }
        public List<string> CollectionGameImages { get; set; } = new();
        public List<PublicProfileCommentViewModel> RecentComments { get; set; } = new();
    }

    public class PublicProfileCommentViewModel
    {
        public string Content { get; set; } = string.Empty;
        public string CollectionTitle { get; set; } = string.Empty;
        public int CollectionId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}