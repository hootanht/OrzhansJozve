using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;

namespace OrzhansJozve.Web.Pages
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [TempData]
        public string reCAPTCHAMessage { get; set; }
        public string Query { get; set; }
        public string MainSite { get; set; }
        public List<OrzhansJozve.DomainClass.Domain.Page> Pages { get; set; }
        private IPageRepository _pageRepository { get; set; }
        private IPageGroupRepository _pageGroupRepository { get; set; }
        private IMasterPageRepository _masterPageRepository { get; set; }
        private readonly ICaptchaValidator _captchaValidator;
        public IndexModel(IPageRepository pageRepository, ICaptchaValidator captchaValidator, IPageGroupRepository pageGroupRepository, IMasterPageRepository masterPageRepository)
        {
            _pageRepository = pageRepository;
            _pageGroupRepository = pageGroupRepository;
            _masterPageRepository = masterPageRepository;
            _captchaValidator = captchaValidator;
            MainSite = "https://orzhansjozve.ir";

        }
        public IActionResult OnGet(string handler, int pageId = 1)
        {
            if (handler != null && handler != "Page" && handler != "search" && handler != "PageGroup" && handler != "MasterPageGroup")
            {
                return NotFound();
            }
            int take = 6;
            int skip = (pageId - 1) * take;
            int Count = _pageRepository.GetAllPageAcceptNumber();
            ViewData["PageID"] = pageId;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            Pages = _pageRepository.SelectForPagging(skip, take).ToList();
            ViewData["Handler"] = "Page";
            ViewData["Title"] = "صفحه‌ اصلی‌";
            return Page();
        }
        public void OnGetPage(int pageId, string query)
        {
            int take = 6;
            int skip = (pageId - 1) * take;
            int Count = _pageRepository.GetAllPageAcceptNumber();
            ViewData["PageID"] = pageId;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            Pages = _pageRepository.SelectForPagging(skip, take).ToList();
            ViewData["Handler"] = "Page";
            ViewData["Title"] = "صفحه‌ اصلی‌";
        }
        public IActionResult OnPost(string query)
        {
            Uri uri = new Uri($"{MainSite}/search/{query.Trim().Replace(" ", "-").ToString()}");
            return Redirect(uri.AbsoluteUri);
        }
        public IActionResult OnGetPageGroup(string query, int pageId = 1)
        {
            if (_pageGroupRepository.IsPageGroupExist(query))
            {
                int take = 6;
                int skip = (pageId - 1) * take;
                int Count = _pageRepository.GetAllPageByPageGroupFilterNumber(query);
                ViewData["PageID"] = pageId;
                ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
                Pages = _pageRepository.GetAllPageByPageGroupFilter(query, skip, take).ToList();
                ViewData["Handler"] = "PageGroup";
                ViewData["Query"] = query;
                ViewData["Title"] = query;
                return Page();
            }
            return NotFound();
        }
        public IActionResult OnGetMasterPageGroup(string query, int pageId = 1)
        {
            if (_masterPageRepository.PageGroupExist(query))
            {
                int take = 6;
                int skip = (pageId - 1) * take;
                int Count = _pageRepository.GetAllMasterPageGroupByMasterPageGroupFilterNumber(query);
                ViewData["PageID"] = pageId;
                ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
                Pages = _pageRepository.GetAllMasterPageGroupByMasterPageGroupFilter(query, skip, take).ToList();
                ViewData["Handler"] = "MasterPageGroup";
                ViewData["Query"] = query;
                ViewData["Title"] = query;
                return Page();
            }
            return NotFound();
        }


        public async Task<IActionResult> OnPostAddNewsAgencyPeople(string email, string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                reCAPTCHAMessage = "شما به عنوان ربات شناخته شدید لطفا دوباره تلاش کنید";
                return Redirect("/");
            }
            return RedirectToPage("AddNewsAgencyPeople", new { email = email });
        }
        public string GetUri()
        {
            return Request.GetDisplayUrl().ToString();
        }
    }
}
