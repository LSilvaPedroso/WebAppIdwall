using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppIdwall.Connections;
using WebAppIdwall.Models;

namespace WebAppIdwall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrimesModelsController : Controller
    {
        private readonly SqlContext _context;

        public CrimesModelsController(SqlContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CrimesModel>> Get()
        {
            var registros = _context.Crimes
                .ToList();

            return Ok(registros);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CrimesModel>> GetById(int id)
        {
            var registros = _context.Crimes.AsNoTracking().Where(x => x.Id.Equals(id));

            return Ok(registros);
        }

        [HttpGet("GetByNameLike/{name}")]
        public ActionResult<IEnumerable<CrimesModel>> GetByName(string name)
        {
            var registros = _context.Crimes.AsNoTracking().Where(x => x.Name.Contains(name));

            return Ok(registros);
        }

        [HttpGet("GetByNameExactly/{name}")]
        public ActionResult<IEnumerable<CrimesModel>> GetByNameExactly(string name)
        {
            var registros = _context.Crimes.AsNoTracking().Where(x => x.Name.Equals(name));

            return Ok(registros);
        }
    }
}
