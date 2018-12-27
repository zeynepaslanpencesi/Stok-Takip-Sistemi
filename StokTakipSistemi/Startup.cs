using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StokTakipSistemi.Data;
using StokTakipSistemi.DTOModels;
using StokTakipSistemi.Helpers;
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
            services.AddOptions();
            services.Configure<LoginVM>(Configuration);
            var config = new LoginVM();
            Configuration.Bind("LoginVM", config);
            services.AddSingleton(config);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/Account/Login/";

                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };

                    options.Events.OnRedirectToAccessDenied = (context) =>
                    {
                        context.Response.StatusCode = 401;
                        context.Response.Redirect("/Account/Unauthorized/");
                        return Task.CompletedTask;
                    };
                });
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
            services.AddScoped<IUrunTurService, UrunTurService>();
            services.AddScoped<ISiparisService, SiparisService>();
            services.AddScoped<IFaturaService, FaturaService>();
            services.AddScoped<IUnvanService, UnvanService>();
            services.AddScoped<IKullaniciService, KullaniciService>();
            services.AddScoped((ctx) =>
            {
                StokTakipSistemiDbContext svc = ctx.GetService<StokTakipSistemiDbContext>();
                return new Helper(svc);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, StokTakipSistemiDbContext dbContext)
        {
            app.UseAuthentication();

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
                config.CreateMap<UrunSaglayiciVM, UrunSaglayici>();
                config.CreateMap<UrunTurVM, UrunTur>();
                config.CreateMap<SiparisVM, Siparis>();
                config.CreateMap<FaturaVM, Fatura>();
                config.CreateMap<UnvanVM, Unvan>();
                config.CreateMap<KullaniciVM, Kullanici>();
                config.CreateMap<Urun, UrunDTO>();                
                config.CreateMap<Siparis, SiparisDTO>();
                config.CreateMap<Siparis, SiparisSelfDTO>();


            });

            app.UseMvc(routes => {
                routes.MapRoute(
                    "default",
                    "{controller=Account}/{action=Login}/"
                );
            });

            app.Use((context, next) =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    var route = context.GetRouteData();
                    if (route.Values["controller"].ToString() == "Acccount" &&
                        route.Values["action"].ToString() == "Login")
                    {
                        context.Response.Redirect("/Urun/Index");
                    }
                }

                return next();
            });
        }
    }
}
