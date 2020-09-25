using Microsoft.EntityFrameworkCore;
using OrzhansJozve.DomainClass.Domain;
using OrzhansJozve.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrzhansJozve.DataLayer.Context;

namespace OrzhansJozve.DataLayer.Services
{
    public class AdsService : IAdsRepository
    {
        private OrzhansJozve_DbContext _context;

        public AdsService(OrzhansJozve_DbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
        public void Delete(Ads ads)
        {
            _context.Ads.Remove(ads);
        }

        public void DeleteById(int id)
        {
            var ad = SelectAdsById(id);
            Delete(ad);
        }

        public bool DownAdsExist()
        {
            return _context.Ads.Any(a => a.TopAds == false);
        }

        public void Insert(Ads ads)
        {
            _context.Ads.Add(ads);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Ads SelectAdsById(int id)
        {
            return _context.Ads.Find(id);
        }

        public IEnumerable<Ads> SelectAdsForPagging(int skip, int take)
        {
            return _context.Ads.OrderByDescending(a => a.AdsCreateDate).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<Ads> SelectAllAds()
        {
           return _context.Ads.OrderByDescending(a=>a.AdsCreateDate).ToList();
        }

        public int SelectAllAdsCount()
        {
            return _context.Ads.Count();
        }

        public IEnumerable<Ads> SelectDownAds()
        {
            return _context.Ads.Where(a => a.TopAds == false).OrderBy(a=>a.AdsRow);
        }

        public IEnumerable<Ads> SelectTopAds()
        {
            return _context.Ads.Where(a => a.TopAds == true).OrderBy(a => a.AdsRow);
        }

        public bool TopAdsExist()
        {
            return _context.Ads.Any(a => a.TopAds == true);
        }

        public void Update(Ads ads)
        {
            _context.Entry(ads).State = EntityState.Modified;
        }
    }
}
