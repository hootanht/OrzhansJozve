using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DomainClass.Domain
{
    public class InsideAticelImage
    {
        [Key]
        public int InsideAticelImageId { get; set; }
        [Required(ErrorMessage ="لطفا عنوان عکس را وارد کنید")]
        [Display(Name ="عنوان عکس")]
        public string InsideAticelImageTitle { get; set; }
        [Display(Name = "نام عکس")]
        public string InsideAticelImageName { get; set; }
        [Display(Name = "تاریخ انتشار عکس")]
        public DateTime InsideAticelImageCreateDate { get; set; }
    }
}
