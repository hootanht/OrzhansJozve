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
    public class AdminEditAdsModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private IAdsRepository _adsRepository { get; set; }
        [BindProperty]
        public Ads AdsModel { get; set; }
        public AdminEditAdsModel(IAdsRepository adsRepository)
        {
            _adsRepository = adsRepository;
        }
        public void OnGet(int id)
        {
            AdsModel = _adsRepository.SelectAdsById(id);
        }
        public IActionResult OnPost(string query)
        {
            return RedirectToPage("AdminSearch", new { query = query, pageid = 1 });
        }
        public async Task<IActionResult> OnPostEditAdds(int id,IFormFile adsImage, bool isTop)
        {
            if (ModelState.IsValid)
            {
                AdsModel.AdsId = id;
                AdsModel.TopAds = isTop;
                var selectedAds = _adsRepository.SelectAdsById(id);
                AdsModel.AdsCreateDate = selectedAds.AdsCreateDate;
                if (adsImage != null)
                {
                    string path = "wwwroot/Blog-Content/Ads-Images";
                    if (selectedAds.AdsImageName != null)
                    {
                        if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path, selectedAds.AdsImageName)))
                        {
                            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), path, selectedAds.AdsImageName));
                        }
                    }
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
                else
                {
                    AdsModel.AdsImageName = selectedAds.AdsImageName;
                }
                _adsRepository.Update(AdsModel);
                _adsRepository.Save();
                Message = "تبلیغ با موفقیت ویرایش شد";
                return Redirect("/admin/ads");
            }
            else
            {
                return Redirect("/error");
            }
        }
    }
}