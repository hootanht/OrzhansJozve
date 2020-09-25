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
    public class AdminAddFooterLinkModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private IFooterLinkRepository _footerLinkRepository { get; set; }
        [BindProperty]
        public FooterLink FooterLinkModel { get; set; }
        public AdminAddFooterLinkModel(IFooterLinkRepository footerLinkRepository)
        {
            _footerLinkRepository = footerLinkRepository;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        public IActionResult OnPostAddFooterLink()
        {
            if (ModelState.IsValid)
            {
                FooterLinkModel.FooterLinkCreateDate = DateTime.Now;
                _footerLinkRepository.Insert(FooterLinkModel);
                _footerLinkRepository.Save();
                Message = "لینک فوتر سایت با موفقیت منتشر شد";
                return Redirect("/admin/footerlink");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}