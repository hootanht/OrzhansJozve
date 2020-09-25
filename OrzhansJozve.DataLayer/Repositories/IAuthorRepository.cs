using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.DataLayer.Repositories
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> SelectAll();
        IEnumerable<Author> SelectAllByOrder();
        IEnumerable<Author> SelectForPagging(int skip, int take);
        Author SelectById(int id);
        void Update(Author author);
        int GetAllAuthorNumber();
        void Insert(Author author);
        void Delete(Author author);
        void DelteById(int id);
        void Save();
    }
}
