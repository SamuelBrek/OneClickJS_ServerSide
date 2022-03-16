using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneClickJS.Infraestructure.Repositories;
using OneClickJS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using OneClickJS.Domain.Interfaces;
using OneClickJS.Application.Services;
using AutoMapper;
using OneClickJS.Domain.Dtos.Responses;
using OneClickJS.Domain.Dtos.Request;
using FluentValidation;
using OneClickJS.Infraestructure.Data;
using OneClickJS.Api.Helpers;

namespace OneClickJS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleoController : ControllerBase
    {
        private readonly OneClickJSContext _context;
        private readonly IEmpleoService _service;
        private readonly IEmpleoRepository _repository;
        private readonly CategoriaSqlRepository _repositoryCat;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IValidator<EmpleoCreateRequest> _createValidator;
        private readonly IValidator<EmpleoUpdateRequest> _updateValidator;

        public EmpleoController(IEmpleoService service, IEmpleoRepository repository, IHttpContextAccessor httpContext, IMapper mapper, IValidator<EmpleoCreateRequest> createValidator, IValidator<EmpleoUpdateRequest> updateValidator, OneClickJSContext context, CategoriaSqlRepository repositoryCat)
        {
            this._service = service;
            this._repository = repository;
            this._httpContext = httpContext;
            this._mapper = mapper;
            this._createValidator = createValidator;
            this._updateValidator = updateValidator;
            this._context = context;
            this._repositoryCat = repositoryCat;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetEmpleos()
        {
            var empleos = await _repository.GetEmpleos();


            var respuesta = _mapper.Map<IEnumerable<Empleo>, IEnumerable<EmpleoResponse>>(empleos);

            return Ok(empleos);
        }
        [HttpGet]
        [Route("GetEmpleos2")]
        public async Task<IActionResult> GetEmpleos2([FromQuery]Paginacion paginacion, [FromQuery] string nombre)
        {
            var queryable =  _context.Empleos.AsQueryable();

            /*var queryable = (from em in _context.Empleos
            join cat in _context.Categorias
            on em.IdCategoria equals cat.IdCategoria
            join emp in _context.Empresas
            on em.IdEmpresa equals emp.IdEmpresa
            select new {em.IdEmpleo, em.NombreEmpleo, em.VacantesEmpleo, em.PrestacionesEmpleo,
            em.SueldoEmpleo, em.MunicipioEmpleo, em.DescripcionEmpleo, em.TipoEmpleo, em.FotoEmpleo 
            ,cat.NombreCategoria, emp.NombreEmpresa}).AsQueryable();*/


            if(!string.IsNullOrEmpty(nombre))
            {
                queryable = queryable.Where(emp => emp.NombreEmpleo.Contains(nombre));
            }
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadAMostrar);
            var empleos = await _repository.GetEmpleos();
            //var respuesta = _mapper.Map<IEnumerable<Empleo>, IEnumerable<EmpleoResponse>>(empleos);
            return Ok(queryable.Paginar(paginacion));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var empleo = await _repository.GetById(id);

            if(empleo == null)
                return NotFound("No fue posible encontrar un empleo con e id que ingresó");

            //var respuesta = _mapper.Map<Empleo, EmpleoResponse>(empleo);
            return Ok(empleo);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateEmpleo(EmpleoCreateRequest newEmpleo)
        {
            var validate = await _createValidator.ValidateAsync(newEmpleo);

            if(!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => Error: {x.ErrorMessage}"));
            
            var entity = _mapper.Map<EmpleoCreateRequest, Empleo>(newEmpleo);
            int id = await _repository.CreateEmpleo(entity);
            
            if(id <= 0)
                return Conflict("El registro no pudo realizarse, verifica los datos...");

            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/empleo/{id}";
            //return Created(urlResult, id);
            return Ok(newEmpleo);
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateEmpleo(int id,[FromBody]EmpleoUpdateRequest updateEmpleo)
        {
            var validate = await _updateValidator.ValidateAsync(updateEmpleo);
            if(!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(dest => $"{dest.PropertyName} => Error: {dest.ErrorMessage}"));
            
            var entity = _mapper.Map<EmpleoUpdateRequest, Empleo>(updateEmpleo);

            if(id <= 0)
                return NotFound($"No se encontró un usuario con el id introducido: {id}");

            var entity2 = await _repository.GetById(id);

            if(entity2 == null)
                return NotFound($"No se encontró un usuario con la id introducida: {id}");

            var Id = await _repository.UpdateEmpleo(id, entity);
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/empleo/{id}";

            //return Ok("El empleo se ha editado correctamente");
            return Ok(updateEmpleo);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteEmpleo(int id)
        {
            if (id <= 0)
                return NotFound("No se encontró un usuario con el id introducido...");

            var entity = await _repository.GetById(id);
            if(entity == null)
                return NotFound("No se encontró un usuario con el valor introducido...");
            var deleted = await _repository.DeleteEmpleo(id);
            if(!deleted)
                Conflict("Ocurrió un error al intentar eliminar al usuario...");
            return Ok("Empleo eliminado correctamente");
        }
    }
}