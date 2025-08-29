using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class Repository<TEntidade> : DbContext, IRepository<TEntidade>
        where TEntidade : EntityBase, new() 
    {
        protected DbContext Db;
        protected DbSet<TEntidade> DbSetContext;

        public Repository(DbContext dbContext)
        {
            Db = dbContext;
            DbSetContext = Db.Set<TEntidade>();
        }

        public void Create(TEntidade Entity)
        {
            if (Entity.Codigo != null)
            {
                DbSetContext.Add(Entity);
                Db.SaveChanges();
            }
        }

        public virtual TEntidade Read(int Id)
        {
            return DbSetContext.Find(Id);
            //return DbSetContext.Where(x => x.Codigo == Id).FirstOrDefault();
        }

        public virtual IEnumerable<TEntidade> Read()
        {
            return DbSetContext.AsNoTracking().ToList();
        }

        public void Update(TEntidade Entity)
        {
            if (Entity.Codigo != null)
            {
                DbSetContext.Update(Entity);
                Db.SaveChanges();
            }
        }

        public virtual void Delete(int Id)
        {
            var obj = DbSetContext.Find(Id);
            if (obj != null)
            {
                DbSetContext.Remove(obj);
                Db.SaveChanges();
            }
        }
    }
}
