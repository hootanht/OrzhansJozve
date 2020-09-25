using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.DataLayer.Repositories
{
    public interface IFooterLinkRepository
    {
        void Insert(FooterLink footerLink);
        FooterLink SelectFooterLinkById(int id);
        IEnumerable<FooterLink> SelectAllFooterLinks();
        IEnumerable<FooterLink> SelectAllFooterLinksForPaging(int skip, int take);
        void Delete(FooterLink footerLink);
        void Delete(int id);
        void Update(FooterLink footerLink);
        int AllFooterLinksCount();
        bool IsColumnOneExist();
        bool IsColumnTwoExist();
        bool FooterLinkExist();
        void Save();
    }
}
