using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheBigThree.GCommon.EntityValidation;

namespace TheBigThree.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        [Required]
        public virtual IdentityUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Collection))]
        public int CollectionId { get; set; }

        [Required]
        public virtual Collection Collection { get; set; } = null!;
    }
}
