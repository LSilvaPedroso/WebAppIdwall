using Microsoft.EntityFrameworkCore;
using WebAppIdwall.Connections;
using WebAppIdwall.Models;

namespace WebAppIdwall.Repository
{
    public class BirthWantedRepository
    {
        private readonly SqlContext _context;

        public BirthWantedRepository(SqlContext context)
        {
            _context = context;
        }

        public IQueryable<BirthWantedModel> GetByBirthId(int id)
        {
            var registros = _context.BirthWanted.AsNoTracking().Where(x => x.IdBirth.Equals(id));
            return registros;
        }

        public IQueryable<BirthWantedModel> GetByWantedId(int id)
        {
            var registros = _context.BirthWanted.AsNoTracking().Where(x => x.IdWanted.Equals(id));
            return registros;
        }

    }
}
