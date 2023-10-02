using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppIdwall.Connections;
using WebAppIdwall.Models;
using WebAppIdwall.Repository;

namespace WebAppIdwall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrimesModelsController : Controller
    {
        private readonly CrimesRepository _crimesRepository;

        public CrimesModelsController(CrimesRepository crimesRepository)
        {
            _crimesRepository = crimesRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CrimesModel>> Get()
        {
            var registros = _crimesRepository.GetAll();

            return Ok(registros);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CrimesModel>> GetById(int id)
        {
            var registros = _crimesRepository.GetById(id);

            return Ok(registros);
        }

        [HttpGet("GetByNameLike/{name}")]
        public ActionResult<IEnumerable<CrimesModel>> GetByName(string name)
        {
            var registros = _crimesRepository.GetByName(name);

            return Ok(registros);
        }

        [HttpGet("GetByNameExactly/{name}")]
        public ActionResult<IEnumerable<CrimesModel>> GetByNameExactly(string name)
        {
            var registros = _crimesRepository.GetByNameExactly(name);

            return Ok(registros);
        }
    }
}
