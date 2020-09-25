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
    public class AdminEditPageGroupModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public PageGroup PageGroupModel { get; set; }
        public List<MasterPageGroup> MasterPageGroupList { get; set; }
        private IPageGroupRepository _pageGroupRepository { get; set; }
        private IMasterPageRepository _masterPageRepository { get; set; }
        public AdminEditPageGroupModel(IPageGroupRepository pageGroupRepository, IMasterPageRepository masterPageRepository)
        {
            _pageGroupRepository = pageGroupRepository;
            _masterPageRepository = masterPageRepository;
        }
        public void OnGet(int id)
        {
            PageGroupModel = _pageGroupRepository.SelectById(id);
            MasterPageGroupList = _masterPageRepository.SelectAll().ToList();
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        
        public IActionResult OnPostEditPageGroup(int id)
        {
            if (ModelState.IsValid)
            {
                PageGroupModel.PageGroupId = id;
                PageGroupModel.PageGroupCreateDate = DateTime.Now;
                var getMasterPageGroup = _masterPageRepository.SelectById(PageGroupModel.MasterPageGroupId);
                PageGroupModel.MasterPageGroup = getMasterPageGroup;
                _pageGroupRepository.Update(PageGroupModel);
                _pageGroupRepository.Save();
                Message = "گروه با موفقیت ویرایش شد";
                return Redirect("/admin/pagegroup");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}