using OrzhansJozve.DataLayer.Context;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Services
{
    public class NewsAgencyPeopleServices : INewsAgencyPeopleRepository
    {
        private OrzhansJozve_DbContext _context;
        public NewsAgencyPeopleServices(OrzhansJozve_DbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public void Insert(NewsAgencyPeople people)
        {
            _context.NewsAgencyPeople.Add(people);
        }

        public bool NewsAgencyPeopleExist(string email)
        {
            return _context.NewsAgencyPeople.Any(n => n.Email == email);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<NewsAgencyPeople> SelectAll()
        {
           return _context.NewsAgencyPeople.ToList();
        }

        public NewsAgencyPeople SelectById(int id)
        {
            return _context.NewsAgencyPeople.Find(id);
        }
    }
}
