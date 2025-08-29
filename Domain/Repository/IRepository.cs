using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRepository<TEntidade>
        where TEntidade : class
    {
        void Create(TEntidade Entity);
        TEntidade Read(int Id);
        IEnumerable<TEntidade> Read();   
        void Update(TEntidade Entity);
        void Delete(int Id);
    }
}
