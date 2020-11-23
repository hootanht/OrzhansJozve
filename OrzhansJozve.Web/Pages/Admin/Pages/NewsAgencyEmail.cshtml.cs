using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    [Authorize]
    public class NewsAgencyEmailModel : PageModel
    {
        private INewsAgencyPeopleRepository _newsAgencyPeopleRepository { get; set; }
        [TempData]
        public string Message { get; set; }
        public NewsAgencyEmailModel(INewsAgencyPeopleRepository newsAgencyPeopleRepository)
        {
            _newsAgencyPeopleRepository = newsAgencyPeopleRepository;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        public async Task<IActionResult> OnPostSendEmail(string emailtext, string emailsubject)
        {
            foreach (var people in _newsAgencyPeopleRepository.SelectAll().ToList())
            {
                using (var usermessage = new MailMessage())
                {
                    usermessage.To.Add(new MailAddress(people.Email, people.Email));
                    usermessage.From = new MailAddress("orzhansjozve@gmail.com", "orzhansjozve");
                    usermessage.Subject = emailsubject;
                    usermessage.Body = emailtext;
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
            Message = "تمامی ایمیل ها با موفقیت ارسال شد";
            return Redirect("/admin/articel");
        }
    }
}