using OrzhansJozve.DataLayer.Context;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Services
{
    public class FooterLinkService : IFooterLinkRepository
    {
        private OrzhansJozve_DbContext _context;
        public FooterLinkService(OrzhansJozve_DbContext context)
        {
            _context = context;
        }
        public int AllFooterLinksCount()
        {
            return _context.FooterLinks.Count();
        }

        public void Delete(FooterLink footerLink)
        {
            _context.FooterLinks.Remove(footerLink);
        }

        public void Delete(int id)
        {
            var footerLink = SelectFooterLinkById(id);
            Delete(footerLink);
        }


        public void Insert(FooterLink footerLink)
        {
            _context.FooterLinks.Add(footerLink);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<FooterLink> SelectAllFooterLinks()
        {
            return _context.FooterLinks.ToList();
        }

        public IEnumerable<FooterLink> SelectAllFooterLinksForPaging(int skip, int take)
        {
            return _context.FooterLinks.OrderByDescending(f => f.FooterLinkCreateDate).Skip(skip).Take(take).ToList();
        }

        public FooterLink SelectFooterLinkById(int id)
        {
            return _context.FooterLinks.Find(id);
        }

        public void Update(FooterLink footerLink)
        {
            _context.Entry(footerLink).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public bool IsColumnOneExist()
        {
            return _context.FooterLinks.Any(f => f.FooterLinkColumn == 1);
        }

        public bool IsColumnTwoExist()
        {
            return _context.FooterLinks.Any(f => f.FooterLinkColumn == 2);
        }
        public bool FooterLinkExist()
        {
           return _context.FooterLinks.Any();
        }

    }
}
