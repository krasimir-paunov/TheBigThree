using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBigThree.Data.Models
{
    public class Like
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; } = null!;

        [Required]
        public int CollectionId { get; set; }

        [Required]
        [ForeignKey(nameof(CollectionId))]
        public virtual Collection Collection { get; set; } = null!;
    }
}
