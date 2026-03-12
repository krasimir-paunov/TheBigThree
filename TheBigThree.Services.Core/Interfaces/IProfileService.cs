namespace TheBigThree.Services.Core.Interfaces
{
    public interface IProfileService
    {
        Task<int> GetUserTotalStarsAsync(string userId);
    }
}
