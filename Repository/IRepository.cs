using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntidade>
        where TEntidade : class
    {
        Task CreateAsync(TEntidade Entity);
        Task<TEntidade> ReadAsync(int Id);
        Task<IEnumerable<TEntidade>> ReadAllAsync();
        Task UpdateAsync(TEntidade Entity);
        Task DeleteAsync(int Id);
    }
}
