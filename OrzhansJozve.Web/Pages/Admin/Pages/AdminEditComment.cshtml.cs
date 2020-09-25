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
    public class AdminEditCommentModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public Comment CommentModel { get; set; }
        private IPageRepository _pageRepository { get; set; }
        private ICommentRepository _commentRepository { get; set; }
        public AdminEditCommentModel(ICommentRepository commentRepository, IPageRepository pageRepository)
        {
            _commentRepository = commentRepository;
            _pageRepository = pageRepository;
        }
        public void OnGet(int id)
        {
            CommentModel = _commentRepository.SelectById(id);
        }
        public IActionResult OnPost(string query)
        {
            //Uri uri = new Uri($"{MainSite}/admin/search/{query}");
            //return Redirect(uri.AbsoluteUri);
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        
        public IActionResult OnPostEditComment(int id)
        {
            if (ModelState.IsValid)
            {
                CommentModel.CommentId = id;
                CommentModel.Page = _pageRepository.SelectById(CommentModel.PageId);
                CommentModel.CommentCreateDate = DateTime.Now;
                _commentRepository.Update(CommentModel);
                _commentRepository.Save();
                Message = "نظر با موفقیت ویرایش شد";
                return Redirect("/admin/comment");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}