using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.models;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ApplicationDbContext dbContext;

        public ClienteRepositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> ActualizarCliente(Cliente cliente)
        {
            dbContext.Cliente.Update(cliente);
            return await Guardar();
        }

        public async Task<bool> CrearCliente(Cliente cliente)
        {
            await dbContext.Cliente.AddAsync(cliente);
            return await Guardar();
        }

        public async Task<bool> EliminarCliente(Cliente cliente)
        {
            dbContext.Cliente.Remove(cliente);
            return await Guardar();
        }

        public async Task<bool> ExisteId(int id)
        {
            return await dbContext.Cliente.AnyAsync(x => x.Id.Equals(id));
        }

        public async Task<bool> ExisteNombre(string nombre)
        {
            return await dbContext.Cliente.AnyAsync(x => x.Nombre.ToLower().Trim().Equals(nombre.ToLower().Trim()));
        }

        public async Task<Cliente> GetCliente(int id)
        {
            return await dbContext.Cliente.FindAsync(id);
        }

        public async Task<ICollection<Cliente>> GetClientes()
        {
            return await dbContext.Cliente.ToListAsync();
        }

        public async Task<bool> Guardar()
        {
            return await dbContext.SaveChangesAsync() >= 0? true: false;
        }
    }
}