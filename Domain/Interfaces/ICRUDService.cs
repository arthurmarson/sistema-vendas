using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICRUDService<TEntidade> 
        where TEntidade : class
    {
        Task<IEnumerable<TEntidade>> FindAllAsync();
        Task InsertAsync(TEntidade categoria);
        Task UpdateAsync(TEntidade categoria);
        Task<TEntidade> FindByIdAsync(int id);
        Task RemoveAsync(int id);
    }
}
