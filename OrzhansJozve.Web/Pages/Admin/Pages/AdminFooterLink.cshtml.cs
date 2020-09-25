using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    public class AdminFooterLinkModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<FooterLink> FooterLinkList { get; set; }
        private IFooterLinkRepository _footerLinkRepository { get; set; }
        public AdminFooterLinkModel(IFooterLinkRepository footerLinkRepository)
        {
            _footerLinkRepository = footerLinkRepository;
        }
        public void OnGet(int pageId = 1)
        {
            int take = 6;
            int skip = (pageId - 1) * take;
            int Count = _footerLinkRepository.AllFooterLinksCount();
            ViewData["PageID"] = pageId;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            FooterLinkList = _footerLinkRepository.SelectAllFooterLinksForPaging(skip, take).ToList();
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        public IActionResult OnGetDelete(int id)
        {
            var footerLink = _footerLinkRepository.SelectFooterLinkById(id);
            _footerLinkRepository.Delete(footerLink);
            _footerLinkRepository.Save();
            Message = "لینک فوتر سایت با موفقیت حذف شد";
            return Redirect("/admin/footerlink");
        }
    }
}