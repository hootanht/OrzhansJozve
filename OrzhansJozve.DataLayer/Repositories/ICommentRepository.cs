using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrzhansJozve.DomainClass.Domain;

namespace OrzhansJozve.DataLayer.Repositories
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> SelectAll();
        IEnumerable<Comment> SelectAllByOrder();
        IEnumerable<Comment> SelectAllForTimeline();
        IEnumerable<Comment> SelectForPagging(int skip, int take);
        IEnumerable<Comment> SelectForPaggingNotAccept(int skip, int take);
        Comment SelectById(int id);
        int GetAllCommentIsAcceptNumber();
        int GetAllCommentNotAcceptNumber();
        int GetAllCommentByPageNumber(int pageId);
        IEnumerable<Comment> GetAllCommentByPage(int pageId);
        void Insert(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
        void DelteById(int id);
        void Save();
    }
}
