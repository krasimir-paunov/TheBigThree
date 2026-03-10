using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentViewModel model, int collectionId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Collection", new { id = collectionId });
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                await commentService.AddCommentAsync(model, collectionId, userId);
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while adding your comment.";
            }

            return RedirectToAction("Details", "Collection", new { id = collectionId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int commentId, int collectionId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                await commentService.DeleteCommentAsync(commentId, userId);
            }
            catch (UnauthorizedAccessException)
            {
                TempData["Error"] = "You do not have permission to delete this comment.";
            }
            catch (ArgumentException)
            {
                TempData["Error"] = "The comment you are trying to delete does not exist.";
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while deleting the comment.";
            }
            return RedirectToAction("Details", "Collection", new { id = collectionId });
        }

    }
}
