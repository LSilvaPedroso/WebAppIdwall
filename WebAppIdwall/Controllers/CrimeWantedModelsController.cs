using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppIdwall.Connections;
using WebAppIdwall.Models;
using Newtonsoft.Json;

namespace WebAppIdwall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrimeWantedModelsController : Controller
    {
        private readonly SqlContext _context;

        public CrimeWantedModelsController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CrimeWantedModel>> Get()
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

            var json = JsonConvert.SerializeObject(registros, options);

            return Content(json, "application/json");
        }

        [HttpGet("GetByIdWanted/{idWanted}")]
        public ActionResult<IEnumerable<CrimeWantedModel>> GetByIdWanted(int idWanted)
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

            var json = JsonConvert.SerializeObject(registros, options);

            return Content(json, "application/json");
        }

        [HttpGet("GetByIdCrime/{idCrime}")]
        public ActionResult<IEnumerable<CrimeWantedModel>> GetByIdCrime(int idCrime)
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

            var json = JsonConvert.SerializeObject(registros, options);

            return Content(json, "application/json");

        }
    }
}
