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
    public class AdminSearchModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public string MainSite { get; set; }
        public string Query { get; set; }
        public List<OrzhansJozve.DomainClass.Domain.Page> Pages { get; set; }
        private IPageRepository _pageRepository { get; set; }
        public AdminSearchModel(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
            MainSite = "https://orzhansjozve.ir";
        }
        public void OnGet(string query, int pageid = 1)
        {
            if (_pageRepository.PageExistAdmin(query))
            {
                int take = 6;
                int skip = (pageid - 1) * take;
                int Count = _pageRepository.GetAllPageForAdminPanelNumber(query.Replace("-", " "));
                ViewData["PageID"] = pageid;
                ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
                Pages = _pageRepository.GetAllPageForAdminPanel(query.Replace("-", " "), skip, take).ToList();
            }
            Query = query;
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

        
        public IActionResult OnPost(string query, int pageid = 1)
        {
            if (_pageRepository.PageExistAdmin(query))
            {
                int take = 6;
                int skip = (pageid - 1) * take;
                int Count = _pageRepository.GetAllPageForAdminPanelNumber(query.Replace("-", " "));
                ViewData["PageID"] = pageid;
                ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
                Pages = _pageRepository.GetAllPageForAdminPanel(query.Replace("-", " "), skip, take).ToList();
            }
            Query = query;
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
    }
}