using System;
using System.Collections.Generic;
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
    public class AdminPageGroupModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<PageGroup> PageGroupsList { get; set; }
        private IPageGroupRepository _pageGroupRepository { get; set; }
        public AdminPageGroupModel(IPageGroupRepository pageGroupRepository)
        {
            _pageGroupRepository = pageGroupRepository;
        }
        public void OnGet(int pageid = 1)
        {
            int take = 6;
            int skip = (pageid - 1) * take;
            int Count = _pageGroupRepository.GetAllPageGroupNumber();
            ViewData["PageID"] = pageid;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            PageGroupsList = _pageGroupRepository.SelectForPagging(skip, take).ToList();
        }
        public IActionResult OnPost(string query)
        {
            //Uri uri = new Uri($"{MainSite}/admin/search/{query}");
            //return Redirect(uri.AbsoluteUri);
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        
        public IActionResult OnGetDelete(int Id)
        {
            var pageGroup = _pageGroupRepository.SelectById(Id);
            _pageGroupRepository.Delete(pageGroup);
            _pageGroupRepository.Save();
            Message = "گروه با موفقیت حذف شد";
            return Redirect("/admin/mastergroup");
        }
    }
}