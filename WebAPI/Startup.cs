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
            //Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject -->IoC Container altyapýsý sunuyorlar.
            //Sadece injection yapacak olsaydýk .Net yeterli oluyor AOP yapacaksak yetmiyor. Mesela Loglama yapacak olsak


            services.AddControllers();
            services.AddSingleton<IProductService,ProductManager>();//BANA ARKA PLANDA BÝR REFERANS OLUÞTUR DEMEK. Yani Apide IProductService görürse ProductManager i newliyor. bir tane Product Manager oluþturuyor. Bir milyon tane client bile gelse ayný instance i veriyor. Ama data tutanlarda yapýlmaz. Yani sepet de mesela .herkesin sepeti farklý biri sepetden bir ürün çýkarýrsa herkesden çýkar. veya biri eklerse hepsine eklenir gibi
            services.AddSingleton<IProductDal, EfProductDal>(); //burada ProductManageri çalýtýrýrken karþýlaþtýðý baþka bir baðýmlýlýðý çözüyoruz.
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
