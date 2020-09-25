using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;
using OrzhansJozve.Utilities;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    [Authorize]
    public class AddNewArticelModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public OrzhansJozve.DomainClass.Domain.Page PageModel { get; set; }
        public MasterPageGroup MasterPageGroupModel { get; set; }
        public List<Author> AuthorList { get; set; }
        public List<PageGroup> PageGroupList { get; set; }
        public List<MasterPageGroup> MasterPageGroupList { get; set; }
        private IAuthorRepository _authorRepository { get; set; }
        private IMasterPageRepository _masterPageRepository { get; set; }
        private IPageGroupRepository _pageGroupRepository { get; set; }
        private IPageRepository _pageRepository { get; set; }
        public AddNewArticelModel(IAuthorRepository authorRepository, IPageGroupRepository pageGroupRepository, IPageRepository pageRepository, IMasterPageRepository masterPageRepository)
        {
            _authorRepository = authorRepository;
            _pageGroupRepository = pageGroupRepository;
            _pageRepository = pageRepository;
            _masterPageRepository = masterPageRepository;
        }
        public void OnGet()
        {
            AuthorList = _authorRepository.SelectAll().ToList();
            PageGroupList = _pageGroupRepository.SelectAll().ToList();
            MasterPageGroupList = _masterPageRepository.SelectAll().ToList();
        }
        public IActionResult OnPost(string query)
        {
            //Uri uri = new Uri($"{MainSite}/admin/search/{query}");
            //return Redirect(uri.AbsoluteUri);
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        public async Task<IActionResult> OnPostAdd(IFormFile mainImage, IFormFile secondImage,IFormFile podcast)
        {
            if (ModelState.IsValid)
            {
                PageModel.PageView = 0;
                PageModel.PageTags = _masterPageRepository.SelectMasterPageGroupByPageGroupId(PageModel.PageGroupId).MasterPageGroupTitle + "-" + _pageGroupRepository.SelectById(PageModel.PageGroupId).PageGroupTitle;
                PageModel.PageCreateDate = DateTime.Now;
                PageModel.TimeCreateString = DateTime.Now.ToShamsi().ToString();
                if (mainImage != null)
                {
                    string path = "wwwroot/Blog-Content/Articel-Images";
                    PageModel.PageImageUrl = Guid.NewGuid().ToString() + PageModel.PageTitle.Replace(" ", "-") + Path.GetExtension(mainImage.FileName);
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), path));
                    }
                    string mainImagePathSave = Path.Combine(Directory.GetCurrentDirectory(), path, PageModel.PageImageUrl);
                    using (var stream = new FileStream(mainImagePathSave, FileMode.Create))
                    {
                        await mainImage.CopyToAsync(stream);
                    }
                }
                if (secondImage != null)
                {
                    string path = "wwwroot/Blog-Content/Articel-Images";
                    PageModel.PageSecondImageUrl = Guid.NewGuid().ToString() + PageModel.PageTitle.Replace(" ", "-") + Path.GetExtension(secondImage.FileName);
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), path));
                    }
                    string secondImagePathSave = Path.Combine(Directory.GetCurrentDirectory(), path, PageModel.PageSecondImageUrl);
                    using (var stream = new FileStream(secondImagePathSave, FileMode.Create))
                    {
                        await secondImage.CopyToAsync(stream);
                    }
                }
                if (podcast != null)
                {
                    string path = "wwwroot/Blog-Content/Podcasts";
                    PageModel.PagePodcastUrl = Guid.NewGuid().ToString() + PageModel.PageTitle.Replace(" ", "-") + Path.GetExtension(podcast.FileName);
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), path));
                    }
                    string podcastPathSave = Path.Combine(Directory.GetCurrentDirectory(), path, PageModel.PagePodcastUrl);
                    using (var stream = new FileStream(podcastPathSave, FileMode.Create))
                    {
                        await podcast.CopyToAsync(stream);
                    }
                }
                PageModel.Shortkey = _pageRepository.GenerateShortKey();
                _pageRepository.Insert(PageModel);
                _pageRepository.Save();
                Message = "مقاله با موفقیت ایجاد شد";
                return Redirect("/admin/articel");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}