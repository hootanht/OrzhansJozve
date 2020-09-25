using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Repositories
{
    public interface IPageGroupRepository
    {
        IEnumerable<PageGroup> SelectAll();
        IEnumerable<PageGroup> SelectAllByOrder();
        PageGroup SelectById(int id);
        IEnumerable<PageGroup> SelectAllFromTop(int take);
        IEnumerable<PageGroup> SelectForPagging(int skip, int take);
        IEnumerable<PageGroup> GetAllPageGroupByMasterPageGroupFilter(int masterPageGroupId);
        void IncreasePageGroupView(int pageGroupId);
        bool IsPageGroupExist(string query);
        int GetAllPageGroupNumber();
        void Insert(PageGroup pageGroup);
        void Update(PageGroup pageGroup);
        void Delete(PageGroup pageGroup);
        void DelteById(int id);
        void Save();
    }
}
