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
    public class CategoriaController : ControllerBase
    {
        private readonly OneClickJSContext _context;
        public CategoriaController(OneClickJSContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetCategorias()
        {
            var repository = new CategoriaSqlRepository();
            var categorias = repository.GetCategorias();

            //CategoriaSqlRepository categorias = new CategoriaSqlRepository();
            return Ok(categorias);
        }
        [HttpGet]
        [Route("GetCategorias2")]
        public async Task<IActionResult> GetCategorias2([FromQuery]Paginacion paginacion, [FromQuery] string nombre)
        {
            var queryable =  _context.Categorias.AsQueryable();
            if(!string.IsNullOrEmpty(nombre))
            {
                queryable = queryable.Where(cat => cat.NombreCategoria.Contains(nombre));
            }
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadAMostrar);
            CategoriaSqlRepository categorias = new CategoriaSqlRepository();
            return Ok(queryable.Paginar(paginacion));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById (int id)
        {
            CategoriaSqlRepository categorias = new CategoriaSqlRepository();
            var categoria = categorias.GetById(id);
            if (categoria == null)
            {
                return NotFound($"Has ingresado el id {id}, sin embargo, no existe dicha categoría.");
            }
            return Ok (categoria);
        }
        
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateMovie (Categoria newCategoria)
        {
            CategoriaSqlRepository categorias = new CategoriaSqlRepository();
            categorias.CreateCategoria(newCategoria);
            // try
            // {
            //     categorias.CreateCategoria(newCategoria);
            // }
            // catch
            // {
            //     return StatusCode(StatusCodes.Status500InternalServerError,
            //     "No es posible realizar el registro, no cambies el valor del id o déjalo en 0.");
            // }
            //return Ok("Se ha agregado la categoria");
            return Ok(newCategoria);
        }
        
        [HttpPut]
        [Route("Update/{id:int}")]
        public IActionResult UpdateCategoria (int id, Categoria updateCategoria)
        {
            CategoriaSqlRepository categorias = new CategoriaSqlRepository();
            var validation = categorias.GetById(id);
            if (validation == null)
            {
                return NotFound($"Has ingresado el id {id}, sin embargo, no existe dicha categoria.");
            }
            categorias.UpdateCategoria(id, updateCategoria);
            return Ok(updateCategoria);
            //return Ok("Se ha actualizado la categoría");
        }
        
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult DeleteCategoria (int id)
        {
            CategoriaSqlRepository categorias = new CategoriaSqlRepository();
            var validation = categorias.GetById(id);
            if (validation == null)
            {
                return NotFound($"Has ingresado el id {id}, sin embargo, no existe dicha categoría.");
            }
            categorias.DeleteCategoria(id);
            return Ok($"Se ha eliminado la categoria con el id {id}");
        }
    }
}