using OrzhansJozve.DataLayer.Context;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Services
{
    public class MasterPageGroupService : IMasterPageRepository
    {
        private OrzhansJozve_DbContext _context;
        public MasterPageGroupService(OrzhansJozve_DbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public void Delete(MasterPageGroup masterPageGroup)
        {
            _context.Remove(masterPageGroup);
        }

        public void DelteById(int id)
        {
            var masterPageGroupItem = SelectById(id);
            Delete(masterPageGroupItem);
        }

        public int GetAllMasterPageGroupNumber()
        {
            return _context.MasterPageGroups.Count();
        }

        public void Insert(MasterPageGroup masterPageGroup)
        {
            _context.MasterPageGroups.Add(masterPageGroup);
        }

        public bool PageGroupExist(int masterPageGroupId)
        {
            return _context.PageGroups.Any(p => p.MasterPageGroupId == masterPageGroupId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<MasterPageGroup> SelectAll()
        {
            return _context.MasterPageGroups.ToList();
        }

        public IEnumerable<MasterPageGroup> SelectAllByOrder()
        {
            return _context.MasterPageGroups.OrderBy(p => p.ShowInMenuNumber).ToList();
        }

        public MasterPageGroup SelectById(int id)
        {
            return _context.MasterPageGroups.Find(id);
        }

        public IEnumerable<MasterPageGroup> SelectForPagging(int skip, int take)
        {
            return _context.MasterPageGroups.OrderByDescending(p => p.MasterPageGroupCreateDate).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<PageGroup> SelectPageGroupByMasterPageGroupFilter(int masterPageGroupId)
        {
            return _context.PageGroups.Where(p => p.MasterPageGroupId == masterPageGroupId).ToList();
        }

        public void Update(MasterPageGroup masterPageGroup)
        {
            _context.Entry(masterPageGroup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public MasterPageGroup SelectMasterPageGroupByPageGroupId(int pageGroupId)
        {
            var masterPageGroupId = _context.PageGroups.FirstOrDefault(p => p.PageGroupId == pageGroupId).MasterPageGroupId;
            return _context.MasterPageGroups.Find(masterPageGroupId);
        }

        public bool PageGroupExist(string query)
        {
            return _context.MasterPageGroups.Any(m => m.MasterPageGroupTitle.Equals(query));
        }
    }
}
