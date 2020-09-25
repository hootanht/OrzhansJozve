using OrzhansJozve.DataLayer.Context;
using OrzhansJozve.DomainClass.Domain;
using OrzhansJozve.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Services
{
    public class AuthorService : IAuthorRepository
    {
        private OrzhansJozve_DbContext _context;
        public AuthorService(OrzhansJozve_DbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
        public void Delete(Author author)
        {
            _context.Remove(author);
        }

        public void DelteById(int id)
        {
            var authorItem = SelectById(id);
            Delete(authorItem);
        }

        public int GetAllAuthorNumber()
        {
            return _context.Authors.Count();
        }

        public void Insert(Author author)
        {
            _context.Authors.Add(author);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Author> SelectAll()
        {
            return _context.Authors.ToList();
        }

        public IEnumerable<Author> SelectAllByOrder()
        {
            return _context.Authors.OrderBy(p => p.AuthorCreateDate).ToList();
        }

        public Author SelectById(int id)
        {
            return _context.Authors.Find(id);
        }

        public IEnumerable<Author> SelectForPagging(int skip, int take)
        {
            return _context.Authors.OrderByDescending(p => p.AuthorCreateDate).Skip(skip).Take(take).ToList();
        }

        public void Update(Author author)
        {
            _context.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
