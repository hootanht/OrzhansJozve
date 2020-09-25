using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.Web.Pages.Admin.Pages
{
    public class AdminAddAdsModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private IAdsRepository _adsRepository { get; set; }
        [BindProperty]
        public Ads AdsModel { get; set; }

        public AdminAddAdsModel(IAdsRepository adsRepository)
        {
            _adsRepository = adsRepository;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }

        public async Task<IActionResult> OnPostAddAdds(IFormFile adsImage, bool isTop)
        {
            if (ModelState.IsValid)
            {
                AdsModel.AdsCreateDate = DateTime.Now;
                AdsModel.TopAds = isTop;
                if (adsImage != null)
                {
                    string path = "wwwroot/Blog-Content/Ads-Images";
                    AdsModel.AdsImageName = Guid.NewGuid().ToString() + AdsModel.AdsTitle.Replace(" ", "-") + Path.GetExtension(adsImage.FileName);
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), path)))
                    {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), path));
                    }
                    string authorImagePath = Path.Combine(Directory.GetCurrentDirectory(), path, AdsModel.AdsImageName);
                    using (var stream = new FileStream(authorImagePath, FileMode.Create))
                    {
                        await adsImage.CopyToAsync(stream);
                    }
                }
                _adsRepository.Insert(AdsModel);
                _adsRepository.Save();
                Message = "تبلیغ با موفقیت منتشر شد";
                return Redirect("/admin/ads");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}