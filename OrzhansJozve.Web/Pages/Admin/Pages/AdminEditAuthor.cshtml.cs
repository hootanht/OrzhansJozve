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
    public class AdminEditAuthorModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public Author AuthorModel { get; set; }
        private IAuthorRepository _authorRepository { get; set; }
        public AdminEditAuthorModel(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public void OnGet(int id)
        {
            AuthorModel = _authorRepository.SelectById(id);
        }
        public IActionResult OnPost(string query)
        {
            //Uri uri = new Uri($"{MainSite}/admin/search/{query}");
            //return Redirect(uri.AbsoluteUri);
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        
        public async Task<IActionResult> OnPostEditAuthor(int id,IFormFile authorImage)
        {
            if (ModelState.IsValid)
            {
                AuthorModel.AuthorId = id;
                AuthorModel.AuthorCreateDate = DateTime.Now;
                var selectedAuthor = _authorRepository.SelectById(id);
                if (authorImage!=null)
                {
                    string path = "wwwroot/Blog-Content/Author-Images";
                    if (selectedAuthor.AuthorImageUrl != null)
                    {
                        if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path, selectedAuthor.AuthorImageUrl)))
                        {
                            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), path, selectedAuthor.AuthorImageUrl));
                        }
                    }
                    AuthorModel.AuthorImageUrl = Guid.NewGuid().ToString() + AuthorModel.AuthorName.Replace(" ", "-") + Path.GetExtension(authorImage.FileName);
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), path));
                    }
                    string authorImagPath = Path.Combine(Directory.GetCurrentDirectory(), path, AuthorModel.AuthorImageUrl);
                    using (var stream = new FileStream(authorImagPath, FileMode.Create))
                    {
                        await authorImage.CopyToAsync(stream);
                    }
                }
                else
                {
                    AuthorModel.AuthorImageUrl = selectedAuthor.AuthorImageUrl;
                }
                _authorRepository.Update(AuthorModel);
                _authorRepository.Save();
                Message = "نویسنده با موفقیت ویرایش شد";
                return Redirect("/admin/author");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}