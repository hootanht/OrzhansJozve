using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    public class AdminAddInsideAticelImageModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private IInsideAticelImageRepository _insideAticelImageRepository { get; set; }
        [BindProperty]
        public InsideAticelImage InsideAticelImageModel { get; set; }
        public AdminAddInsideAticelImageModel(IInsideAticelImageRepository insideAticelImageRepository)
        {
            _insideAticelImageRepository = insideAticelImageRepository;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        public async Task<IActionResult> OnPostAddInsideAticelImage(IFormFile insideAticelImage)
        {
            if (ModelState.IsValid)
            {
                InsideAticelImageModel.InsideAticelImageCreateDate = DateTime.Now;
                if (insideAticelImage != null)
                {
                    string path = "wwwroot/Blog-Content/InsideAticelImages";
                    InsideAticelImageModel.InsideAticelImageName = Guid.NewGuid().ToString() + InsideAticelImageModel.InsideAticelImageTitle.Replace(" ", "-") + Path.GetExtension(insideAticelImage.FileName);
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), path));
                    }
                    string authorImagePath = Path.Combine(Directory.GetCurrentDirectory(), path, InsideAticelImageModel.InsideAticelImageName);
                    using (var stream = new FileStream(authorImagePath, FileMode.Create))
                    {
                        await insideAticelImage.CopyToAsync(stream);
                    }
                }
                _insideAticelImageRepository.Insert(InsideAticelImageModel);
                _insideAticelImageRepository.Save();
                Message = "عکس با موفقیت منتشر شد";
                return Redirect("/admin/Insideaticelimage");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}