using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Repositories
{
    public interface IAdsRepository
    {
        IEnumerable<Ads> SelectAllAds();
        int SelectAllAdsCount();
        bool TopAdsExist();
        bool DownAdsExist();
        IEnumerable<Ads> SelectTopAds();
        IEnumerable<Ads> SelectDownAds();
        void Insert(Ads ads);
        void Update(Ads ads);
        void Delete(Ads ads);
        void DeleteById(int id);
        Ads SelectAdsById(int id);
        IEnumerable<Ads> SelectAdsForPagging(int skip, int take);
        void Save();
    }
}
