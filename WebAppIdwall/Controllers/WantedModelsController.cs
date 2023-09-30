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

namespace WebAppIdwall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WantedModelsController : Controller
    {
        private readonly SqlContext _context;

        public WantedModelsController(SqlContext context, IMapper mapper)
        {
            _context = context;
        }

        // Método para mapear a entidade Wanted para um objeto anônimo
        private object MapWanted(WantedModel wanted)
        {
            return new
            {
                wanted.Id,
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
                Cautions = wanted.Cautions.Select(ca => new
                {
                    ca.Id,
                    ca.Name,
                    ca.IdWanted
                }).ToList(),
                Crimes = wanted.CrimeWanted.Select(cw => new
                {
                    cw.Id,
                    cw.IdCrime,
                    CrimeName = cw.Crimes.Name,
                    cw.IdWanted
                }).ToList(),
                Birth = wanted.BirthWanted.Select(cw => new
                {
                    cw.Id,
                    cw.IdBirth,
                    Birth = cw.Birth.Birth
                }).ToList()
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<WantedModel>> Get()
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

            var json = JsonConvert.SerializeObject(resultado, options);

            return Content(json, "application/json");
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<WantedModel>> GetById(int id)
        {
            //var registros = _context.Wanted.AsNoTracking().Where(x => x.Id.Equals(id));
            
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

            var json = JsonConvert.SerializeObject(resultado, options);

            return Content(json, "application/json");
        }
        
        [HttpGet("GetByNameLike/{name}")]
        public ActionResult<IEnumerable<WantedModel>> GetByNameLike(string name)
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

            return Content(json, "application/json");
        }

        [HttpGet("GetByNameExactly/{name}")]
        public ActionResult<IEnumerable<WantedModel>> GetByNameExactly(string name)
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

            return Content(json, "application/json");
        }

        [HttpGet("GetByNacionality/{nationality}")]
        public ActionResult<IEnumerable<WantedModel>> GetByNacionality(string nationality)
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

            return Content(json, "application/json");
        }

        [HttpPost("BuscarDadosDoFBI")]
        public async Task<IActionResult> ConsultarEInserirDados()
        {
            try
            {
                var client = new HttpClient();
                int pageSize = 50;
                int page = 1;

                bool result = true;

                while (result)
                {
                    string apiUrl = $"https://api.fbi.gov/@wanted?pageSize={pageSize}&page={page}&sort_on=modified&sort_order=desc";

                    // Faz a solicitação GET à API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Verifica se a solicitação foi bem-sucedida
                    if (response.IsSuccessStatusCode)
                    {
                        // Lê a resposta como uma string
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Desserializa a resposta em objetos C#
                        RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(responseBody);
                        List<DataApiFBI> items = rootObject.Items;

                        foreach (var apiData in items)
                        {
                            // Faça o mapeamento de DataApiFBI para WantedModel
                            var wantedModel = new WantedModel
                            {
                                // Mapeie as propriedades aqui, por exemplo:
                                Name = apiData.Title,
                                Height = (decimal)apiData.HeightMax,
                                Weight = (decimal)apiData.WeightMax,
                                Hair = apiData.Hair,
                                EyesColor = apiData.Eyes,
                                Nacionalidade = apiData.Nationality,
                                Remarks = apiData.Remarks,
                                Sexo = apiData.Sex,
                                LocalNascimento = apiData.PlaceOfBirth,
                                Recompensa = (decimal)apiData.RewardMax

                                // Continue mapeando as propriedades conforme necessário
                            };

                            // Adicione o objeto WantedModel ao contexto
                            _context.Wanted.Add(wantedModel);
                        }

                        // Salve as mudanças no banco de dados
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        return Ok("Houve um erro na colicitação da API.");

                    }
                    page++;
                }

                return Ok("Dados inseridos com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao consultar a API e inserir os dados: {ex.Message}");
            }
        }
    }
}
