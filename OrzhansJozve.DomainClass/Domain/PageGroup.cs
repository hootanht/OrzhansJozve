using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DomainClass.Domain
{
    
    public class PageGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PageGroupId { get; set; }
        [Required(ErrorMessage = "لطفا متن گروه را وارد کنید")]
        [Display(Name = "عنوان گروه")]
        public string PageGroupTitle { get; set; }
        [Display(Name = "تاریخ انتشار")]
        public DateTime PageGroupCreateDate { get; set; }
        [Required(ErrorMessage = "لطفا شماره نمایش گروه در منو را وارد کنید")]
        [Display(Name = "شماره گروه در صفحه")]
        public int PageGroupShowInMenuNumber { get; set; }
        [Display(Name = "تعداد بازدید گروه")]
        public int PageGroupView { get; set; }

        //Relations Property
        public int MasterPageGroupId { get; set; }

        //Relations
        public virtual MasterPageGroup MasterPageGroup { get; set; }
        public virtual List<Page> Pages { get; set; }
    }
}
