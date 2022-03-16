using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace OneClickJS.Domain.Dtos.Responses
{
    public class UsuarioResponse
    {
        public int IdUsuario{get; set;}
        public string FullName{get; set;}
        public string CorreoUsuario{get; set;}
    }
}