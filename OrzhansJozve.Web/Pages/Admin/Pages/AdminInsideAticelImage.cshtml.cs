using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    public class AdminInsideAticelImageModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private IInsideAticelImageRepository _insideAticelImageRepository { get; set; }
        public List<InsideAticelImage> InsideAticelImages { get; set; }
        public AdminInsideAticelImageModel(IInsideAticelImageRepository insideAticelImageRepository)
        {
            _insideAticelImageRepository = insideAticelImageRepository;
        }
        public void OnGet(int pageid = 1)
        {
            int take = 6;
            int skip = (pageid - 1) * take;
            int Count = _insideAticelImageRepository.AllInsideAticelImageCount();
            ViewData["PageID"] = pageid;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            InsideAticelImages = _insideAticelImageRepository.SelectAllInsideAticelImageForPaging(skip, take).ToList();
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        public IActionResult OnGetDelete(int id)
        {
            var ad = _insideAticelImageRepository.SelectInsideAticelImageById(id);
            string authorImagePath = "wwwroot/Blog-Content/InsideAticelImages";
            if (ad.InsideAticelImageName != null)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), authorImagePath, ad.InsideAticelImageName)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), authorImagePath, ad.InsideAticelImageName));
                }
            }
            _insideAticelImageRepository.Delete(ad);
            _insideAticelImageRepository.Save();
            Message = "عکس با موفقیت حذف شد";
            return Redirect("/admin/Insideaticelimage");
        }
    }
}