using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    public class AdminAdsModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<Ads> Ads { get; set; }
        private IAdsRepository _adsRepository { get; set; }
        public AdminAdsModel(IAdsRepository adsRepository)
        {
            _adsRepository = adsRepository;
        }
        public void OnGet(int pageid = 1)
        {
            int take = 6;
            int skip = (pageid - 1) * take;
            int Count = _adsRepository.SelectAllAdsCount();
            ViewData["PageID"] = pageid;
            ViewData["PageCount"] = (int)Math.Ceiling(Convert.ToDouble(Count) / Convert.ToDouble(take));
            Ads = _adsRepository.SelectAdsForPagging(skip, take).ToList();
        }

        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        public IActionResult OnGetDelete(int id)
        {
            var ad = _adsRepository.SelectAdsById(id);
            string authorImagePath = "wwwroot/Blog-Content/Ads-Images";
            if (ad.AdsImageName != null)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), authorImagePath, ad.AdsImageName)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), authorImagePath, ad.AdsImageName));
                }
            }
            _adsRepository.Delete(ad);
            _adsRepository.Save();
            Message = "تبلیغ با موفقیت حذف شد";
            return Redirect("/admin/ads");
        }
    }
}