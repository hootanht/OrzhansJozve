using System;
using System.Collections.Generic;
using System.IO;
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
    public class AdminAuthorModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<Author> AuthorsList { get; set; }
        private IAuthorRepository _authorRepository { get; set; }
        public AdminAuthorModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public void OnGet(int pageid = 1)
        {
            int take = 6;
            int skip = (pageid - 1) * take;
            int Count = _authorRepository.GetAllAuthorNumber();
            ViewData["PageID"] = pageid;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            AuthorsList = _authorRepository.SelectForPagging(skip, take).ToList();
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        
        public IActionResult OnGetDelete(int id)
        {
            var author = _authorRepository.SelectById(id);
            string authorImagePath = "wwwroot/Blog-Content/Author-Images";
            if (author.AuthorImageUrl != null)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), authorImagePath, author.AuthorImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), authorImagePath, author.AuthorImageUrl));
                }
            }
            _authorRepository.Delete(author);
            _authorRepository.Save();
            Message = "نویسنده با موفقیت حذف شد";
            return Redirect("/admin/author");
        }
    }
}