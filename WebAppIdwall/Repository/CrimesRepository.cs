using Microsoft.EntityFrameworkCore;
using WebAppIdwall.Connections;
using WebAppIdwall.Models;

namespace WebAppIdwall.Repository
{
    public class CrimesRepository
    {
        private readonly SqlContext _context;

        public CrimesRepository(SqlContext context)
        {
            _context = context;
        }

        public List<CrimesModel> GetAll()
        {
            var registros = _context.Crimes
                .ToList();
            return registros;
        }

        public IQueryable<CrimesModel> GetById(int id)
        {
            var registros = _context.Crimes.AsNoTracking().Where(x => x.Id.Equals(id));

            return registros;
        }

        public IQueryable<CrimesModel> GetByName(string name)
        {
            var registros = _context.Crimes.AsNoTracking().Where(x => x.Name.Contains(name));

            return registros;
        }

        public IQueryable<CrimesModel> GetByNameExactly(string name)
        {
            var registros = _context.Crimes.AsNoTracking().Where(x => x.Name.Equals(name));

            return registros;
        }

    }
}
