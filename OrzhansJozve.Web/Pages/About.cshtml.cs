using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OrzhansJozve.Web.Pages
{
    public class AboutModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [TempData]
        public string reCAPTCHAMessage { get; set; }
        public string MainSite { get; set; }
        private readonly ICaptchaValidator _captchaValidator;
        public AboutModel(ICaptchaValidator captchaValidator)
        {
            MainSite = "https://orzhansjozve.ir";
            _captchaValidator = captchaValidator;
        }
        public void OnGet()
        {

        }


        public IActionResult OnPostAddNewsAgencyPeople(string email)
        {
            return RedirectToPage("AddNewsAgencyPeople", new { email = email });
        }

        public string GetUri()
        {
            return Request.GetDisplayUrl().ToString();
        }
    }
}