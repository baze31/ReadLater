using Data;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;

        public BookmarkService(ReadLaterDataContext readLaterDataContext) 
        {
            _ReadLaterDataContext = readLaterDataContext;            
        }

        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            bookmark.CreateDate = DateTime.Now;
            
            
            _ReadLaterDataContext.Add(bookmark);
            _ReadLaterDataContext.SaveChanges();
            return bookmark;
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            Category category = new Category();
            category.Name = bookmark.Category.Name;
            var count = _ReadLaterDataContext.Categories.Where(v => v.Name == category.Name);
            var Count1 = count.Count();
            if (Count1 == 0)
            {
                _ReadLaterDataContext.Add(category);
                bookmark.CategoryId = _ReadLaterDataContext.Categories.OrderBy(x => x.ID).Last().ID;
                bookmark.Category = _ReadLaterDataContext.Categories.OrderBy(x => x.ID).Last();

            }
            else
            {
                bookmark.CategoryId = _ReadLaterDataContext.Categories.FirstOrDefault(x => x.Name == category.Name).ID;
                bookmark.Category = _ReadLaterDataContext.Categories.FirstOrDefault(x => x.Name == category.Name);
            }
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<Bookmark> GetBookmarks()
        {
            return _ReadLaterDataContext.Bookmark.ToList();
        }

        public Bookmark GetBookmark(int Id)
        {
            return _ReadLaterDataContext.Bookmark.Where(c => c.ID == Id).FirstOrDefault();
        }

        public Bookmark GetBookmark(string URL)
        {
            return _ReadLaterDataContext.Bookmark.Where(c => c.URL == URL).FirstOrDefault();
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Bookmark.Remove(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
