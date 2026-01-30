using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheBigThree.Common.EntityValidation;

namespace TheBigThree.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GameTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(GameImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(GameDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; } 
        [Required]
        public virtual Genre Genre { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Collection))]
        public int CollectionId { get; set; }

        [Required]
        public virtual Collection Collection { get; set; } = null!;
    }
}
