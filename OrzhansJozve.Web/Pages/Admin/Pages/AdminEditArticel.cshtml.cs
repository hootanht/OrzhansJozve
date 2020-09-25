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
    public class AdminEditArticelModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public OrzhansJozve.DomainClass.Domain.Page PageModel { get; set; }
        public MasterPageGroup MasterPageGroupModel { get; set; }
        public List<Author> AuthorList { get; set; }
        public List<PageGroup> pageGroupList { get; set; }
        public List<MasterPageGroup> MasterPageGroupList { get; set; }
        private IAuthorRepository _authorRepository { get; set; }
        private IMasterPageRepository _masterPageRepository { get; set; }
        private IPageGroupRepository _pageGroupRepository { get; set; }
        private IPageRepository _pageRepository { get; set; }
        public AdminEditArticelModel(IAuthorRepository authorRepository, IPageGroupRepository pageGroupRepository, IPageRepository pageRepository, IMasterPageRepository masterPageRepository)
        {
            _authorRepository = authorRepository;
            _pageGroupRepository = pageGroupRepository;
            _pageRepository = pageRepository;
            _masterPageRepository = masterPageRepository;
        }
        public void OnGet(int id)
        {
            PageModel = _pageRepository.SelectById(id);
            AuthorList = _authorRepository.SelectAll().ToList();
            pageGroupList = _pageGroupRepository.SelectAll().ToList();
            MasterPageGroupList = _masterPageRepository.SelectAll().ToList();
        }
        public IActionResult OnPost(string query)
        {
            //Uri uri = new Uri($"{MainSite}/admin/search/{query}");
            //return Redirect(uri.AbsoluteUri);
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        public IActionResult OnGetDeletePodcast(int Id)
        {
            var selectedPage = _pageRepository.SelectById(Id);
            string path = "wwwroot/Blog-Content/Podcasts";
            if (selectedPage.PagePodcastUrl != null)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path, selectedPage.PagePodcastUrl)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), path, selectedPage.PagePodcastUrl));
                    selectedPage.PagePodcastUrl = null;
                    selectedPage.PagePodcastAuthor = null;
                }
            }
            _pageRepository.Update(selectedPage);
            _pageRepository.Save();
            Message = "پادکست مقاله با موفقیت حذف شد";
            return Redirect("/admin/articel");
        }
        public async Task<IActionResult> OnPostEdit(int Id, IFormFile mainImage, IFormFile secondImage, IFormFile podcast)
        {
            if (ModelState.IsValid)
            {
                PageModel.PageId = Id;
                PageModel.PageTags = _masterPageRepository.SelectMasterPageGroupByPageGroupId(PageModel.PageGroupId).MasterPageGroupTitle + "-" + _pageGroupRepository.SelectById(PageModel.PageGroupId).PageGroupTitle;
                PageModel.PageCreateDate = DateTime.Now;
                PageModel.TimeCreateString = DateTime.Now.ToShamsi().ToString();
                var selectedPage = _pageRepository.SelectById(Id);
                if (mainImage != null)
                {
                    string path = "wwwroot/Blog-Content/Articel-Images";
                    if (selectedPage.PageImageUrl != null)
                    {
                        if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path, selectedPage.PageImageUrl)))
                        {
                            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), path, selectedPage.PageImageUrl));
                        }
                    }
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
                else
                {
                    PageModel.PageImageUrl = selectedPage.PageImageUrl;
                }
                if (secondImage != null)
                {
                    string path = "wwwroot/Blog-Content/Articel-Images";
                    if (selectedPage.PageSecondImageUrl != null)
                    {
                        if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path, selectedPage.PageSecondImageUrl)))
                        {
                            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), path, selectedPage.PageSecondImageUrl));
                        }
                    }
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
                else
                {
                    PageModel.PageSecondImageUrl = selectedPage.PageSecondImageUrl;
                }
                if (podcast != null)
                {
                    string path = "wwwroot/Blog-Content/Podcasts";
                    if (selectedPage.PagePodcastUrl != null)
                    {
                        if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path, selectedPage.PagePodcastUrl)))
                        {
                            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), path, selectedPage.PagePodcastUrl));
                        }
                    }
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
                else
                {
                    PageModel.PagePodcastUrl = selectedPage.PagePodcastUrl;
                }
                PageModel.PageView = _pageRepository.SelectById(Id).PageView;
                var getAuthor = _authorRepository.SelectById(PageModel.AuthorId);
                var getPageGroup = _pageGroupRepository.SelectById(PageModel.PageGroupId);
                PageModel.Author = getAuthor;
                PageModel.PageGroup = getPageGroup;
                PageModel.Shortkey = _pageRepository.SelectById(Id).Shortkey;
                if (string.IsNullOrEmpty(PageModel.Shortkey))
                {
                    PageModel.Shortkey = _pageRepository.GenerateShortKey();
                }
                _pageRepository.Update(PageModel);
                _pageRepository.Save();
                Message = "مقاله با موفقیت ویرایش شد";
                return Redirect("/admin/articel");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}