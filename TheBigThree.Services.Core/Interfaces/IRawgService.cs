using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services.Core.Interfaces
{
    public interface IRawgService
    {
        Task<IEnumerable<RawgSearchResultViewModel>> SearchGamesAsync(string query);
        Task<RawgGameViewModel?> GetGameDetailsAsync(string gameName);
        bool IsConfigured();
    }
}