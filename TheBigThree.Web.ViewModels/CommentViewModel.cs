namespace TheBigThree.Web.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string UserName { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string? AvatarUrl { get; set; }
    }
}
