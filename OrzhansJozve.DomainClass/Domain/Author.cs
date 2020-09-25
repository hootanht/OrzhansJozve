using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DomainClass.Domain
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Display(Name = "عکس نویسنده")]
        public string AuthorImageUrl { get; set; }
        [Required(ErrorMessage = "لطفا نام نویسنده را وارد کنید")]
        [Display(Name = "نام نویسنده")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "لطفا توضیح کوتاه نویسنده را وارد کنید")]
        [Display(Name = "توضیح کوتاه نویسنده")]
        public string AuthorShortDescription { get; set; }
        [Required(ErrorMessage = "لطفا تاریخ انتشار را وارد کنید")]
        [Display(Name = "تاریخ انتشار")]
        public DateTime AuthorCreateDate { get; set; }
        [Required(ErrorMessage = "لطفا تاریخ تولد نویسنده را وارد کنید")]
        [Display(Name = "تاریخ تولد نویسنده")]
        public string AuthorBirthday { get; set; }


        //Relations
        public virtual List<Page> Pages { get; set; }
    }
}
