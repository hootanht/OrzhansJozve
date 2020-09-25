using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Repositories
{
    public interface IPageRepository
    {
        IEnumerable<Page> SelectAll();
        IEnumerable<Page> SelectAllByOrder();
        IEnumerable<Page> SelectAllForShowInTimeline();
        IEnumerable<Page> SelectAllForShowInNews();
        IEnumerable<Page> SelectTopPage(int take);
        IEnumerable<Page> SelectAllOrderByTime(int take);
        IEnumerable<Page> SelectForPagging(int skip, int take);
        IEnumerable<Page> SelectForPaggingNotAccept(int skip, int take);
        IEnumerable<Page> SelectSimilarPage(int pageGroupId, int take);
        IEnumerable<Page> GetAllPageAcceptByFilter(string query, int skip, int take);
        IEnumerable<Page> GetAllPageByPageGroupFilter(string query, int skip, int take);
        IEnumerable<Page> GetAllMasterPageGroupByMasterPageGroupFilter(string query, int skip, int take);
        IEnumerable<Page> GetAllPageForAdminPanel(string query, int skip, int take);
        bool PageExistByShoertkey(string shortKey);
        Page SelectPageByShortKey(string shortKey);
        int GetAllMasterPageGroupByMasterPageGroupFilterNumber(string query);
        int GetAllPageByPageGroupFilterNumber(string query);
        int GetAllPageAcceptByFilterNumber(string query);
        int GetAllPageForAdminPanelNumber(string query);
        int GetAllPageViewer();
        int GetAllPageAcceptNumber();
        int GetAllPageNotAcceptNumber();
        bool IncreasePageView(int pageId);
        Page SelectById(int id);
        void Insert(Page page);
        void Update(Page page);
        void Delete(Page page);
        void DeleteById(int id);
        bool PageExist(int id);
        bool PageExist(string query);
        bool PageExist(int pageId, string title);
        bool PageExistAdmin(string query);
        string GenerateShortKey(int lenght = 4);
        void Save();
    }
}
