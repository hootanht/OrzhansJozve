using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;
using OrzhansJozve.Web.SiteMap;

namespace OrzhansJozve.Web.SiteMap
{
    [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any, NoStore = false)]
    public class SiteMapModel : PageModel
    {
        private IPageRepository _pageRepository { get; set; }
        private IPageGroupRepository _pageGroupRepository { get; set; }
        private IMasterPageRepository _masterPageRepository { get; set; }
        public string MainSite { get; set; }
        public SiteMapModel(IPageRepository pageRepository, IPageGroupRepository pageGroupRepository, IMasterPageRepository masterPageRepository)
        {
            _pageRepository = pageRepository;
            _pageGroupRepository = pageGroupRepository;
            _masterPageRepository = masterPageRepository;
            MainSite = "https://orzhansjozve.ir";
        }
        public ContentResult OnGet()
        {
            // list of items to add
            var posts = _pageRepository.SelectAllForShowInNews().ToList();
            var pageGroups = _pageGroupRepository.SelectAll().ToList();
            var masterPageGroups = _masterPageRepository.SelectAll().ToList();
            var siteMapBuilder = new SiteMapBuilder();

            //add other link of page to the sitemap
            siteMapBuilder.AddUrl(MainSite, DateTime.Parse("2020-04-9"), ChangeFrequency.Daily, 1.0);
            siteMapBuilder.AddUrl(MainSite + "/aboutus", DateTime.Parse("2020-04-9"), ChangeFrequency.Daily, 1.0);
            siteMapBuilder.AddUrl(MainSite + "/contactus", DateTime.Parse("2020-04-9"), ChangeFrequency.Daily, 1.0);
            // add the blog posts to the sitemap
            foreach (var post in posts)
            {
                siteMapBuilder.AddUrl(MainSite + $"/articel/{post.PageId}/{post.PageTitle.Trim().Replace(" ", "-")}", modified: post.PageCreateDate, changeFrequency: ChangeFrequency.Daily, priority: 1.0);
            }
            // add pagegroups to the sitemap
            foreach (var pageGroup in pageGroups)
            {
                siteMapBuilder.AddUrl(MainSite + $"/PageGroup?query={pageGroup.PageGroupTitle.Trim().Replace(" ", "%20")}" , modified: pageGroup.PageGroupCreateDate, changeFrequency: ChangeFrequency.Daily, priority: 1.0);
            }
            // add masterpagegroups to the sitemap
            foreach (var masterPageGroup in masterPageGroups)
            {
                siteMapBuilder.AddUrl(MainSite + $"/MasterPageGroup?query={masterPageGroup.MasterPageGroupTitle.Trim().Replace(" ", "%20")}", modified: masterPageGroup.MasterPageGroupCreateDate, changeFrequency: ChangeFrequency.Daily, priority: 1.0);
            }
            //add shortlinks to sitemap
            foreach (var shortlink in _pageRepository.SelectAllForShowInNews().ToList())
            {
                siteMapBuilder.AddUrl($"{MainSite}/a/{shortlink.Shortkey}", shortlink.PageCreateDate, ChangeFrequency.Daily, 1.0);
            }
            // generate the sitemap xml
            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
        }
    }
}