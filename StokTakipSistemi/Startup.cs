using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StokTakipSistemi.Data;
using StokTakipSistemi.DTOModels;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services;
using StokTakipSistemi.Services.Interfaces;
using StokTakipSistemi.ViewModels;

namespace StokTakipSistemi
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
            services.AddDbContext<StokTakipSistemiDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();

            services.AddScoped<ISehirService, SehirService>();
            services.AddScoped<IIlceService, IlceService>();
            services.AddScoped<IAdresService, AdresService>();
            services.AddScoped<IDepartmanService, DepartmanService>();
            services.AddScoped<IFirmaService, FirmaService>();
            services.AddScoped<IUrunService, UrunService>();
            services.AddScoped<IMarkaService, MarkaService>();
            services.AddScoped<IUrunSaglayiciService, UrunSaglayiciService>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, StokTakipSistemiDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           

            app.UseStaticFiles();

            Mapper.Initialize(config =>
            {
                config.CreateMap<SehirVM, Sehir>();
                config.CreateMap<IlceVM, Ilce>();
                config.CreateMap<AdresVM, Adres>();
                config.CreateMap<DepartmanVM, Departman>();
                config.CreateMap<FirmaVM, Firma>();
                config.CreateMap<UrunVM, Urun>();
                config.CreateMap<MarkaVM, Marka>();
                config.CreateMap<Urun, UrunDTO>();


            });

                app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Urun}/{action=Index}/{id?}");
            });
        }
    }
}
