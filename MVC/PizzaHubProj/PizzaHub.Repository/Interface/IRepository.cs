using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        int SaveChange();
    }
}
