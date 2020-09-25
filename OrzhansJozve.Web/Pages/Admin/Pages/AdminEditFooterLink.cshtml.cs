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
    public class AdminEditFooterLinkModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public FooterLink FooterLinkModel { get; set; }
        private IFooterLinkRepository _footerLinkRepository { get; set; }
        public AdminEditFooterLinkModel(IFooterLinkRepository footerLinkRepository)
        {
            _footerLinkRepository = footerLinkRepository;
        }
        public void OnGet(int id)
        {
            FooterLinkModel = _footerLinkRepository.SelectFooterLinkById(id);
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        public IActionResult OnPostEditFooterLink(int id)
        {
            if (ModelState.IsValid)
            {
                FooterLinkModel.FooterLinkId = id;
                FooterLinkModel.FooterLinkCreateDate = DateTime.Now;
                _footerLinkRepository.Update(FooterLinkModel);
                _footerLinkRepository.Save();
                Message = "فوتر لینک سایت با موفقیت ویرایش شد";
                return Redirect("/admin/footerlink");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}