using Business.Abstract;
using Business.Concrete;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();
            //services.AddSingleton<IProductService,ProductManager>();//BANA ARKA PLANDA B�R REFERANS OLU�TUR DEMEK. Yani Apide IProductService g�r�rse ProductManager i newliyor. bir tane Product Manager olu�turuyor. Bir milyon tane client bile gelse ayn� instance i veriyor. Ama data tutanlarda yap�lmaz. Yani sepet de mesela .herkesin sepeti farkl� biri sepetden bir �r�n ��kar�rsa herkesden ��kar. veya biri eklerse hepsine eklenir gibi
            //services.AddSingleton<IProductDal, EfProductDal>(); //burada ProductManageri �al�t�r�rken kar��la�t��� ba�ka bir ba��ml�l��� ��z�yoruz.
            services.AddCors(); //api de cors injection� yapmak gerekiyor.


            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            services.AddDependencyResolvers(new ICoreModule[] { new CoreModule()});
            //ServiceTool.Create(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureCustomExceptionMiddleware(); //t�m sistemi try catch i�ine al�yor. her yere tek tek yazmak yerine

            app.UseCors(builder=>builder.WithOrigins("http://localhost:4200").AllowAnyHeader()); //buradaki s�ras� �nemli en ba�ta olmal�. Bu adresden gelen isteklere g�ven diye yaz�yoruz bunu.

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
