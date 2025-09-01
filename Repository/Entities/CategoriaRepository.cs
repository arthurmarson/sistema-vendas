using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using SistemaVenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    { 
        public CategoriaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
