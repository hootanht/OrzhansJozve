using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DomainClass.Domain
{
    public class MasterPageGroup
    {
        [Key]
        public int MasterPageGroupId { get; set; }
        [Required(ErrorMessage = "لطفا نام سرگروه را وارد کنید")]
        [Display(Name = "سرگروه")]
        public string MasterPageGroupTitle { get; set; }
        [Display(Name = "تاریخ انتشار")]
        public DateTime MasterPageGroupCreateDate { get; set; }
        [Required(ErrorMessage = "لطفا شماره نمایش سرگروه در منو را وارد کنید")]
        [Display(Name = "شماره سرگروه در صفحه")]
        public int ShowInMenuNumber { get; set; }

        //Relations
        public virtual List<PageGroup> PageGroups { get; set; }
    }
}
