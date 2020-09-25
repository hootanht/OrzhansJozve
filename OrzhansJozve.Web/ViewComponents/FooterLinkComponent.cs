using Microsoft.AspNetCore.Mvc;
using OrzhansJozve.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.Web.ViewComponents
{
    public class FooterLinkComponent : ViewComponent
    {
       private IFooterLinkRepository _footerLinkRepository;
        public FooterLinkComponent(IFooterLinkRepository footerLinkRepository)
        {
            _footerLinkRepository = footerLinkRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("FooterLinkComponent", _footerLinkRepository.SelectAllFooterLinks()));
        }
    }
}
