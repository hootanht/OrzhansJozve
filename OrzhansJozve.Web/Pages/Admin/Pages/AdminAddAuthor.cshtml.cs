using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    [Authorize]
    public class AdminAddAuthorModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public Author AuthorModel { get; set; }
        private IAuthorRepository _authorRepository { get; set; }
        public AdminAddAuthorModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        public async Task<IActionResult> OnPostAddAuthor(IFormFile authorImage)
        {
            AuthorModel.AuthorCreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {

                if (authorImage != null)
                {
                    string path = "wwwroot/Blog-Content/Author-Images";
                    AuthorModel.AuthorImageUrl = Guid.NewGuid().ToString() + AuthorModel.AuthorName.Replace(" ", "-") + Path.GetExtension(authorImage.FileName);
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), path));
                    }
                    string authorImagePath = Path.Combine(Directory.GetCurrentDirectory(), path, AuthorModel.AuthorImageUrl);
                    using (var stream = new FileStream(authorImagePath, FileMode.Create))
                    {
                        await authorImage.CopyToAsync(stream);
                    }
                }
                _authorRepository.Insert(AuthorModel);
                _authorRepository.Save();
                Message = "نویسنده با موفقیت منتشر شد";
                return Redirect("/admin/author");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}