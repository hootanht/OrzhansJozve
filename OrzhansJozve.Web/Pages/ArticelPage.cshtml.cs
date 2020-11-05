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
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.Web.Pages
{
    public class ArticelPageModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [TempData]
        public string reCAPTCHAMessage { get; set; }
        public string MainSite { get; set; }
        public OrzhansJozve.DomainClass.Domain.Page SingelPage { get; set; }
        public List<Comment> CommentsList { get; set; }
        [BindProperty]
        public Comment Comment { get; set; }
        public List<OrzhansJozve.DomainClass.Domain.Page> SimilarPageList { get; set; }
        public IPageRepository _pageRepository { get; set; }
        public IPageGroupRepository _pageGroupRepository { get; set; }
        public ICommentRepository _commentRepository { get; set; }
        public PageGroup SingelPageGroup { get; set; }
        private readonly ICaptchaValidator _captchaValidator;
        public ArticelPageModel(IPageRepository pageRepository, IPageGroupRepository pageGroupRepository, ICommentRepository commentRepository, ICaptchaValidator captchaValidator)
        {
            _pageRepository = pageRepository;
            _pageGroupRepository = pageGroupRepository;
            _commentRepository = commentRepository;
            _captchaValidator = captchaValidator;
            MainSite = "https://orzhansjozve.ir";
        }
        public IActionResult OnGet(int pageId, string title)
        {
            if (_pageRepository.PageExist(pageId, title))
            {
                SingelPage = _pageRepository.SelectById(pageId);
                SingelPageGroup = _pageGroupRepository.SelectById(SingelPage.PageGroupId);
                CommentsList = _commentRepository.GetAllCommentByPage(SingelPage.PageId).ToList();
                _pageRepository.IncreasePageView(pageId);
                _pageGroupRepository.IncreasePageGroupView(SingelPage.PageGroupId);
                SimilarPageList = _pageRepository.SelectSimilarPage(SingelPage.PageGroupId, 4).ToList();
                _pageGroupRepository.Save();
                _pageRepository.Save();
                return Page();
            }
            return NotFound();
        }


        public IActionResult OnPostAddNewsAgencyPeople(string email)
        {
            return RedirectToPage("AddNewsAgencyPeople", new { email = email });
        }


        public async Task<IActionResult> OnPostPushCooment(int pageId, string title, string captcha)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                reCAPTCHAMessage = "شما به عنوان ربات شناخته شدید لطفا دوباره تلاش کنید";
                return RedirectToPage();
            }
            if (ModelState.IsValid)
            {
                Comment.CommentCreateDate = DateTime.Now;
                Comment.CommentIsAccept = false;
                Comment.PageId = pageId;
                _commentRepository.Insert(Comment);
                _commentRepository.Save();
                Message = "نظر شما با موفقیت ثبت شد و پس از برسی منتشر می شود";
                try
                {
                    using (var message = new MailMessage())
                    {
                        using (var client = new SmtpClient("smtp.gmail.com"))
                        {
                            client.Port = 587;
                            client.Credentials = new NetworkCredential("orzhansjozve@gmail.com", "orzhansJozve2020");
                            client.EnableSsl = true;
                            //user
                            message.To.Add(new MailAddress(Comment.CommentAuthorEmail, Comment.CommentAuthorName));
                            message.From = new MailAddress("no-reply@orzhansjozve.ir", "no-reply");
                            message.Subject = "ثبت نظر";
                            message.Body = $"<!DOCTYPE html><html lang=\"en\"><head><title>Email Scotech</title></head><body style=\"text - align: center; \" dir=\"rtl\"><p>سلام {Comment.CommentAuthorName} نظر شما با موفقیت ثبت شد و پس از برسی منتشر می شود</p></body></html>";
                            message.IsBodyHtml = true;
                            await client.SendMailAsync(message);
                        }
                    }
                }
                catch (Exception)
                {
                    return RedirectToPage();
                }
            }
            return RedirectToPage();
        }
    }
}