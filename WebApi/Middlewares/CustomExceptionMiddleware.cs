using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
         private readonly RequestDelegate _next;
         // kullanıcak fonksiyon için interface ekledik
         private readonly ILoggerService _loggerService; // dependency
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService) // dependency injection
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew(); // işlem süresini tutmak için
            // API işlem yaparken middleware araya girip hata varsa yakalıycak
            // yoksa normal şeyleri konsola basacak
            try
            {
                //request kısmı
                // request hangi metodla hangi endpointten çağırıldı onu yazıyor
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message); // console.writeları ilogger servisle değiş

                // bir sonraki middleware çağırılıyor
                await _next(context);

                // süreyi durdur
                watch.Stop();
                // işlemler bittikten sonra yani response kısmı
                message = "[Response] HTTP " + 
                            context.Request.Method + " - " + 
                            context.Request.Path + "responded " + 
                            context.Response.StatusCode + " in " + 
                            watch.Elapsed.TotalMilliseconds + "ms";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {  
                // hata alırsada süreyi durdur
                watch.Stop();
                // try catch çok büyüdü diye bu kısmı ayırdık
                await HandleException(context, ex, watch);
            }

        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            // response kodunu 500 yap diyorsun
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "[Error] HTTP " + 
                             context.Request.Method + " - " + 
                             context.Response.StatusCode + " Error Message " +
                             ex.Message + " in " + watch.Elapsed.TotalMilliseconds + "ms";
            _loggerService.Write(message);
            // json döncek diyorsun
            context.Response.ContentType = "application/json";
            // herşeyi jsona çevir -> dotnet add package Newtonsoft.Json
            // validationdaki hatayı da error içine koy
            var result = JsonConvert.SerializeObject(new {error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }

}