using Microsoft.EntityFrameworkCore;
using WebAppIdwall.Connections;
using WebAppIdwall.Models;

namespace WebAppIdwall.Repository
{
    public class CautionRepository
    {
        private readonly SqlContext _context;

        public CautionRepository(SqlContext context)
        {
            _context = context;
        }

        public List<CautionModel> GetAll()
        {
            var registros = _context.Caution
                .ToList();
            return registros;
        }

        public IQueryable<CautionModel> GetById(int id)
        {
            var registros = _context.Caution.AsNoTracking().Where(x => x.Id.Equals(id));
            return registros;
        }

        public IQueryable<CautionModel> GetByDescription(string description)
        {
            var registros = _context.Caution.AsNoTracking().Where(x => x.Name.Equals(description));
            return registros;
        }

        public IQueryable<CautionModel> GetByIdWanted(int idWanted)
        {
            var registros = _context.Caution.AsNoTracking().Where(x => x.IdWanted.Equals(idWanted));

            return registros;
        }
    }
}
