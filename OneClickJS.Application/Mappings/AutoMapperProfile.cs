using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OneClickJS.Domain.Entities;
using OneClickJS.Domain.Dtos.Responses;
using OneClickJS.Domain.Dtos.Request;

namespace OneClickJS.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //USUARIOS
            CreateMap<Usuario, UsuarioResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.NombreUsuario} {src.ApellidoUsuario}"));

            CreateMap<UsuarioCreateRequest, Usuario>();

            CreateMap<UsuarioUpdateRequest, Usuario>();

            //EMPRESAS
            CreateMap<Empresa, EmpresaResponse>()
            .ForMember(dest => dest.Direccion, opt => 
            opt.MapFrom(src => $"Calle {src.CalleEmpresa}, #{src.NumeroEmpresa}, entre {src.CruzamientoEmpresa}, colonia {src.ColoniaEmpresa}"));

            CreateMap<EmpresaCreateRequest, Empresa>();
            CreateMap<EmpresaUpdateRequest, Empresa>();

            //EMPLEOS
            CreateMap<Empleo, EmpleoResponse>();

            CreateMap<EmpleoCreateRequest, Empleo>();
            CreateMap<EmpleoUpdateRequest, Empleo>();
            
        }
    }
}