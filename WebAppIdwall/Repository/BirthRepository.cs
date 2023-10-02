using Microsoft.EntityFrameworkCore;
using WebAppIdwall.Connections;
using WebAppIdwall.Models;

namespace WebAppIdwall.Repository
{
    public class BirthRepository
    {
        private readonly SqlContext _context;

        public BirthRepository(SqlContext context)
        {
            _context = context;
        }

        public IQueryable<BirthModel> GetByDate(string date)
        {
            var registros = _context.Birth.AsNoTracking().Where(x => x.Birth.Equals(date));
            return registros;
        }

        public IQueryable<BirthModel> GetById(int id)
        {
            var registros = _context.Birth.AsNoTracking().Where(x => x.Id.Equals(id));
            return registros;
        }
    }
}
