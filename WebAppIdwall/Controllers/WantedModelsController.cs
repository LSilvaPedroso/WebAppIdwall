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
using AutoMapper;
using WebAppIdwall.Repository;

namespace WebAppIdwall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WantedModelsController : Controller
    {
        private readonly WantedRepository _wantedRepository;

        public WantedModelsController(WantedRepository wantedRepository)
        {
            _wantedRepository = wantedRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WantedModel>> Get()
        {
            string json = _wantedRepository.GetAll(); // Obtém a função

            return Content(json, "application/json");
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<WantedModel>> GetById(int id)
        {
            string json = _wantedRepository.GetById(id);

            return Content(json, "application/json");
        }
        
        [HttpGet("GetByNameLike/{name}")]
        public ActionResult<IEnumerable<WantedModel>> GetByNameLike(string name)
        {
            string json = _wantedRepository.GetByNameLike(name);

            return Content(json, "application/json");
        }

        [HttpGet("GetByNameExactly/{name}")]
        public ActionResult<IEnumerable<WantedModel>> GetByNameExactly(string name)
        {
            string json = _wantedRepository.GetByNameExactly(name);

            return Content(json, "application/json");
        }

        [HttpGet("GetByNacionality/{nationality}")]
        public ActionResult<IEnumerable<WantedModel>> GetByNacionality(string nationality)
        {
            string json = _wantedRepository.GetByNacionality(nationality);

            return Content(json, "application/json");
        }
    }
}
