using OrzhansJozve.DataLayer.Context;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Services
{
    public class PageService : IPageRepository
    {
        private OrzhansJozve_DbContext _context;
        public PageService(OrzhansJozve_DbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
        public void Delete(Page page)
        {
            _context.Remove(page);
        }
        public string GenerateShortKey(int lenght = 4)
        {
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, lenght);

            while (_context.Pages.Any(p => p.Shortkey == key))
            {
                key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, lenght);
            }

            return key;
        }

        public void DeleteById(int id)
        {
            var pageItem = SelectById(id);
            Delete(pageItem);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Page> SelectAll()
        {
            return _context.Pages.ToList();
        }

        public Page SelectById(int id)
        {
            return _context.Pages.Find(id);
        }
        public IEnumerable<Page> SelectAllForShowInTimeline()
        {
            return _context.Pages.Where(p => p.PageShow == false).ToList();
        }
        public IEnumerable<Page> SelectAllForShowInNews()
        {
            return _context.Pages.Where(p => p.PageShow == true);
        }
        public IEnumerable<Page> SelectTopPage(int take)
        {
            return _context.Pages.Where(p => p.PageShow == true).OrderByDescending(p => p.PageView).Take(take).ToList();
        }
        public IEnumerable<Page> SelectAllOrderByTime(int take)
        {
            return _context.Pages.Where(p => p.PageShow == true).OrderByDescending(p => p.PageCreateDate).Take(take).ToList();
        }
        public int GetAllPageViewer()
        {
            return _context.Pages.Where(p => p.PageShow == true).Sum(p => p.PageView);
        }
        public void Update(Page page)
        {
            _context.Entry(page).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public int GetAllPageAcceptNumber()
        {
            return _context.Pages.Where(p => p.PageShow == true).Count();
        }
        public int GetAllPageNotAcceptNumber()
        {
            return _context.Pages.Where(p => p.PageShow == false).Count();
        }

        public void Insert(Page page)
        {
            _context.Pages.Add(page);
        }

        public IEnumerable<Page> SelectForPagging(int skip, int take)
        {
            return _context.Pages.Where(p => p.PageShow == true).OrderByDescending(p => p.PageCreateDate).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<Page> SelectForPaggingNotAccept(int skip, int take)
        {
            return _context.Pages.Where(p => p.PageShow == false).OrderByDescending(p => p.PageCreateDate).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<Page> SelectAllByOrder()
        {
            return _context.Pages.OrderBy(p => p.PageCreateDate).ToList();
        }

        public bool PageExist(int id)
        {
            return _context.Pages.Any(p => p.PageId == id);
        }

        public bool IncreasePageView(int pageId)
        {
            var page = _context.Pages.Find(pageId);
            page.PageView++;
            Update(page);
            return true;
        }

        public IEnumerable<Page> SelectSimilarPage(int pageGroupId, int take)
        {
            var MasterPageGroupId = _context.PageGroups.Find(pageGroupId).MasterPageGroupId;
            return _context.Pages.Where(p => p.PageGroup.MasterPageGroupId == MasterPageGroupId && p.PageShow == true).OrderBy(p => Guid.NewGuid()).Take(take).ToList();
        }

        public bool PageExistByShoertkey(string shortKey)
        {
            return _context.Pages.Any(p => p.Shortkey == shortKey);
        }

        public Page SelectPageByShortKey(string shortKey)
        {
            return _context.Pages.SingleOrDefault(p => p.Shortkey == shortKey);
        }

        public IEnumerable<Page> GetAllPageAcceptByFilter(string query, int skip, int take)
        {
            return _context.Pages.Where(p => (p.PageCreateDate.ToString().Contains(query) || p.PageKeyWords.Contains(query) || p.PagePodcastAuthor.Contains(query) || p.PageShortDiscription.Contains(query) || p.PageTags.Contains(query) || p.PageTitle.Contains(query) || p.Author.AuthorName.Contains(query) || p.TimeCreateString.Contains(query) || p.PageVideoTitle.Contains(query)) && p.PageShow == true).Distinct().OrderByDescending(p => p.PageCreateDate).Skip(skip).Take(take).ToList();
        }

        public int GetAllPageAcceptByFilterNumber(string query)
        {
            return _context.Pages.Where(p => (p.PageCreateDate.ToString().Contains(query) || p.PageKeyWords.Contains(query) || p.PagePodcastAuthor.Contains(query) || p.PageShortDiscription.Contains(query) || p.PageTags.Contains(query) || p.PageTitle.Contains(query) || p.Author.AuthorName.Contains(query) || p.TimeCreateString.Contains(query) || p.PageVideoTitle.Contains(query)) && p.PageShow == true).Distinct().OrderByDescending(p => p.PageCreateDate).Count();
        }

        public IEnumerable<Page> GetAllPageByPageGroupFilter(string query, int skip, int take)
        {
            return _context.Pages.Where(p => p.PageGroup.PageGroupTitle == query && p.PageShow == true).OrderByDescending(p => p.PageCreateDate).Skip(skip).Take(take).ToList();
        }

        public int GetAllPageByPageGroupFilterNumber(string query)
        {
            return _context.Pages.Where(p => p.PageGroup.PageGroupTitle == query && p.PageShow == true).Count();
        }

        public IEnumerable<Page> GetAllMasterPageGroupByMasterPageGroupFilter(string query, int skip, int take)
        {
            var masterPageId = _context.MasterPageGroups.FirstOrDefault(m => m.MasterPageGroupTitle == query).MasterPageGroupId;
            return _context.Pages.Where(p => p.PageGroup.MasterPageGroupId == masterPageId && p.PageShow == true).OrderByDescending(p => p.PageCreateDate).Skip(skip).Take(take).ToList();
        }

        public int GetAllMasterPageGroupByMasterPageGroupFilterNumber(string query)
        {
            var masterPageId = _context.MasterPageGroups.FirstOrDefault(m => m.MasterPageGroupTitle == query).MasterPageGroupId;
            return _context.Pages.Where(p => p.PageGroup.MasterPageGroupId == masterPageId && p.PageShow == true).Count();
        }

        public bool PageExist(string query)
        {
            return _context.Pages.Any(p => (p.PageCreateDate.ToString().Contains(query) || p.PageKeyWords.Contains(query) || p.PagePodcastAuthor.Contains(query) || p.PageShortDiscription.Contains(query) || p.PageTags.Contains(query) || p.PageTitle.Contains(query) || p.Author.AuthorName.Contains(query) || p.TimeCreateString.Contains(query) || p.PageVideoTitle.Contains(query)) && p.PageShow == true);
        }

        public IEnumerable<Page> GetAllPageForAdminPanel(string query, int skip, int take)
        {
            return _context.Pages.Where(p => (p.PageCreateDate.ToString().Contains(query) || p.PageKeyWords.Contains(query) || p.PagePodcastAuthor.Contains(query) || p.PageShortDiscription.Contains(query) || p.PageTags.Contains(query) || p.PageTitle.Contains(query) || p.Author.AuthorName.Contains(query) || p.TimeCreateString.Contains(query) || p.PageVideoTitle.Contains(query))).Distinct().OrderByDescending(p => p.PageCreateDate).Skip(skip).Take(take).ToList();
        }

        public int GetAllPageForAdminPanelNumber(string query)
        {
            return _context.Pages.Where(p => (p.PageCreateDate.ToString().Contains(query) || p.PageKeyWords.Contains(query) || p.PagePodcastAuthor.Contains(query) || p.PageShortDiscription.Contains(query) || p.PageTags.Contains(query) || p.PageTitle.Contains(query) || p.Author.AuthorName.Contains(query) || p.TimeCreateString.Contains(query) || p.PageVideoTitle.Contains(query))).Distinct().OrderByDescending(p => p.PageCreateDate).Count();
        }

        public bool PageExistAdmin(string query)
        {
            return _context.Pages.Any(p => (p.PageCreateDate.ToString().Contains(query) || p.PageKeyWords.Contains(query) || p.PagePodcastAuthor.Contains(query) || p.PageShortDiscription.Contains(query) || p.PageTags.Contains(query) || p.PageTitle.Contains(query) || p.Author.AuthorName.Contains(query) || p.TimeCreateString.Contains(query) || p.PageVideoTitle.Contains(query)));
        }

        public bool PageExist(int pageId, string title)
        {
            return _context.Pages.Any(p => p.PageId == pageId && p.PageTitle == title.Replace("-"," "));
        }
    }
}
