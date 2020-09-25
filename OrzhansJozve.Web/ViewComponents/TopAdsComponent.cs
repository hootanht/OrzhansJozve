using Microsoft.AspNetCore.Mvc;
using OrzhansJozve.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.Web.ViewComponents
{
    public class TopAdsComponent:ViewComponent
    {
        private IAdsRepository _adsRepository;
        public TopAdsComponent(IAdsRepository adsRepository)
        {
            _adsRepository = adsRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("TopAdsComponent", _adsRepository.SelectTopAds()));
        }
    }
}
