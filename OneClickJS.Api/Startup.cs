using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OneClickJS.Infraestructure.Data;
using OneClickJS.Application.Services;
using OneClickJS.Domain.Interfaces;
using OneClickJS.Infraestructure.Repositories;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using FluentValidation;
using OneClickJS.Domain.Dtos.Request;
using OneClickJS.Infraestructure.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OneClickJS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OneClickJS.Api", Version = "v1" });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthorizationCore();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IEmpleoService, EmpleoService>();

            services.AddTransient<IUsuarioRepository, UsuarioSqlRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaSqlrepository>();
            services.AddTransient<CategoriaSqlRepository, CategoriaSqlRepository>();
            services.AddTransient<PostulacionSqlRepository, PostulacionSqlRepository>();
            services.AddTransient<IEmpleoRepository, EmpleoSqlRepository>();
            
            services.AddDbContext<OneClickJSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OneClickJS")));
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<OneClickJSContext>().AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                    ClockSkew = TimeSpan.Zero
                });


            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //USUARIOS
            services.AddScoped<IValidator<UsuarioCreateRequest>, UsuarioCreateRequestValidator>();
            services.AddScoped<IValidator<UsuarioUpdateRequest>, UsuarioUpdateRequestValidator>();

            //EMPRESAS
            services.AddScoped<IValidator<EmpresaCreateRequest>, EmpresaCreateRequestValidator>();
            services.AddScoped<IValidator<EmpresaUpdateRequest>, EmpresaUpdateRequestValidator>();

            //EMPLEOS
            services.AddScoped<IValidator<EmpleoCreateRequest>, EmpleoCreateRequestValidator>();
            services.AddScoped<IValidator<EmpleoUpdateRequest>, EmpleoUpdateRequestValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OneClickJS.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
