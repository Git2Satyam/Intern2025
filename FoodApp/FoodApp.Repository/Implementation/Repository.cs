using FoodApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repository.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DbContext _db;
        public Repository(DbContext db)
        {
            _db = db;
        }
        public void Add(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
        }

        public void Delete(int id)
        {
            TEntity entity = _db.Set<TEntity>().Find(id);
            if(entity != null)
            {
                _db.Set<TEntity>().Remove(entity);
            }

        }

        public IEnumerable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().ToList(); 
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
        }
    }
}
