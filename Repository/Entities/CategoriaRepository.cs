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
    { // Classe concreta que herda características da classe abstrata Repository e implementa a interface ICategoriaRepository
      // Que por sua vez implementa a interface genérica IRepository
        public CategoriaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
