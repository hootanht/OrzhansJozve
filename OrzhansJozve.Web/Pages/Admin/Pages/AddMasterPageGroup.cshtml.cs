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
    public class AddMasterPageGroupModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public MasterPageGroup MasterPageGroupModel { get; set; }
        private IMasterPageRepository _masterPageRepository { get; set; }
        public AddMasterPageGroupModel(IMasterPageRepository masterPageRepository)
        {
            _masterPageRepository = masterPageRepository;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost(string query)
        {
            //Uri uri = new Uri($"{MainSite}/admin/search/{query}");
            //return Redirect(uri.AbsoluteUri);
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        
        public IActionResult OnPostAddMasterPageGroup()
        {
            if (ModelState.IsValid)
            {
                MasterPageGroupModel.MasterPageGroupCreateDate = DateTime.Now;
                _masterPageRepository.Insert(MasterPageGroupModel);
                _masterPageRepository.Save();
                Message = "سر گروه با موفقیت منتشر شد";
                return Redirect("/admin/mastergroup");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}