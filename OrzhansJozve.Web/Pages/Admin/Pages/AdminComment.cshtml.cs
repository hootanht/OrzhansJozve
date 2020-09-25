using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    [Authorize]
    public class AdminCommentModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<Comment> CommentsList { get; set; }
        private ICommentRepository _commentRepository { get; set; }
        public AdminCommentModel(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public void OnGet(int pageId = 1)
        {
            int take = 6;
            int skip = (pageId - 1) * take;
            int Count = _commentRepository.GetAllCommentIsAcceptNumber();
            ViewData["PageID"] = pageId;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            CommentsList = _commentRepository.SelectForPagging(skip, take).ToList();
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        
        public IActionResult OnGetDelete(int id)
        {
            var comment = _commentRepository.SelectById(id);
            _commentRepository.Delete(comment);
            _commentRepository.Save();
            Message = "نظر با موفقیت حذف شد";
            return Redirect("/admin/comment");
        }
    }
}