using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.models;

namespace Core.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<ICollection<Cliente>> GetClientes();
        Task<Cliente> GetCliente(int id);
        Task<bool> ExisteId(int id);
        Task<bool> ExisteNombre(string nombre);
        Task<bool> CrearCliente(Cliente cliente);
        Task<bool> ActualizarCliente(Cliente cliente);
        Task<bool> EliminarCliente(Cliente cliente);
        Task<bool> Guardar();
    }
}