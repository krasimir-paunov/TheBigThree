using System.ComponentModel.DataAnnotations;
using static TheBigThree.Common.EntityValidation;

namespace TheBigThree.ViewModels
{
    public class GameFormViewModel
    {
        [Required(ErrorMessage = "Game title is required.")]
        [StringLength(GameTitleMaxLength, MinimumLength = GameTitleMinLength,
        ErrorMessage = "Title must be between {2} and {1} characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Image URL is required.")]
        [MaxLength(GameImageUrlMaxLength)]
        [RegularExpression(@"^https?:\/\/.*", ErrorMessage = "Please enter a valid URL starting with http:// or https://")]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "You must explain why this game is in your Top 3.")]
        [StringLength(GameDescriptionMaxLength, MinimumLength = GameDescriptionMinLength,
            ErrorMessage = "Description must be between {2} and {1} characters.")]
        [Display(Name = "Why is this in your Big Three?")]
        public string Description { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid genre.")]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public IEnumerable<GenreSelectViewModel> Genres { get; set; } = new List<GenreSelectViewModel>();
    }
}

