using BooksAPIsv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPIsv2.Repository.IRepository
{
    public interface IRepositoryCategory
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int IdCategory);
        bool existsCategory(string NameCategory);
        bool existsCategory(int IdCategory);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}

