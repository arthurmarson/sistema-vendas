using Domain.Interfaces;
using Domain.Repository;
using SistemaVenda.Domain.Entities;
using System.Data;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        IClienteRepository ClienteRepository;

        // Injeção de dependência do repositório de categoria
        public ClienteService(IClienteRepository clienteRepository)
        {
            ClienteRepository = clienteRepository;
        }

        public async Task InsertAsync(Cliente cliente)
        {
            ClienteRepository.Create(cliente);
        }

        public async Task<IEnumerable<Cliente>> FindAllAsync()
        {
            // Comunicação com o repositório para buscar todas as categorias
            return ClienteRepository.Read(); 
        }

        public async Task<Cliente> FindByIdAsync(int id)
        {
            return ClienteRepository.Read(id);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            ClienteRepository.Update(cliente);
        }

        public async Task RemoveAsync(int id)
        {
            ClienteRepository.Delete(id);
        }


        
    }
}
