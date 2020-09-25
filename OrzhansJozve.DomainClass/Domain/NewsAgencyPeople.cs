using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DomainClass.Domain
{
    public class NewsAgencyPeople
    {
        [Key]
        public int NewsAgencyPeopleId { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "لطفا یک ایمیل معتبر وارد کنید")]
        public string Email { get; set; }
    }
}
