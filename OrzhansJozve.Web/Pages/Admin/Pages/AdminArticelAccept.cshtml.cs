using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    [Authorize]
    public class AdminArticelAcceptModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<OrzhansJozve.DomainClass.Domain.Page> Pages { get; set; }
        private IPageRepository _pageRepository { get; set; }
        public AdminArticelAcceptModel(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public void OnGet(int pageid = 1)
        {
            int take = 6;
            int skip = (pageid - 1) * take;
            int Count = _pageRepository.GetAllPageNotAcceptNumber();
            ViewData["PageID"] = pageid;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            Pages = _pageRepository.SelectForPaggingNotAccept(skip, take).ToList();
        }
        
        public IActionResult OnGetRelese(int Id)
        {
            var page = _pageRepository.SelectById(Id);
            page.PageShow = true;
            page.PageView = _pageRepository.SelectById(Id).PageView;
            _pageRepository.Update(page);
            _pageRepository.Save();
            Message = "مقاله با موفقیت منتشر شد";
            return Redirect("/admin/articel");
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        
        public IActionResult OnGetDelete(int Id)
        {
            var page = _pageRepository.SelectById(Id);
            string mainImagepath = "wwwroot/Blog-Content/Articel-Images";
            string podcastPath = "wwwroot/Blog-Content/podcasts";
            if (page.PageImageUrl != null)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), mainImagepath, page.PageImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), mainImagepath, page.PageImageUrl));
                }
            }
            if (page.PageSecondImageUrl != null)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), mainImagepath, page.PageSecondImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), mainImagepath, page.PageSecondImageUrl));
                }
            }
            if (page.PagePodcastUrl != null)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), podcastPath, page.PagePodcastUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), podcastPath, page.PagePodcastUrl));
                }
            }
            _pageRepository.Delete(page);
            _pageRepository.Save();
            Message = "مقاله با موفقیت حذف شد";
            return Redirect("/admin/articel");
        }
    }
}