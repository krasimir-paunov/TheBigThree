namespace TheBigThree.Services.Core.Interfaces
{
    public interface IProfileService
    {
        Task<int> GetUserTotalStarsAsync(string userId);
        Task<(string? Title, int? Id, List<string> GameImages)> GetOwnCollectionPreviewAsync(string userId);
    }
}
