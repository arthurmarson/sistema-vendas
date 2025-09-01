using Domain.Interfaces;
using Domain.Repository;
using SistemaVenda.Domain.Entities;
using System.Data;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        IClienteRepository ClienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            ClienteRepository = clienteRepository;
        }

        public async Task InsertAsync(Cliente cliente)
        {
            await ClienteRepository.CreateAsync(cliente);
        }

        public async Task<IEnumerable<Cliente>> FindAllAsync()
        {
            return await ClienteRepository.ReadAllAsync(); 
        }

        public async Task<Cliente> FindByIdAsync(int id)
        {
            return await ClienteRepository.ReadAsync(id);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            await ClienteRepository.UpdateAsync(cliente);
        }

        public async Task RemoveAsync(int id)
        {
            await ClienteRepository.DeleteAsync(id);
        }
    }
}
