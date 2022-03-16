using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;
using OneClickJS.Infraestructure.Data;

namespace OneClickJS.Infraestructure.Repositories
{
    public class CategoriaSqlRepository
    {
        private readonly OneClickJSContext _context;
        public CategoriaSqlRepository(){
            _context = new OneClickJSContext();
        }
        public IEnumerable<Categoria> GetCategorias()
        {
            return _context.Categorias;
        }
        public Categoria GetById(int id)
        {
            var categoria = _context.Categorias.Where(cat => cat.IdCategoria == id);
            return categoria.FirstOrDefault();
        }
        public void CreateCategoria(Categoria newCategoria)
        {
            var entity = newCategoria;
            _context.Categorias.Add(entity);
            var rows =  _context.SaveChanges();
            if(rows <= 0)
                throw new Exception("INCORRECTO! No se ha podido registrar la categoria");

            return;
        }
        public void UpdateCategoria (int id, Categoria updateCategoria)
        {
            if (id <= 0 || updateCategoria == null)
            {
                throw new ArgumentException("Falta aún información por completar");
            }
            var entity = GetById(id);

            entity.NombreCategoria = updateCategoria.NombreCategoria;
            _context.Update(entity);
            var rows =  _context.SaveChanges();
            return;
        }
        public void DeleteCategoria (int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Falta aún información por completar");
            }
            var result = GetById(id);
            _context.Remove(result);
            var rows =  _context.SaveChanges();
            return;
        }
    }
}