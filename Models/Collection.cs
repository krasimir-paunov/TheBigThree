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

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
