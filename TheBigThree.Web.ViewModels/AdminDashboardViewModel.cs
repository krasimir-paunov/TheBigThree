namespace TheBigThree.Web.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalCollections { get; set; }
        public int TotalComments { get; set; }
        public int TotalStars { get; set; }
        public string TopCollectionTitle { get; set; } = null!;
        public string TopCollectionOwner { get; set; } = null!;
        public int TopCollectionStars { get; set; }
        public int TopCollectionId { get; set; }
        public string MostActiveUser { get; set; } = null!;
        public int MostActiveUserComments { get; set; }
        public List<CollectionManagementViewModel> RecentCollections { get; set; } = new List<CollectionManagementViewModel>();
    }
}