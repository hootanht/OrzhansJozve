using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DomainClass.Domain
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }
        [Required(ErrorMessage = "لطفا عنوان خبر را وارد کنید")]
        [Display(Name = "عنوان خبر")]
        public string PageTitle { get; set; }
        [Display(Name = "عکس خبر")]
        public string PageImageUrl { get; set; }
        [Required(ErrorMessage = "لطفا توضیح کوتاه خبر را وارد کنید")]
        [Display(Name = "توضیح کوتاه")]
        public string PageShortDiscription { get; set; }
        [Required(ErrorMessage = "لطفا محتوای خبر را وارد کنید")]
        [Display(Name = "محتوا خبر")]
        public string PageContent { get; set; }
        [Display(Name = "میزان بازدید خبر")]
        public int PageView { get; set; }
        [Required(ErrorMessage = "لطفا تاریخ انتشار خبر را وارد کنید")]
        [Display(Name = "تاریخ انتشار خبر")]
        public DateTime PageCreateDate { get; set; }
        [Display(Name = "تگ ها خبر")]
        public string PageTags { get; set; }
        [Required(ErrorMessage = "لطفا برچسب های خبر را وارد کنید")]
        [Display(Name = "برچسب های خبر")]
        public string PageKeyWords { get; set; }
        [Display(Name = "پادکست خبر")]
        public string PagePodcastUrl { get; set; }
        [Display(Name = "ویدیو خبر")]
        public string PageVideoUrl { get; set; }
        [Display(Name = "عکس دوم کوچک خبر")]
        public string PageSecondImageUrl { get; set; }
        [Required(ErrorMessage = "لطفا وضعیت نمایش خبر را وارد کنید")]
        [Display(Name = "نمایش خبر")]
        public bool PageShow { get; set; }
        [Display(Name = "عنوان ویدئو")]
        public string PageVideoTitle { get; set; }
        [Display(Name = "گوینده پادکست")]
        public string PagePodcastAuthor { get; set; }
        [Display(Name = "کلید خبر")]
        public string Shortkey { get; set; }
        [Required(ErrorMessage = "لطفا زمان مطاله خبر را وارد کنید")]
        [Display(Name = "زمان مطالعه خبر")]
        public string ReadingTime { get; set; }

        [Display(Name = "زمان انتشار خبر")]
        public string TimeCreateString { get; set; }



        //Relations Property
        public int PageGroupId { get; set; }
        public int AuthorId { get; set; }

        //Relations
        public virtual PageGroup PageGroup { get; set; }
        public virtual Author Author { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
