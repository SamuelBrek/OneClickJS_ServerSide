using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace OneClickJS.Api.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertarParametrosPaginacionEnRespuesta<T>(this HttpContext context, 
        IQueryable<T> queryable, int CantidadAMostrar)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            double conteo = await queryable.CountAsync();
            double totalPaginas = Math.Ceiling(conteo/CantidadAMostrar);
            context.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
            
        }
    }
}