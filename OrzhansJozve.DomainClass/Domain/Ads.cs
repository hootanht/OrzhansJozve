using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DomainClass.Domain
{
    public class Ads
    {
        [Key]
        public int AdsId { get; set; }
        [Required(ErrorMessage = "لطفا عنوان تبلیغ را وارد کنید")]
        [Display(Name = "عنوان تبلیغ")]
        public string AdsTitle { get; set; }
        [Required(ErrorMessage = "لطفا آدرس اینترنتی تبلیغ را وارد کنید")]
        [Display(Name = "آدرس اینترنتی تبلیغ")]
        public string AdsUrl { get; set; }
        [Required(ErrorMessage = "لطفا مکان قرار گیری تبلیغ را وارد کنید")]
        [Display(Name = "مکان قرار گیری تبلیغ")]
        public bool TopAds { get; set; }
        [Display(Name = "عکس تبلیغ")]
        public string AdsImageName { get; set; }
        public DateTime AdsCreateDate { get; set; }
        [Required(ErrorMessage = "لطفا شماره ستون تبلیغ را وارد کنید")]
        [Display(Name = "شماره ستون تبلیغ")]
        public int AdsRow { get; set; }
    }
}
