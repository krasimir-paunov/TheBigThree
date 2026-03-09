using System.ComponentModel.DataAnnotations;
using static TheBigThree.GCommon.EntityValidation;

namespace TheBigThree.Web.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        [MinLength(CommentContentMinLength)]
        [MaxLength(CommentContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
