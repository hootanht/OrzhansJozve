using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.DataLayer.Repositories
{
    public interface IMasterPageRepository
    {
        IEnumerable<MasterPageGroup> SelectAll();
        IEnumerable<MasterPageGroup> SelectAllByOrder();
        MasterPageGroup SelectMasterPageGroupByPageGroupId(int pageGroupId);
        MasterPageGroup SelectById(int id);
        int GetAllMasterPageGroupNumber();
        IEnumerable<MasterPageGroup> SelectForPagging(int skip, int take);
        bool PageGroupExist(int masterPageGroupId);
        bool PageGroupExist(string query);
        IEnumerable<PageGroup> SelectPageGroupByMasterPageGroupFilter(int masterPageGroupId);
        void Insert(MasterPageGroup masterPageGroup);
        void Update(MasterPageGroup masterPageGroup);
        void Delete(MasterPageGroup masterPageGroup);
        void DelteById(int id);
        void Save();
    }
}
