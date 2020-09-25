using OrzhansJozve.DomainClass.Domain;
using OrzhansJozve.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrzhansJozve.DataLayer.Context;

namespace OrzhansJozve.DataLayer.Services
{
    public class InsideAticelImageService : IInsideAticelImageRepository
    {
        private OrzhansJozve_DbContext _context;
        public InsideAticelImageService(OrzhansJozve_DbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
        public int AllInsideAticelImageCount()
        {
            return _context.InsideAticelImage.Count();
        }

        public void Delete(InsideAticelImage insideAticelImage)
        {
            _context.InsideAticelImage.Remove(insideAticelImage);
        }

        public void Delete(int id)
        {
            var insideAticelImage = SelectInsideAticelImageById(id);
            Delete(insideAticelImage);
        }


        public void Insert(InsideAticelImage insideAticelImage)
        {
            _context.InsideAticelImage.Add(insideAticelImage);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<InsideAticelImage> SelectAllInsideAticelImage()
        {
            return _context.InsideAticelImage.ToList();
        }

        public IEnumerable<InsideAticelImage> SelectAllInsideAticelImageForPaging(int skip, int take)
        {
            return _context.InsideAticelImage.OrderByDescending(i => i.InsideAticelImageCreateDate).Skip(skip).Take(take).ToList();
        }

        public InsideAticelImage SelectInsideAticelImageById(int id)
        {
            return _context.InsideAticelImage.Find(id);
        }

        public void Update(InsideAticelImage insideAticelImage)
        {
            _context.Entry(insideAticelImage).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
