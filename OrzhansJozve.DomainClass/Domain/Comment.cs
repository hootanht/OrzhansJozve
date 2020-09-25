using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DomainClass.Domain
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
        [Display(Name = "نام")]
        public string CommentAuthorName { get; set; }
        [Required(ErrorMessage = "لطفا تاریخ انتشار را وارد کنید")]
        [Display(Name = "تاریخ انتشار")]
        public DateTime CommentCreateDate { get; set; }
        [Required(ErrorMessage = "لطفا ایمیل خود را وارد کنید")]
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage ="لطفا یک ایمیل معتبر وارد کنید")]
        public string CommentAuthorEmail { get; set; }
        [Display(Name = "آدرس سایت")]
        public string CommentAuthorWebsite { get; set; }
        [Required(ErrorMessage = "لطفا نظر خود را وارد کنید")]
        [Display(Name = "نظر")]
        public string CommentAuthorText { get; set; }
        [Display(Name = "نمایش")]
        public bool CommentIsAccept { get; set; }

        //Relations Property
        public int PageId { get; set; }

        //Relations
        public virtual Page Page { get; set; }
    }
}
