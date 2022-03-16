using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneClickJS.Api.Helpers;
using OneClickJS.Domain.Entities;
using OneClickJS.Infraestructure.Data;
using OneClickJS.Infraestructure.Repositories;

namespace OneClickJS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostulacionController : ControllerBase
    {
        private readonly OneClickJSContext _context;
        public PostulacionController(OneClickJSContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetPostulaciones()
        {
            var repository = new PostulacionSqlRepository();
            var postulaciones = repository.GetPostulaciones();

            //CategoriaSqlRepository categorias = new CategoriaSqlRepository();
            return Ok(postulaciones);
        }
        [HttpGet]
        [Route("GetPostulaciones2")]
        public async Task<IActionResult> GetPostulaciones2([FromQuery]Paginacion paginacion)
        {
            var queryable =  _context.Postulaciones.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadAMostrar);
            PostulacionSqlRepository categorias = new PostulacionSqlRepository();
            return Ok(queryable.Paginar(paginacion));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById (int id)
        {
            PostulacionSqlRepository postulaciones = new PostulacionSqlRepository();
            var postulacion = postulaciones.GetById(id);
            if (postulacion == null)
            {
                return NotFound($"Has ingresado el id {id}, sin embargo, no existe dicha postulacion.");
            }
            return Ok (postulacion);
        }
        
        [HttpPost]
        [Route("Create")]
        public IActionResult CreatePostulacion (Postulacione newPostulacion)
        {
            PostulacionSqlRepository postulaciones = new PostulacionSqlRepository();
            
            try
            {
                postulaciones.CreatePostulacion(newPostulacion);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "No es posible realizar el registro, no cambies el valor del id o déjalo en 0.");
            }
            //return Ok("Se ha agregado la categoria");
            return Ok(newPostulacion);
        }
        
        [HttpPut]
        [Route("Update/{id:int}")]
        public IActionResult UpdatePostulacion (int id, Postulacione updatePostulacion)
        {
            PostulacionSqlRepository postulaciones = new PostulacionSqlRepository();
            var validation = postulaciones.GetById(id);
            if (validation == null)
            {
                return NotFound($"Has ingresado el id {id}, sin embargo, no existe dicha postulacion.");
            }
            postulaciones.UpdatePostulacion(id, updatePostulacion);
            return Ok(updatePostulacion);
            //return Ok("Se ha actualizado la categoría");
        }
        
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult DeletePostulacion(int id)
        {
            PostulacionSqlRepository postulaciones = new PostulacionSqlRepository();
            var validation = postulaciones.GetById(id);
            if (validation == null)
            {
                return NotFound($"Has ingresado el id {id}, sin embargo, no existe dicha postulacion.");
            }
            postulaciones.DeletePostulacion(id);
            return Ok($"Se ha eliminado la postulacion con el id {id}");
        }
    }
}