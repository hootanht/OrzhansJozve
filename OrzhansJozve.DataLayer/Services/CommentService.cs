using OrzhansJozve.DataLayer.Context;
using OrzhansJozve.DataLayer.Repositories;
using OrzhansJozve.DomainClass.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrzhansJozve.DataLayer.Services
{
    public class CommentService : ICommentRepository
    {
        private OrzhansJozve_DbContext _context;
        public CommentService(OrzhansJozve_DbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
        public void Delete(Comment comment)
        {
            _context.Remove(comment);
        }

        public void DelteById(int id)
        {
            var commentItem = SelectById(id);
            Delete(commentItem);
        }

        public IEnumerable<Comment> GetAllCommentByPage(int pageId)
        {
            return _context.Comments.Where(c => c.CommentIsAccept == true && c.PageId == pageId).ToList();
        }

        public int GetAllCommentByPageNumber(int pageId)
        {
            return _context.Comments.Where(c => c.CommentIsAccept == true && c.PageId==pageId).Count();
        }

        public int GetAllCommentIsAcceptNumber()
        {
            return _context.Comments.Where(c => c.CommentIsAccept == true).Count();
        }

        public int GetAllCommentNotAcceptNumber()
        {
            return _context.Comments.Where(c => c.CommentIsAccept == false).Count();
        }

        public void Insert(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Comment> SelectAll()
        {
            return _context.Comments.Where(c => c.CommentIsAccept == true).ToList();
        }

        public IEnumerable<Comment> SelectAllByOrder()
        {
            return _context.Comments.OrderBy(p => p.CommentCreateDate).ToList();
        }

        public IEnumerable<Comment> SelectAllForTimeline()
        {
            return _context.Comments.Where(c => c.CommentIsAccept == false).ToList();
        }

        public Comment SelectById(int id)
        {
            return _context.Comments.Find(id);
        }

        public IEnumerable<Comment> SelectForPagging(int skip, int take)
        {
            return _context.Comments.Where(c => c.CommentIsAccept == true).OrderByDescending(p => p.CommentCreateDate).Skip(skip).Take(take).ToList();

        }

        public IEnumerable<Comment> SelectForPaggingNotAccept(int skip, int take)
        {
            return _context.Comments.Where(c => c.CommentIsAccept == false).OrderByDescending(p => p.CommentCreateDate).Skip(skip).Take(take).ToList();
        }

        public void Update(Comment comment)
        {
            _context.Entry(comment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
