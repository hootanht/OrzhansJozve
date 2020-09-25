using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;

namespace OrzhansJozve.Web.Pages
{
    public class ArticelShortLinkModel : PageModel
    {
        public IPageRepository _pageRepository { get; set; }
        public string MainSite { get; set; }
        public Uri GraphUri { get; set; }
        public OrzhansJozve.DomainClass.Domain.Page PageModel { get; set; }
        public ArticelShortLinkModel(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
            MainSite = "https://orzhansjozve.ir";
        }
        public IActionResult OnGet(string key)
        {
            if (_pageRepository.PageExistByShoertkey(key))
            {
                var page = _pageRepository.SelectPageByShortKey(key);
                Uri uri = new Uri($"{MainSite}/articel/{page.PageId}/{page.PageTitle.Trim().Replace(" ", "-").ToString()}");
                GraphUri = uri;
                PageModel = page;
                return RedirectToPage("ArticelPage", new { pageId = page.PageId, title = page.PageTitle.Trim().Replace(" ", "-").ToString() });
            }
            else
            {
                return NotFound();
            }
        }
    }
}