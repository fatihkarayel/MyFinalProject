using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
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
            //Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject -->IoC Container altyap�s� sunuyorlar.
            //Sadece injection yapacak olsayd�k .Net yeterli oluyor AOP yapacaksak yetmiyor. Mesela Loglama yapacak olsak


            services.AddControllers();
            services.AddSingleton<IProductService,ProductManager>();//BANA ARKA PLANDA B�R REFERANS OLU�TUR DEMEK. Yani Apide IProductService g�r�rse ProductManager i newliyor. bir tane Product Manager olu�turuyor. Bir milyon tane client bile gelse ayn� instance i veriyor. Ama data tutanlarda yap�lmaz. Yani sepet de mesela .herkesin sepeti farkl� biri sepetden bir �r�n ��kar�rsa herkesden ��kar. veya biri eklerse hepsine eklenir gibi
            services.AddSingleton<IProductDal, EfProductDal>(); //burada ProductManageri �al�t�r�rken kar��la�t��� ba�ka bir ba��ml�l��� ��z�yoruz.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
