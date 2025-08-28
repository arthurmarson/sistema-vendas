using Application.ApplicationServices.Interfaces;
using Domain.Interfaces;
using Domain.Services;
using SistemaVenda.Domain.Entities;
using SistemaVenda.Models;

namespace Application.ApplicationServices
{
    public class ClienteApplicationService : IClienteApplicationService
    {
        private readonly IClienteService _clienteService;

        // Injeção de dependência do serviço de cliente do domínio
        public ClienteApplicationService(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task InsertAsync(ClienteFormViewModel cliente)
        {
            Cliente entity = new Cliente()
            {
                Codigo = cliente.Codigo,
                Nome = cliente.Nome,
                CNPJ_CPF = cliente.CNPJ_CPF,
                Email = cliente.Email,
                Celular = cliente.Celular
            };
            await _clienteService.InsertAsync(entity);
        }

        public async Task<IEnumerable<ClienteFormViewModel>> FindAllAsync()
        {
            var lista = await _clienteService.FindAllAsync();

            List<ClienteFormViewModel> listaClientes = new List<ClienteFormViewModel>();

            // Mapeamento dos dados do domínio (Cliente) para a ViewModel (ClienteFormViewModel)
            foreach (var item in lista)
            {
                ClienteFormViewModel cliente = new ClienteFormViewModel()
                {
                    Codigo = item.Codigo,
                    Nome = item.Nome,
                    CNPJ_CPF = item.CNPJ_CPF,
                    Email = item.Email,
                    Celular = item.Celular
                };
                listaClientes.Add(cliente);
            }
            return listaClientes;
        }

        public async Task<ClienteFormViewModel> FindByIdAsync(int codigoCliente)
        {
            var registro = await _clienteService.FindByIdAsync(codigoCliente);

            // Mapeamento manual dos dados do domínio (Cliente) para a ViewModel (ClienteFormViewModel)
            ClienteFormViewModel cliente = new ClienteFormViewModel()
            {
                Codigo = registro.Codigo,
                Nome = registro.Nome,
                CNPJ_CPF = registro.CNPJ_CPF,
                Email = registro.Email,
                Celular = registro.Celular
            };

            return cliente;
        }

        public async Task UpdateAsync(ClienteFormViewModel cliente)
        {
            Cliente entity = new Cliente()
            {
                Codigo = cliente.Codigo,
                Nome = cliente.Nome,
                CNPJ_CPF = cliente.CNPJ_CPF,
                Email = cliente.Email,
                Celular = cliente.Celular
            };
            await _clienteService.UpdateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            await _clienteService.RemoveAsync(id);
        }
    }
}
