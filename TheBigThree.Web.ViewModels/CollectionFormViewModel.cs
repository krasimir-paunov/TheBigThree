using System.ComponentModel.DataAnnotations;
using static TheBigThree.GCommon.EntityValidation;

namespace TheBigThree.Web.ViewModels
{
    public class CollectionFormViewModel
    {
        [Required]
        [StringLength(CollectionTitleMaxLength, MinimumLength = CollectionTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GamesPerCollectionCount, ErrorMessage = "You must provide exactly {1} games.")]
        [MaxLength(GamesPerCollectionCount, ErrorMessage = "You cannot provide more than {1} games.")]
        public List<GameFormViewModel> Games { get; set; } = new List<GameFormViewModel>();
    }
}
