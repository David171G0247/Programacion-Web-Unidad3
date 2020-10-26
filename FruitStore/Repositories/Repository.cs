using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitStore.Models;

namespace FruitStore.Repositories
{
    public class Repository<T> where T:class
    {
        public fruteriashopContext Context { get; set; }

        public Repository(fruteriashopContext context)
        {
            Context = context;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public virtual T Get(object id)
        {
            return Context.Find<T>(id);
        }
        public virtual void insert(T entidad)
        {
            if(Validate(entidad))
            {
                Context.Add<T>(entidad);
                Context.SaveChanges();
            }
        }
        public virtual void update(T entidad)
        {
            if (Validate(entidad))
            {
                Context.Update<T>(entidad);
                Context.SaveChanges();
            }
        }
        public virtual void delete(T entidad)
        {
            Context.Remove<T>(entidad);
            Context.SaveChanges();
        }
        public virtual bool Validate(T entidad)
        {
            return true;
        }
    }
}
