using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.DataLayer.Repositories
{
    public interface INewsAgencyPeopleRepository
    {
        IEnumerable<NewsAgencyPeople> SelectAll();
        NewsAgencyPeople SelectById(int id);
        bool NewsAgencyPeopleExist(string email);
        void Insert(NewsAgencyPeople people);
        void Save();
    }
}
