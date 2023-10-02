using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Globalization;
using WebAppIdwall.Connections;
using WebAppIdwall.Models;
using WebAppIdwall.Repository;
using WebAppIdwall.Views;

namespace WebAppIdwall.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsertBaseController : Controller
    {
        private readonly SqlContext _context;
        private readonly WantedRepository _wantedRepository;
        private readonly CrimesWantedRepository _crimesWantedRepository;
        private readonly CrimesRepository _crimesRepository;
        private readonly CautionRepository _cautionRepository;
        private readonly BirthRepository _birthRepository;
        private readonly BirthWantedRepository _birthWantedRepository;

        public InsertBaseController(SqlContext context, WantedRepository wantedRepository, CrimesWantedRepository crimesWantedRepository, 
            CrimesRepository crimesRepository, CautionRepository cautionRepository, BirthRepository birthRepository, BirthWantedRepository birthWantedRepository)
        {
            _cautionRepository = cautionRepository;
            _crimesWantedRepository = crimesWantedRepository;
            _crimesRepository = crimesRepository;
            _wantedRepository = wantedRepository;
            _birthRepository = birthRepository;
            _birthWantedRepository = birthWantedRepository;
            _context = context;
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

                        if (responseBody.IsNullOrEmpty())
                        {
                            result = false;
                        }
                        else
                        {
                            // Desserializa a resposta em objetos C#
                            var settings = new JsonSerializerSettings
                            {
                                ContractResolver = new IgnoreOccupationsContractResolver()
                            };

                            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(responseBody, settings);
                            List<DataApiFBI> items = rootObject.Items;

                            foreach (var apiData in items)
                            {
                                await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(apiData));
                                // mapeamento de DataApiFBI para WantedModel
                                var wantedModel = new WantedModel
                                {
                                    Name = apiData.Title ?? "", // Se apiData.Title for nulo, atribui uma string vazia
                                    Height = apiData.HeightMax ?? 0, // Se apiData.HeightMax for nulo, atribui 0
                                    Weight = apiData.WeightMax ?? 0, // Se apiData.WeightMax for nulo, atribui 0
                                    Hair = apiData.Hair ?? "", // Se apiData.Hair for nulo, atribui uma string vazia
                                    EyesColor = apiData.Eyes ?? "", // Se apiData.Eyes for nulo, atribui uma string vazia
                                    Nacionalidade = apiData.Nationality ?? "", // Se apiData.Nationality for nulo, atribui uma string vazia
                                    Remarks = apiData.Remarks ?? "", // Se apiData.Remarks for nulo, atribui uma string vazia
                                    Sexo = apiData.Sex ?? "", // Se apiData.Sex for nulo, atribui uma string vazia
                                    LocalNascimento = apiData.PlaceOfBirth ?? "", // Se apiData.PlaceOfBirth for nulo, atribui uma string vazia
                                    Recompensa = apiData.RewardMax ?? 0, // Se apiData.RewardMax for nulo, atribui 0
                                    IdSource = apiData.Uid ?? "" // Se apiData.Uid for nulo, atribui uma string vazia
                                };

                                var wantedBase = _wantedRepository.GetBySourceId(apiData.Uid);

                                if (wantedBase == "[]")
                                {
                                    // Adicione o objeto WantedModel ao contexto
                                    _context.Wanted.Add(wantedModel);

                                    // Salve as mudanças no banco de dados
                                    await _context.SaveChangesAsync();
                                }

                                wantedBase = _wantedRepository.GetBySourceId(wantedModel.IdSource);
                                List<WantedModel> wantedList = JsonConvert.DeserializeObject<List<WantedModel>>(wantedBase);
                                WantedModel wantedPerson = wantedList[0];
                                
                                if (apiData.Details != null)
                                {
                                    var cautionBase = _cautionRepository.GetByDescription(apiData.Details).FirstOrDefault();

                                    if (cautionBase == null)
                                    {
                                        var cautionModel = new CautionModel
                                        {
                                            IdWanted = wantedPerson.Id,
                                            Name = apiData.Details,
                                        };
                                        // Adicione o objeto WantedModel ao contexto
                                        _context.Caution.Add(cautionModel);

                                    }
                                }
                                if (apiData.DatesOfBirthUsed != null)
                                {
                                    foreach (var birthDate in apiData.DatesOfBirthUsed)
                                    {
                                        //string formatoData = "MMMM d, yyyy";
                                        //DateTime data = DateTime.ParseExact(birthDate, formatoData, CultureInfo.InvariantCulture);

                                        var birthBase = _birthRepository.GetByDate(birthDate).FirstOrDefault();

                                        if (birthBase == null)
                                        {
                                            var birthModel = new BirthModel
                                            {
                                                Birth = birthDate
                                            };

                                            // Adicione o objeto WantedModel ao contexto
                                            _context.Birth.Add(birthModel);
                                            await _context.SaveChangesAsync();

                                        }

                                        birthBase = _birthRepository.GetByDate(birthDate).FirstOrDefault();
                                        var birthWantedRelationWanted = _birthWantedRepository.GetByWantedId((int)wantedPerson.Id).FirstOrDefault();
                                        var birthWantedRelationBirth = _birthWantedRepository.GetByBirthId((int)birthBase.Id).FirstOrDefault();

                                        if (birthWantedRelationWanted == null && birthWantedRelationBirth == null)
                                        {
                                            var birthWantedModel = new BirthWantedModel
                                            {

                                                IdBirth = birthBase.Id,
                                                IdWanted = wantedPerson.Id
                                            };

                                            _context.BirthWanted.Add(birthWantedModel);
                                        }
                                    }
                                }

                                if (apiData.Subjects != null)
                                {
                                    foreach (var subject in apiData.Subjects)
                                    {
                                        var crimeBase = _crimesRepository.GetByNameExactly(subject).FirstOrDefault();

                                        if (crimeBase == null)
                                        {
                                            var crimesModel = new CrimesModel
                                            {
                                                Name = subject
                                            };

                                            // Adicione o objeto WantedModel ao contexto
                                            _context.Crimes.Add(crimesModel);
                                            await _context.SaveChangesAsync();

                                        }

                                        crimeBase = _crimesRepository.GetByNameExactly(subject).FirstOrDefault();
                                        var crimeWantedRelationWanted = _crimesWantedRepository.GetByIdWanted((int)wantedPerson.Id);
                                        var crimeWantedRelationCrime = _crimesWantedRepository.GetByIdCrime((int)crimeBase.Id);

                                        if (crimeWantedRelationWanted == "[]" && crimeWantedRelationCrime == "[]")
                                        {
                                            var crimeWantedModel = new CrimeWantedModel
                                            {

                                                IdCrime = crimeBase.Id,
                                                IdWanted = wantedPerson.Id
                                            };

                                            _context.CrimeWanted.Add(crimeWantedModel);
                                        }
                                    }
                                }
                                // Salve as mudanças no banco de dados
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {
                        result = false;
                        return Ok("Houve um erro na solicitação da API.");
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

        [HttpPost("BuscarDadosDoInterpol")]
        public async Task<IActionResult> ConsultarEInserirDadosInterpol()
        {
            try
            {
                var client = new HttpClient();
                int pageSize = 160; // Alterado o tamanho da página para 160
                int page = 1;

                bool result = true;
                HashSet<string> processedEntityIds = new HashSet<string>();
                int consecutiveDuplicates = 0;

                while (result)
                {
                    string apiUrl = $"https://ws-public.interpol.int/notices/v1/red?page={page}&resultPerPage={pageSize}";

                    // Faz a solicitação GET à API da Interpol
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (responseBody.IsNullOrEmpty())
                        {
                            result = false;
                        }
                        else
                        {
                            // Desserializa a resposta em objetos C#
                            var interpolResponse = JsonConvert.DeserializeObject<InterpolResponse>(responseBody);

                            foreach (var notice in interpolResponse._embedded.notices)
                            {
                                if (processedEntityIds.Contains(notice.entity_id))
                                {
                                    // Esta entidade já foi processada, então pule para a próxima
                                    consecutiveDuplicates++;

                                    if (consecutiveDuplicates >= 5)
                                    {
                                        // Parar o loop se encontrarmos a mesma entidade 5 vezes seguidas
                                        result = false;
                                        break;
                                    }

                                    continue;
                                }
                                else
                                {
                                    consecutiveDuplicates = 0;
                                }
                                // Consulta a segunda rota para obter mais informações sobre o usuário
                                string entityUrl = $"https://ws-public.interpol.int/notices/v1/red/{notice.entity_id.Replace("/","%2F")}";
                                HttpResponseMessage entityResponse = await client.GetAsync(entityUrl);

                                if (entityResponse.IsSuccessStatusCode)
                                {
                                    string entityResponseBody = await entityResponse.Content.ReadAsStringAsync();
                                    var entityData = JsonConvert.DeserializeObject<EntityData>(entityResponseBody);

                                    await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(entityData));
                                    // Crie um objeto WantedModel e mapeie os dados
                                    var wantedModel = new WantedModel
                                    {
                                        Name = ((entityData.forename ?? "") + " " + (entityData.name ?? "")).TrimEnd().TrimStart(),
                                        Height = !string.IsNullOrEmpty(entityData.height) ? (decimal?)Convert.ToDecimal(entityData.height) : 0,
                                        Weight = !string.IsNullOrEmpty(entityData.weight) ? (decimal?)Convert.ToDecimal(entityData.weight) : 0,
                                        Recompensa = 0,
                                        Hair = entityData.hairs_id != null && entityData.hairs_id.Any() ? entityData.hairs_id[0] : "",
                                        EyesColor = entityData.eyes_colors_id != null && entityData.eyes_colors_id.Any() ? entityData.eyes_colors_id[0] : "",
                                        Nacionalidade = entityData.nationalities != null && entityData.nationalities.Any() ? entityData.nationalities[0] : "",
                                        Remarks = entityData.distinguishing_marks ?? "",
                                        Sexo = entityData.sex_id ?? "",
                                        LocalNascimento = entityData.place_of_birth ?? "",
                                        IdSource = notice.entity_id
                                    };

                                    var wantedBase = _wantedRepository.GetBySourceId(notice.entity_id);

                                    if (wantedBase == "[]")
                                    {
                                        // Adicione o objeto WantedModel ao contexto
                                        _context.Wanted.Add(wantedModel);

                                        // Salve as mudanças no banco de dados
                                        await _context.SaveChangesAsync();
                                    }

                                    wantedBase = _wantedRepository.GetBySourceId(wantedModel.IdSource);
                                    List<WantedModel> wantedList = JsonConvert.DeserializeObject<List<WantedModel>>(wantedBase);
                                    WantedModel wantedPerson = wantedList[0];
                                   
                                    if (entityData.date_of_birth != null)
                                    {
                                        
                                        //string formatoData = "MMMM d, yyyy";
                                        //DateTime data = DateTime.ParseExact(birthDate, formatoData, CultureInfo.InvariantCulture);

                                        var birthBase = _birthRepository.GetByDate(entityData.date_of_birth).FirstOrDefault();

                                        if (birthBase == null)
                                        {
                                            var birthModel = new BirthModel
                                            {
                                                Birth = entityData.date_of_birth
                                            };

                                            // Adicione o objeto WantedModel ao contexto
                                            _context.Birth.Add(birthModel);
                                            await _context.SaveChangesAsync();

                                        }

                                        birthBase = _birthRepository.GetByDate(entityData.date_of_birth).FirstOrDefault();
                                        var birthWantedRelationWanted = _birthWantedRepository.GetByWantedId((int)wantedPerson.Id).FirstOrDefault();
                                        var birthWantedRelationBirth = _birthWantedRepository.GetByBirthId((int)birthBase.Id).FirstOrDefault();

                                        if (birthWantedRelationWanted == null && birthWantedRelationBirth == null)
                                        {
                                            var birthWantedModel = new BirthWantedModel
                                            {

                                                IdBirth = birthBase.Id,
                                                IdWanted = wantedPerson.Id
                                            };

                                            _context.BirthWanted.Add(birthWantedModel);
                                        }
                                        
                                    }

                                    if (entityData.arrest_warrants != null && entityData.arrest_warrants.Count > 0)
                                    {
                                        foreach (var subject in entityData.arrest_warrants)
                                        {
                                            var crimeBase = _crimesRepository.GetByNameExactly((string)subject.charge).FirstOrDefault();

                                            if (crimeBase == null)
                                            {
                                                var crimesModel = new CrimesModel
                                                {
                                                    Name = subject.charge
                                                };

                                                // Adicione o objeto WantedModel ao contexto
                                                _context.Crimes.Add(crimesModel);
                                                await _context.SaveChangesAsync();

                                            }

                                            crimeBase = _crimesRepository.GetByNameExactly((string)subject.charge).FirstOrDefault();
                                            if (crimeBase == null)
                                            {
                                                await Task.Delay(1000);
                                                crimeBase = _crimesRepository.GetByNameExactly((string)subject.charge).FirstOrDefault();
                                                if (crimeBase == null)
                                                {
                                                    continue;
                                                }
                                            }
                                            var crimeWantedRelationWanted = _crimesWantedRepository.GetByIdWanted((int)wantedPerson.Id);
                                            var crimeWantedRelationCrime = _crimesWantedRepository.GetByIdCrime((int)crimeBase.Id);

                                            if (crimeWantedRelationWanted == "[]" && crimeWantedRelationCrime == "[]")
                                            {
                                                var crimeWantedModel = new CrimeWantedModel
                                                {

                                                    IdCrime = crimeBase.Id,
                                                    IdWanted = wantedPerson.Id
                                                };

                                                _context.CrimeWanted.Add(crimeWantedModel);
                                            }
                                        }
                                    }

                                    await _context.SaveChangesAsync();
                                    processedEntityIds.Add(notice.entity_id);
                                }
                                else
                                {
                                    result = false;
                                    return Ok("Houve um erro na solicitação da API.");
                                }
                            }
                        }
                    }
                    else
                    {
                        result = false;
                        return Ok("Houve um erro na solicitação da API da Interpol.");
                    }

                    page++;
                }

                return Ok("Dados inseridos com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao consultar a API da Interpol e inserir os dados: {ex.Message}");
            }
        }
    }
}
