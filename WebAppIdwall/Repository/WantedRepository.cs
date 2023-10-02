using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAppIdwall.Connections;
using WebAppIdwall.Models;

namespace WebAppIdwall.Repository
{
    public class WantedRepository
    {
        private readonly SqlContext _context;

        public WantedRepository(SqlContext context)
        {
            _context = context;
        }

        // Método para mapear a entidade Wanted para um objeto anônimo
        private object MapWanted(WantedModel wanted)
        {
            return new
            {
                wanted.Id,
                wanted.IdSource,
                wanted.Name,
                wanted.Height,
                wanted.Weight,
                wanted.Hair,
                wanted.EyesColor,
                wanted.Nacionalidade,
                wanted.Remarks,
                wanted.Sexo,
                wanted.LocalNascimento,
                wanted.Recompensa,
                Cautions = wanted.Cautions?.Select(ca => new
                {
                    ca.Id,
                    ca.Name,
                    ca.IdWanted
                }).ToList(),
                Crimes = wanted.CrimeWanted?.Select(cw => new
                {
                    cw.Id,
                    cw.IdCrime,
                    CrimeName = cw.Crimes?.Name, // Verificar se Crimes e Name não são nulos
                    cw.IdWanted
                }).ToList(),
                Birth = wanted.BirthWanted?.Select(bw => new
                {
                    bw.Id,
                    bw.IdBirth,
                    Birth = bw.Birth?.Birth
                }).ToList()
            };
        }

        public string GetAll()
        {
            var registros = _context.Wanted
                .Include(ca => ca.Cautions)
                .Include(bw => bw.BirthWanted)
                .ThenInclude(b => b.Birth)
                .Include(c => c.CrimeWanted)
                .ThenInclude(cr => cr.Crimes)
                .ToList();

            // Projeção para selecionar as propriedades desejadas
            var resultado = registros.Select(MapWanted).ToList();

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos
                Formatting = Formatting.Indented, // Formato indentado para facilitar a leitura
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(resultado, options);

            return json;
        }

        public string GetById(int id)
        {
            var registros = _context.Wanted
            .AsNoTracking()
            .Include(ca => ca.Cautions)
            .Include(bw => bw.BirthWanted)
            .ThenInclude(b => b.Birth)
            .Include(c => c.CrimeWanted)
            .ThenInclude(cr => cr.Crimes)
            .Where(x => x.Id.Equals(id));

            var resultado = registros.Select(MapWanted).ToList();

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos
                Formatting = Formatting.Indented, // Formato indentado para facilitar a leitura
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(resultado, options);

            return json;
        }

        public string GetBySourceId(string id)
        {
            var registros = _context.Wanted
            .AsNoTracking()
            .Include(ca => ca.Cautions)
            .Include(bw => bw.BirthWanted)
            .ThenInclude(b => b.Birth)
            .Include(c => c.CrimeWanted)
            .ThenInclude(cr => cr.Crimes)
            .Where(x => x.IdSource.Equals(id));

            var resultado = registros.Select(MapWanted).ToList();

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos
                Formatting = Formatting.Indented, // Formato indentado para facilitar a leitura
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(resultado, options);

            return json;
        }

        public string GetByNameLike(string name)
        {
            var registros = _context.Wanted
            .AsNoTracking()
            .Include(ca => ca.Cautions)
            .Include(bw => bw.BirthWanted)
            .ThenInclude(b => b.Birth)
            .Include(c => c.CrimeWanted)
            .ThenInclude(cr => cr.Crimes)
            .Where(x => x.Name.Contains(name))
            .ToList();

            var resultado = registros.Select(MapWanted).ToList();

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos
                Formatting = Formatting.Indented, // Formato indentado para facilitar a leitura
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(resultado, options);

            return json;
        }

        public string GetByNameExactly(string name)
        {
            var registros = _context.Wanted
            .AsNoTracking()
            .Include(ca => ca.Cautions)
            .Include(bw => bw.BirthWanted)
            .ThenInclude(b => b.Birth)
            .Include(c => c.CrimeWanted)
            .ThenInclude(cr => cr.Crimes)
            .Where(x => x.Name.Equals(name));

            var resultado = registros.Select(MapWanted).ToList();

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos
                Formatting = Formatting.Indented, // Formato indentado para facilitar a leitura
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(resultado, options);

            return json;
        }

        public string GetByNacionality(string nationality)
        {
            var registros = _context.Wanted
                .AsNoTracking()
                .Include(ca => ca.Cautions)
                .Include(bw => bw.BirthWanted)
                .ThenInclude(b => b.Birth)
                .Include(c => c.CrimeWanted)
                .ThenInclude(cr => cr.Crimes)
                .Where(x => x.Nacionalidade.Equals(nationality));

            var resultado = registros.Select(MapWanted).ToList();

            var options = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos
                Formatting = Formatting.Indented, // Formato indentado para facilitar a leitura
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(resultado, options);

            return json;
        }
    }
}
