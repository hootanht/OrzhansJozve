using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;

namespace OrzhansJozve.Web.Pages
{
    public class AddNewsAgencyPeopleModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [TempData]
        public string reCAPTCHAMessage { get; set; }
        private INewsAgencyPeopleRepository _newsAgencyPeopleRepository { get; set; }
        private readonly ICaptchaValidator _captchaValidator;
        public AddNewsAgencyPeopleModel(INewsAgencyPeopleRepository newsAgencyPeopleRepository , ICaptchaValidator captchaValidator)
        {
            _newsAgencyPeopleRepository = newsAgencyPeopleRepository;
            _captchaValidator = captchaValidator;
        }
        public async Task<IActionResult> OnGet(string email)
        {
            if (ModelState.IsValid)
            {
                if (!_newsAgencyPeopleRepository.NewsAgencyPeopleExist(email))
                {
                    _newsAgencyPeopleRepository.Insert(new OrzhansJozve.DomainClass.Domain.NewsAgencyPeople()
                    {
                        Email = email
                    });
                    _newsAgencyPeopleRepository.Save();
                    Message = "شما با موفقیت عضو سرویس خبری اورژانس جزوه شدید";
                    try
                    {
                        using (var usermessage = new MailMessage())
                        {
                            usermessage.To.Add(new MailAddress(email, email));
                            usermessage.From = new MailAddress("no-reply@orzhansjozve.ir", "no-reply");
                            usermessage.Subject = "عضویت در خبرگذاری";
                            usermessage.Body = $"<!DOCTYPE html><html lang=\"fa\"><head><title>Email Scotech</title></head><body style=\"text-align: right; \" dir=\"rtl\"><h3>با سلام {email} شما با موفقیت در سرویس خبرگذاری اورژانس جزوه عضو شدید.</h3></body></html>";
                            usermessage.IsBodyHtml = true;

                            using (var client = new SmtpClient("smtp.gmail.com"))
                            {
                                client.Port = 587;
                                client.Credentials = new NetworkCredential("orzhansjozve@gmail.com", "orzhansJozve2021");
                                client.EnableSsl = true;
                                await client.SendMailAsync(usermessage);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return RedirectToPage("Index");
                    }
                }
                else
                {
                    Message = "شما در خبرگزاری اورژانس جزوه عضو هستید";
                }
            }
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPost(string email)
        {
            if (ModelState.IsValid)
            {
                _newsAgencyPeopleRepository.Insert(new OrzhansJozve.DomainClass.Domain.NewsAgencyPeople()
                {
                    Email = email
                });
                Message = "شما با موفقیت عضو سرویس خبری اورژانس جزوه شدید";
                try
                {
                    using (var usermessage = new MailMessage())
                    {
                        usermessage.To.Add(new MailAddress(email, email));
                        usermessage.From = new MailAddress("no-reply@orzhansjozve.ir", "no-reply");
                        usermessage.Subject = "عضویت در خبرگذاری";
                        usermessage.Body = $"<!DOCTYPE html><html lang=\"fa\"><head><title>Email Scotech</title></head><body style=\"text-align: center; \" dir=\"rtl\"><h3>با سلام {email} شما با موفقیت در سرویس خبرگذاری اورژانس جزوه عضو شدید.</h3></body></html>";
                        usermessage.IsBodyHtml = true;

                        using (var client = new SmtpClient("smtp.gmail.com"))
                        {
                            client.Port = 587;
                            client.Credentials = new NetworkCredential("orzhansjozve@gmail.com", "orzhansJozve2021");
                            client.EnableSsl = true;
                            await client.SendMailAsync(usermessage);
                        }
                    }
                }
                catch (Exception)
                {
                    return RedirectToPage("Index");
                }
            }
            return RedirectToPage("Index");
        }
    }
}