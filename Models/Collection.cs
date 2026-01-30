using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheBigThree.Common.EntityValidation;

namespace TheBigThree.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CollectionTitleMaxLength)]
        public string Title { get; set; } = null!;

        public int TotalStars { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        [Required]
        public virtual IdentityUser User { get; set; } = null!;

        [Required]
        public virtual ICollection<Game> Games { get; set; } = null!;
    }
}
