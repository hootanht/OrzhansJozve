using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DomainClass.Domain
{
    public class FooterLink
    {
        [Key]
        public int FooterLinkId { get; set; }
        [Required(ErrorMessage ="لطفا عنوان لینک را وارد کنید")]
        [Display(Name = "عنوان لینک")]
        public string FooterLinkTitle { get; set; }
        [Required(ErrorMessage = "لطفا لینک سایت را وارد کنید")]
        [Display(Name = "لینک سایت")]
        [Url(ErrorMessage ="لطفا لینک معتبر وارد کنید")]
        public string FooterLinkUrl { get; set; }
        [Display(Name ="نوع Rel لینک")]
        public string FooterLinkUrlRel { get; set; }
        [Display(Name = "شماره ستون")]
        public int FooterLinkColumn { get; set; }
        [Display(Name = "تاریخ انتشار")]
        public DateTime FooterLinkCreateDate { get; set; }
    }
}
