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
    public class AdminAddPageGroupModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public PageGroup PageGroupModel { get; set; }
        public List<MasterPageGroup> MasterPageGroupList { get; set; }
        private IPageGroupRepository _pageGroupRepository { get; set; }
        private IMasterPageRepository _masterPageRepository { get; set; }
        public AdminAddPageGroupModel(IPageGroupRepository pageGroupRepository,IMasterPageRepository masterPageRepository)
        {
            _pageGroupRepository = pageGroupRepository;
            _masterPageRepository = masterPageRepository;
        }
        public void OnGet()
        {
            MasterPageGroupList = _masterPageRepository.SelectAll().ToList();
        }
        public IActionResult OnPost(string query)
        {
            //Uri uri = new Uri($"{MainSite}/admin/search/{query}");
            //return Redirect(uri.AbsoluteUri);
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        
        public IActionResult OnPostAddPageGroup()
        {
            if (ModelState.IsValid)
            {
                PageGroupModel.PageGroupCreateDate = DateTime.Now;
                _pageGroupRepository.Insert(PageGroupModel);
                _pageGroupRepository.Save();
                Message = "گروه با موفقیت منتشر شد";
                return Redirect("/admin/pagegroup");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}