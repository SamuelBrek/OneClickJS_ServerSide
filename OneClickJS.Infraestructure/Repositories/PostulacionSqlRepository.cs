using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneClickJS.Domain.Entities;
using OneClickJS.Infraestructure.Data;

namespace OneClickJS.Infraestructure.Repositories
{
    public class PostulacionSqlRepository
    {
        private readonly OneClickJSContext _context;
        public PostulacionSqlRepository(){
            _context = new OneClickJSContext();
        }
        public IEnumerable<Postulacione> GetPostulaciones()
        {
            return _context.Postulaciones;
        }
        public Postulacione GetById(int id)
        {
            var postulacion = _context.Postulaciones.Where(post => post.IdPostulacion == id);
            return postulacion.FirstOrDefault();
        }
        public void CreatePostulacion(Postulacione newPostulacion)
        {
            var entity = newPostulacion;
            _context.Postulaciones.Add(entity);
            var rows =  _context.SaveChanges();
            if(rows <= 0)
                throw new Exception("INCORRECTO! No se ha podido registrar la postulacion");

            return;
        }
        public void UpdatePostulacion (int id, Postulacione updatePostulacion)
        {
            if (id <= 0 || updatePostulacion == null)
            {
                throw new ArgumentException("Falta aún información por completar");
            }
            var entity = GetById(id);

            entity.EstadoPostulacion = updatePostulacion.EstadoPostulacion;
            _context.Update(entity);
            var rows =  _context.SaveChanges();
            return;
        }
        public void DeletePostulacion (int id)
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