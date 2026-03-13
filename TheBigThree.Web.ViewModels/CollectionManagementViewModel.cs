namespace TheBigThree.Web.ViewModels
{
    public class CollectionManagementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Owner { get; set; } = null!;
        public int TotalStars { get; set; }
        public int TotalComments { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
