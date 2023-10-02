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
using WebAppIdwall.Repository;

namespace WebAppIdwall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrimeWantedModelsController : Controller
    {
        private readonly CrimesWantedRepository _crimesWantedRepository;

        public CrimeWantedModelsController(CrimesWantedRepository crimesWantedRepository)
        {
            _crimesWantedRepository = crimesWantedRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CrimeWantedModel>> Get()
        {
            string json = _crimesWantedRepository.GetAll();

            return Content(json, "application/json");
        }

        [HttpGet("GetByIdWanted/{idWanted}")]
        public ActionResult<IEnumerable<CrimeWantedModel>> GetByIdWanted(int idWanted)
        {
            string json = _crimesWantedRepository.GetByIdWanted(idWanted);

            return Content(json, "application/json");
        }

        [HttpGet("GetByIdCrime/{idCrime}")]
        public ActionResult<IEnumerable<CrimeWantedModel>> GetByIdCrime(int idCrime)
        {
            string json = _crimesWantedRepository.GetByIdCrime(idCrime);

            return Content(json, "application/json");

        }
    }
}
