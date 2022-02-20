using BooksAPIsv2.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPIsv2.Repository.IRepository
{
    public interface IRepositoryBook
    {
        ICollection<Book> GetBooks();
        ICollection<Book> GetBooksInCat(int IdCategory);

        Book GetBook(int IdBook);
        IEnumerable<Book> FindBook(string book);
        bool existsBook(string title);

        bool existsBook(int IdBook);
        bool CreateBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(Book book);
        bool Save();
    }
}
