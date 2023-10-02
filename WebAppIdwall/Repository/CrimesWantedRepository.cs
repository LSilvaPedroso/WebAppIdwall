using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAppIdwall.Connections;

namespace WebAppIdwall.Repository
{
    public class CrimesWantedRepository
    {
        private readonly SqlContext _context;

        public CrimesWantedRepository(SqlContext context)
        {
            _context = context;
        }

        public string GetAll()
        {
            //var registros = _context.CrimeWanted.ToList();
            var registros = _context.CrimeWanted
                .Include(w => w.Wanted)
                .Include(c => c.Crimes)
                .AsNoTracking()
                .ToList();

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos
                Formatting = Formatting.Indented, // Formato indentado para facilitar a leitura
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(registros, options);

            return json;
        }
        public string GetByIdWanted(int idWanted)
        {
            var registros = _context.CrimeWanted
            .Include(w => w.Wanted)
            .Include(c => c.Crimes)
            .Where(x => x.IdWanted.Equals(idWanted))
            .AsNoTracking()
            .ToList();

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos
                Formatting = Formatting.Indented, // Formato indentado para facilitar a leitura
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(registros, options);

            return json;
        }

        public string GetByIdCrime(int idCrime)
        {
            var registros = _context.CrimeWanted.AsNoTracking()
            .Include(w => w.Wanted)
            .Include(c => c.Crimes)
            .Where(x => x.IdCrime.Equals(idCrime))
            .ToList();

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos
                Formatting = Formatting.Indented, // Formato indentado para facilitar a leitura
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(registros, options);

            return json;
        }
    }
}
