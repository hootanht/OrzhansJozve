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
    public class AdminEditMasterPageGroupModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public MasterPageGroup MasterPageGroupModel { get; set; }
        private IMasterPageRepository _MasterPageRepository { get; set; }
        public AdminEditMasterPageGroupModel(IMasterPageRepository masterPageRepository)
        {
            _MasterPageRepository = masterPageRepository;
        }
        public void OnGet(int id)
        {
            MasterPageGroupModel = _MasterPageRepository.SelectById(id);
        }
        public IActionResult OnPost(string query)
        {
            //Uri uri = new Uri($"{MainSite}/admin/search/{query}");
            //return Redirect(uri.AbsoluteUri);
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        public IActionResult OnPostEditMasterPageGroup(int id)
        {
            if (ModelState.IsValid)
            {
                MasterPageGroupModel.MasterPageGroupId = id;
                MasterPageGroupModel.MasterPageGroupCreateDate = DateTime.Now;
                _MasterPageRepository.Update(MasterPageGroupModel);
                _MasterPageRepository.Save();
                Message = "سرگروه با موفقیت ویرایش شد";
                return Redirect("/admin/mastergroup");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}