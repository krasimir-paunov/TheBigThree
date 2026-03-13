namespace TheBigThree.Web.ViewModels
{
    public class UserManagementViewModel
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool HasCollection { get; set; }
        public int? CollectionId { get; set; }
    }
}
