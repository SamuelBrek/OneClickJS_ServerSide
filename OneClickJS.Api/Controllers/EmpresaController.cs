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
using OneClickJS.Api.Helpers;
using OneClickJS.Infraestructure.Data;

namespace OneClickJS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly OneClickJSContext _context;
        private readonly IEmpresaService _service;
        private readonly IEmpresaRepository _repository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IValidator<EmpresaCreateRequest> _createValidator;
        private readonly IValidator<EmpresaUpdateRequest> _updateValidator;

        public EmpresaController(IEmpresaService service, IEmpresaRepository repository, IHttpContextAccessor httpContext, IMapper mapper, IValidator<EmpresaCreateRequest> createValidator, IValidator<EmpresaUpdateRequest> updateValidator, OneClickJSContext context) 
        {
            this._service = service;
            this._repository = repository;
            this._httpContext = httpContext;
            this._mapper = mapper;
            this._createValidator = createValidator;
            this._updateValidator = updateValidator;
            this._context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetEmpresas()
        {
            var empresas = await _repository.GetEmpresas();
            //var respuesta = _mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaResponse>>(empresas);
            return Ok(empresas);
        }
        
        [HttpGet]
        [Route("GetEmpresas2")]
        public async Task<IActionResult> GetEmpresas2([FromQuery]Paginacion paginacion, [FromQuery] string nombre)
        {
            var queryable =  _context.Empresas.AsQueryable();
            if(!string.IsNullOrEmpty(nombre))
            {
                queryable = queryable.Where(emp => emp.NombreEmpresa.Contains(nombre));
            }
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadAMostrar);
            var empresas = await _repository.GetEmpresas();
            //var respuesta = _mapper.Map<IEnumerable<Empresa>, IEnumerable<EmpresaResponse>>(empresas);
            return Ok(queryable.Paginar(paginacion));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var empresa = await _repository.GetById(id);

            if(empresa == null)
                return NotFound("No fue posible encontrar una empresa con el id ingresado");

            //var respuesta = _mapper.Map<Empresa, EmpresaResponse>(empresa);
            return Ok(empresa);
        } 

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateEmpresa(EmpresaCreateRequest newEmpresa)
        {
            var validate = await _createValidator.ValidateAsync(newEmpresa);

            if(!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => Error: {x.ErrorMessage}"));
            
            var entity = _mapper.Map<EmpresaCreateRequest, Empresa>(newEmpresa);
            int id = await _repository.CreateEmpresa(entity);
            
            if(id <= 0)
                return Conflict("El registro no pudo realizarse, verifica los datos...");

            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/empresa/{id}";
            //return Created(urlResult, id);
            return Ok(newEmpresa);
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateEmpresa(int id,[FromBody]EmpresaUpdateRequest updateEmpresa)
        {
            var validate = await _updateValidator.ValidateAsync(updateEmpresa);
            if(!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(dest => $"{dest.PropertyName} => Error: {dest.ErrorMessage}"));
            
            var entity = _mapper.Map<EmpresaUpdateRequest, Empresa>(updateEmpresa);

            if(id <= 0)
                return NotFound($"No se encontró un usuario con el id introducido: {id}");

            var entity2 = await _repository.GetById(id);

            if(entity2 == null)
                return NotFound($"No se encontró un usuario con la id introducida: {id}");

            var Id = await _repository.UpdateEmpresa(id, entity);
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/empresa/{id}";

            //return Ok("El usuario se ha editado correctamente");
            return Ok(updateEmpresa);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            if (id <= 0)
                return NotFound("No se encontró un usuario con el id introducido...");

            var entity = await _repository.GetById(id);
            if(entity == null)
                return NotFound("No se encontró un usuario con el valor introducido...");
            var deleted = await _repository.DeleteEmpresa(id);
            if(!deleted)
                Conflict("Ocurrió un error al intentar eliminar al usuario...");
            return Ok("Empresa eliminada correctamente");
        }
    }
}