using System.Net.Security;
using System.Net;
using System.Reflection.Metadata.Ecma335;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OneClickJS.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly OneClickJSContext _context;
        private readonly IUsuarioService _service;
        private readonly IUsuarioRepository _repository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        private readonly IValidator<UsuarioCreateRequest> _createValidator;
        private readonly IValidator<UsuarioUpdateRequest> _updateValidator;

        public UsuarioController(IUsuarioService service, IUsuarioRepository repository, IHttpContextAccessor httpContext, IMapper mapper, IValidator<UsuarioCreateRequest> createValidator, IValidator<UsuarioUpdateRequest> updateValidator, OneClickJSContext context)
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
        public async Task<IActionResult> GetUsuarios()
        {
            
            var usuarios = await _repository.GetUsuarios();
            //var respuesta = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioResponse>>(usuarios);
            return Ok(usuarios);        
        }
        [HttpGet]
        [Route("GetUsuarios2")]
        public async Task<IActionResult> GetUsuarios2([FromQuery]Paginacion paginacion, [FromQuery] string nombre)
        {
            var queryable =  _context.Usuarios.AsQueryable();
            if(!string.IsNullOrEmpty(nombre))
            {
                queryable = queryable.Where(use => use.NombreUsuario.Contains(nombre));
            }
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadAMostrar);
            var usuarios = await _repository.GetUsuarios();
            //var respuesta = _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioResponse>>(usuarios);
            return Ok(queryable.Paginar(paginacion));        
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _repository.GetById(id);
            if(usuario == null)
                return NotFound($"No existe un usuario con el id: {id}");

            //var respuesta = _mapper.Map<Usuario, UsuarioResponse>(usuario);
            return Ok(usuario);
        } 




        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUsuario(UsuarioCreateRequest newUsuario)
        {

            var validate = await _createValidator.ValidateAsync(newUsuario);

            if(!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => Error: {x.ErrorMessage}"));
            
            var entity = _mapper.Map<UsuarioCreateRequest, Usuario>(newUsuario);
            int id = await _repository.CreateUsuario(entity);
            
            if(id <= 0)
                return Conflict("El registro no pudo realizarse, verifica los datos...");
            
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/usuario/{id}";

            //return Created(urlResult, id);
            return Ok(newUsuario);
            
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateUsuario(int id,[FromBody]UsuarioUpdateRequest updateUsuario)
        {
            var validate = await _updateValidator.ValidateAsync(updateUsuario);
            if(!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(dest => $"{dest.PropertyName} => Error: {dest.ErrorMessage}"));
            
            var entity = _mapper.Map<UsuarioUpdateRequest, Usuario>(updateUsuario);

            if(id <= 0)
                return NotFound($"No se encontró un usuario con el id introducido: {id}");

            var entity2 = await _repository.GetById(id);

            if(entity2 == null)
                return NotFound($"No se encontró un usuario con la id introducida: {id}");

            var Id = await _repository.UpdateUsuario(id, entity);
            var host = _httpContext.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/usuario/{id}";

            //return Ok("El usuario se ha editado correctamente");
            return Ok(updateUsuario);
        }


        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (id <= 0) 
                return NotFound("No se encontró un usuario con el id introducido...");

            var entity = await _repository.GetById(id);
            if(entity == null)
                return NotFound("No se encontró un usuario con el valor introducido...");
            var deleted = await _repository.DeleteUsuario(id);
            if(!deleted)
                Conflict("Ocurrió un error al intentar eliminar al usuario...");
            return NoContent();
        }
    }
}