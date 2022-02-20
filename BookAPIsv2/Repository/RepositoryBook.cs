using BooksAPIsv2.Models;
using BooksAPIsv2.Repository.IRepository;
using BooksAPIsv2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPIsv2.Repository
{
    public class RepositoryBook : IRepositoryBook
    {
        private readonly ApplicationDbContext _db;

        public RepositoryBook(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool UpdateBook(Book book)
        {
            _db.Book.Update(book);
            return Save();
        }
        public bool CreateBook(Book book)
        {
            _db.Book.Add(book);
            return Save();
        }

        public bool DeleteBook(Book book)
        {
            _db.Book.Remove(book);
            return Save();
        }
        public IEnumerable<Book> FindBook(string book)
        {
            IQueryable<Book> query = _db.Book;

            if (!string.IsNullOrEmpty(book))
            {
                query = query.Where(b => b.Title.Contains(book) || b.Sumary.Contains(book));
            }

            return query.ToList();

        }
        public Book GetBook(int IdBook)
        {
            return _db.Book.FirstOrDefault(b => b.Id == IdBook);
        }

        public ICollection<Book> GetBooks()
        {
            return _db.Book.OrderBy(d => d.Title).ToList();
        }
       
        public ICollection<Book> GetBooksInCat(int IdCategory)
        {
            return _db.Book.Include(ca => ca.IdCategory).Where(ca => ca.IdCategory == IdCategory).ToList();

        }
       

        public bool existsBook(int IdBook)
        {
            bool value = _db.Book.Any(c => c.Id == IdBook);
            return value;
        }
        public bool existsBook(string title)
        {
            bool value = _db.Book.Any(c => c.Title.ToLower().Trim() == title.ToLower().Trim()); ;
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;

        }

       
    }
}
