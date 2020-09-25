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
    public class AdminMasterGroupModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<MasterPageGroup> MasterPageGroups { get; set; }
        private IMasterPageRepository _masterPageGroupRepository { get; set; }
        public AdminMasterGroupModel(IMasterPageRepository masterPageGroupRepository)
        {
            _masterPageGroupRepository = masterPageGroupRepository;
        }
        public void OnGet(int pageid = 1)
        {
            int take = 6;
            int skip = (pageid - 1) * take;
            int Count = _masterPageGroupRepository.GetAllMasterPageGroupNumber();
            ViewData["PageID"] = pageid;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            MasterPageGroups = _masterPageGroupRepository.SelectForPagging(skip, take).ToList();
        }
        public IActionResult OnPost(string query)
        {
            //Uri uri = new Uri($"{MainSite}/admin/search/{query}");
            //return Redirect(uri.AbsoluteUri);
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        
        public IActionResult OnGetDelete(int Id)
        {
            var masterPageGroup = _masterPageGroupRepository.SelectById(Id);
            _masterPageGroupRepository.Delete(masterPageGroup);
            _masterPageGroupRepository.Save();
            Message = "سرگروه با موفقیت حذف شد";
            return Redirect("/admin/mastergroup");
        }
    }
}