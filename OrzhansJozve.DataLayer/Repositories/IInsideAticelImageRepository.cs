using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Repositories
{
    public interface IInsideAticelImageRepository
    {
        void Insert(InsideAticelImage insideAticelImage);
        IEnumerable<InsideAticelImage> SelectAllInsideAticelImage();
        IEnumerable<InsideAticelImage> SelectAllInsideAticelImageForPaging(int skip, int take);
        InsideAticelImage SelectInsideAticelImageById(int id);
        void Update(InsideAticelImage insideAticelImage);
        void Delete(InsideAticelImage insideAticelImage);
        void Delete(int id);
        int AllInsideAticelImageCount();
        void Save();
    }
}
