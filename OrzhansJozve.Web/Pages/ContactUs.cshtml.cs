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
    [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Any, Duration = 604800)]
    public class ContactUsModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [TempData]
        public string reCAPTCHAMessage { get; set; }
        public string MainSite { get; set; }
        private readonly ICaptchaValidator _captchaValidator;
        public ContactUsModel(ICaptchaValidator captchaValidator)
        {
            MainSite = "https://orzhansjozve.ir";
            _captchaValidator = captchaValidator;
        }
        public void OnGet()
        {

        }

        
        public async Task<IActionResult> OnPostUploadUserComment(string name, string email, string subject, string message, string phone, string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                reCAPTCHAMessage = "شما به عنوان ربات شناخته شدید لطفا دوباره تلاش کنید";
                return RedirectToPage();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using (var usermessage = new MailMessage())
                    {
                        usermessage.To.Add(new MailAddress("orzhansjozve@gmail.com", "orzhansjozve"));
                        usermessage.From = new MailAddress(email, email);
                        usermessage.Subject = "تیکت";
                        usermessage.Body = $"<!DOCTYPE html><html lang=\"fa\"><head><title>Email Sepantab</title></head><body style=\"text-align: center; \" dir=\"rtl\"><h1>نام فرستنده : {name}</h1><h2>ایمیل فرستنده : {email}</h2><h3>موضوع تیکت : {subject}</h3><h4>شماره تلفن : {phone}</h4><p>{message}</p></body></html>";
                        usermessage.IsBodyHtml = true;

                        using (var client = new SmtpClient("smtp.gmail.com"))
                        {
                            client.Port = 587;
                            client.Credentials = new NetworkCredential("orzhansjozve@gmail.com", "orzhansJozve2020");
                            client.EnableSsl = true;
                            await client.SendMailAsync(usermessage);
                            Message = "ممنون از این که ما را در راستای بهبود این سایت کمک می کنید";
                        }
                    }
                }
                catch (Exception)
                {
                    return Redirect("/contactus");
                }
            }
            return Redirect("/contactus");
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