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
    public class AdminCommentAcceptModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public string MainSite { get; set; }
        public List<Comment> CommentsList { get; set; }
        private ICommentRepository _commentRepository { get; set; }
        private IPageRepository _pageRepository { get; set; }
        public AdminCommentAcceptModel(ICommentRepository commentRepository, IPageRepository pageRepository)
        {
            _commentRepository = commentRepository;
            _pageRepository = pageRepository;
            MainSite = "https://orzhansjozve.ir";
        }
        public void OnGet(int pageid = 1)
        {
            int take = 6;
            int skip = (pageid - 1) * take;
            int Count = _commentRepository.GetAllCommentNotAcceptNumber();
            ViewData["PageID"] = pageid;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            CommentsList = _commentRepository.SelectForPaggingNotAccept(skip, take).ToList();
        }


        public IActionResult OnGetDelete(int Id)
        {
            var comment = _commentRepository.SelectById(Id);
            _commentRepository.Delete(comment);
            _commentRepository.Save();
            Message = "نظر با موفقیت حذف شد";
            return Redirect("/admin/comment");
        }

        public async Task<IActionResult> OnGetRelese(int Id)
        {
            var comment = _commentRepository.SelectById(Id);
            comment.CommentIsAccept = true;
            _commentRepository.Update(comment);
            _commentRepository.Save();
            try
            {
                using (var message = new MailMessage())
                {
                    message.To.Add(new MailAddress(comment.CommentAuthorEmail, comment.CommentAuthorEmail));
                    message.From = new MailAddress("orzhansjozve@gmail.com", "orzhansjozve");
                    message.Subject = "ثبت نظر";
                    message.Body = $"<!DOCTYPE html><html lang=\"en\"><head><title>Email Scotech</title></head><body style=\"text - align: center; \" dir=\"rtl\"><h2>سلام {comment.CommentAuthorName} نظر شما با موفقیت منتشر شد.</h2><h3><a href=\"{MainSite}/articel/{_pageRepository.SelectById(comment.PageId).PageId}/{_pageRepository.SelectById(comment.PageId).PageTitle.Trim().Replace(" ", "-")}\">مشاهده نظر</a></h3></body></html>";
                    message.IsBodyHtml = true;

                    using (var client = new SmtpClient("smtp.gmail.com"))
                    {
                        client.Port = 587;
                        client.Credentials = new NetworkCredential("orzhansjozve@gmail.com", "orzhansJozve2020");
                        client.EnableSsl = true;
                        await client.SendMailAsync(message);
                    }
                }
            }
            catch (Exception)
            {
                Message = "نظر با موفقیت منتشر شد";
                return Redirect("/admin/comment");
            }
            Message = "نظر با موفقیت منتشر شد";
            return Redirect("/admin/comment");
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        public IActionResult OnGetDeleteAllCommentsNotAccept(int pageid = 1)
        {
            int take = 6;
            int skip = (pageid - 1) * take;
            foreach (var comment in _commentRepository.SelectForPaggingNotAccept(skip, take).ToList())
            {
                _commentRepository.DelteById(comment.CommentId);
            }
            _commentRepository.Save();
            return Redirect("/admin/commentaccept");
        }
    }
}