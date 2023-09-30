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
    public class CautionModelsController : Controller
    {
        private readonly SqlContext _context;

        public CautionModelsController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CautionModel>> Get()
        {
            var registros = _context.Caution
                .ToList();

            return Ok(registros);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CautionModel>> GetById(int id)
        {
            var registros = _context.Caution.AsNoTracking().Where(x => x.Id.Equals(id));

            return Ok(registros);
        }

        [HttpGet("GetByIdWanted/{idWanted}")]
        public ActionResult<IEnumerable<CautionModel>> GetByIdWanted(int idWanted)
        {
            var registros = _context.Caution.AsNoTracking().Where(x => x.IdWanted.Equals(idWanted));

            return Ok(registros);
        }
    }
}
