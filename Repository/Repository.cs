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

        public async Task CreateAsync(TEntidade Entity)
        {
            await DbSetContext.AddAsync(Entity);
            await Db.SaveChangesAsync();
        }

        public virtual async Task<TEntidade> ReadAsync(int Id)
        {
            return await DbSetContext.FindAsync(Id);
        }

        public virtual async  Task<IEnumerable<TEntidade>> ReadAllAsync()
        {
            return await DbSetContext.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(TEntidade Entity)
        {
            DbSetContext.Update(Entity);
            await Db.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int Id)
        {
            var obj = await DbSetContext.FindAsync(Id);
            if (obj != null)
            {
                DbSetContext.Remove(obj);
                await Db.SaveChangesAsync();
            }
        }
    }
}
