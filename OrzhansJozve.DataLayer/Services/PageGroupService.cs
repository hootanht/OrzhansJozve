using Microsoft.EntityFrameworkCore.Metadata;
using OrzhansJozve.DataLayer.Context;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Services
{
    public class PageGroupService : IPageGroupRepository
    {
        private OrzhansJozve_DbContext _context;
        public PageGroupService(OrzhansJozve_DbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
        public void Delete(PageGroup pageGroup)
        {
            _context.Remove(pageGroup);
        }
        public IEnumerable<PageGroup> SelectAllFromTop(int take = 3)
        {
            return _context.PageGroups.OrderByDescending(p => p.PageGroupView).Take(take);
        }

        public void DelteById(int id)
        {
            var pageGroupItem = SelectById(id);
            Delete(pageGroupItem);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<PageGroup> SelectAll()
        {
            return _context.PageGroups.ToList();
        }

        public PageGroup SelectById(int id)
        {
            return _context.PageGroups.Find(id);
        }

        public void Update(PageGroup pageGroup)
        {
            _context.Entry(pageGroup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public int GetAllPageGroupNumber()
        {
            return _context.PageGroups.Count();
        }

        public void Insert(PageGroup pageGroup)
        {
            _context.PageGroups.Add(pageGroup);
        }

        public IEnumerable<PageGroup> SelectForPagging(int skip, int take)
        {
            return _context.PageGroups.OrderByDescending(p => p.PageGroupCreateDate).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<PageGroup> SelectAllByOrder()
        {
            return _context.PageGroups.OrderBy(p => p.PageGroupShowInMenuNumber).ToList();
        }

        public void IncreasePageGroupView(int pageGroupId)
        {
            var pageGroup = _context.PageGroups.Find(pageGroupId);
            pageGroup.PageGroupView++;
            Update(pageGroup);
        }

        public IEnumerable<PageGroup> GetAllPageGroupByMasterPageGroupFilter(int masterPageGroupId)
        {
            return _context.PageGroups.Where(p => p.MasterPageGroupId == masterPageGroupId).OrderBy(p => p.PageGroupShowInMenuNumber).ToList();
        }

        public bool IsPageGroupExist(string query)
        {
            return _context.PageGroups.Any(p => p.PageGroupTitle.Equals(query));
        }
    }
}
