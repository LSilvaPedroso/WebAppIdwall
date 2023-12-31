﻿using System;
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
    public class CautionModelsController : Controller
    {
        private readonly CautionRepository _cautionRepository;

        public CautionModelsController(CautionRepository cautionRepository)
        {
            _cautionRepository = cautionRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CautionModel>> Get()
        {
            var registros = _cautionRepository.GetAll();

            return Ok(registros);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CautionModel>> GetById(int id)
        {
            var registros = _cautionRepository.GetById(id);

            return Ok(registros);
        }

        [HttpGet("GetByIdWanted/{idWanted}")]
        public ActionResult<IEnumerable<CautionModel>> GetByIdWanted(int idWanted)
        {
            var registros = _cautionRepository.GetByIdWanted(idWanted);

            return Ok(registros);
        }
    }
}
