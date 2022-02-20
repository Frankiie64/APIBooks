using BooksAPIsv2.Data;
using BooksAPIsv2.Models;
using BooksAPIsv2.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPIsv2.Repository
{
    public class RepositoryCategory : IRepositoryCategory
    {
        private readonly ApplicationDbContext _db;

        public RepositoryCategory(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateCategory(Category category)
        {
            _db.Category.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _db.Category.Remove(category);
            return Save();
        }
        public bool UpdateCategory(Category category)
        {
            _db.Category.Update(category);
            return Save();
        }
        public bool existsCategory(string NameCategory)
        {
            bool value = _db.Category.Any(c => c.Name.ToLower().Trim() == NameCategory.ToLower().Trim());
            return value;
        }

        public bool existsCategory(int IdCategory)
        {
            bool value = _db.Category.Any(c => c.Id == IdCategory);
            return value;
        }

        public ICollection<Category> GetCategories()
        {
            return _db.Category.OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(int IdCategory)
        {
            return _db.Category.FirstOrDefault(c => c.Id == IdCategory);
        }


        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        
    }
}
