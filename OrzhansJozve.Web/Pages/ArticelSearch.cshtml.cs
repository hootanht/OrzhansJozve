using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;

namespace OrzhansJozve.Web.Pages
{
    public class ArticelSearchModel : PageModel
    {
        [TempData]
        public string reCAPTCHAMessage { get; set; }
        public List<OrzhansJozve.DomainClass.Domain.Page> Pages { get; set; }
        [BindProperty(Name = "s", SupportsGet = true)]
        public string Query { get; set; }
        private IPageRepository _pageRepository { get; set; }
        public string MainSite { get; set; }
        private readonly ICaptchaValidator _captchaValidator;
        public ArticelSearchModel(IPageRepository pageRepository, ICaptchaValidator captchaValidator)
        {
            _pageRepository = pageRepository;
            MainSite = "https://orzhansjozve.ir";
            _captchaValidator = captchaValidator;
        }
        public void OnGet(string s, int pageId = 1)
        {
            int take = 6;
            int skip = (pageId - 1) * take;
            int Count = _pageRepository.GetAllPageAcceptByFilterNumber(s.Replace("-", " "));
            ViewData["PageID"] = pageId;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            Pages = _pageRepository.GetAllPageAcceptByFilter(s.Replace("-", " "), skip, take).ToList();
            Query = s.Replace("-", " ");
            ViewData["Title"] = Query;
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

        public void OnGetSearch(string s, int pageId = 1)
        {
            int take = 6;
            int skip = (pageId - 1) * take;
            int Count = _pageRepository.GetAllPageAcceptByFilterNumber(s.Replace("-", " "));
            ViewData["PageID"] = pageId;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            Pages = _pageRepository.GetAllPageAcceptByFilter(s.Replace("-", " "), skip, take).ToList();
            Query = s.Replace("-", " ");
            ViewData["Title"] = Query;
        }

        public string GetUri()
        {
            return Request.GetDisplayUrl().ToString();
        }
    }
}