using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CalculatorMateriale.Models;

namespace CalculatorMateriale.Data
{
    public interface IUnitOfWork
    {
        IRepository<Client> ClientRepository { get; }
        IRepository<Obiectiv> ObiectivRepository { get; }
        IRepository<Material> MaterialRepository { get; }
        IRepository<CalculConsum> CalculConsumRepository { get; }
        IRepository<Comanda> ComandaRepository { get; }
        IRepository<DetaliiComanda> DetaliiComandaRepository { get; }

        Task SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Client> ClientRepository { get; }
        public IRepository<Obiectiv> ObiectivRepository { get; }
        public IRepository<Material> MaterialRepository { get; }
        public IRepository<CalculConsum> CalculConsumRepository { get; }
        public IRepository<Comanda> ComandaRepository { get; }
        public IRepository<DetaliiComanda> DetaliiComandaRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ClientRepository = new Repository<Client>(context);
            ObiectivRepository = new Repository<Obiectiv>(context);
            MaterialRepository = new Repository<Material>(context);
            CalculConsumRepository = new Repository<CalculConsum>(context);
            ComandaRepository = new Repository<Comanda>(context);
            DetaliiComandaRepository = new Repository<DetaliiComanda>(context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
