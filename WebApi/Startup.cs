using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.DBOperations;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi
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
            // bunu controllerdan önce koyman lazım                            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                // tokenın nasıl çalışcağını belirttiğin yer
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    // özellikleri doğrula 
                    ValidateAudience = true, // kişiler 
                    ValidateIssuer = true,   // veren
                    ValidateLifetime = true, // süre
                    ValidateIssuerSigningKey = true, // tokenı imzalayıp kriptolıyacağımız yer
                    // token Iconfigurationdan geliyor tokenı veren kişi kim onu yaz
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Audience"],
                    // keyin şifresini çözmek için burdaki configi kullancaksın
                    // normalde identity provider komple ayrı bir proje oluyor
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])),
                    // eğer kullanıcı ile sunucu arasında zaman dilimi farkı varsa eşitle
                    ClockSkew = TimeSpan.Zero // bu bitince yeni token gerekir
                };
            });
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            // uygulamaya contexti göster ve inmemory olarak BookStoreDB database'i oluştur
            services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
            // interfaceli olan bookstore contexti her requestte çağırılması için servislere ekle
            services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
            // automapper ekle
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // service'ı register et, her seferinde loga yazıcak ondan singleton
            // kalıtım aldığı sınıf ile çağırılacak sınıf
            // _Ilogger hangisini çalıştırsın istiyorsan onu yaz 
            services.AddSingleton<ILoggerService, ConsoleLogger>();
            // alt alta aynı şeyi eklersen ikinci çalışır
            //services.AddSingleton<ILoggerService, DBLogger>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            //Buraya yetkilendirme ekle
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomExceptionMiddle();

            // endpointler çalışmadan önce middleware koy
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
