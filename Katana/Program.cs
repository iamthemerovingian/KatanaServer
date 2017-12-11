using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Katana
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        string uri = "http://localhost:8080";

    //        using (WebApp.Start<Startup>(uri))
    //        {
    //            Console.WriteLine("Server Started!!");
    //            Console.ReadKey();
    //            Console.WriteLine("Server Stopped :( ");

    //        }
    //    }
    //}

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (environment, next) =>
            {
                foreach (var keyValuePair in environment.Environment)
                {
                    Console.WriteLine($"{keyValuePair.Key}:{keyValuePair.Value}");
                }

                await next();
            });

            app.Use(async (environment, next) =>
            {
                Console.WriteLine($"Requesting: {environment.Request.Path}");

                await next();

                Console.WriteLine($"Status Code:{environment.Response.StatusCode}");
            });

            ConfigureWebApi(app);

            app.UseHelloWorld();
            //app.Use<HellowWorldComponent>();
            //app.UseWelcomePage();
            ////app.Run(ctx => 
            ////{
            ////    return ctx.Response.WriteAsync("Hello World");
            ////});
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var httpconfig = new HttpConfiguration();
            httpconfig.Routes.MapHttpRoute(
                "DefaultApi", 
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional});

            app.UseWebApi(httpconfig);
        }
    }

    public static class HelloWorldAppBuilderExtensions
    {
        public static void UseHelloWorld(this IAppBuilder appBuilder)
        {
            appBuilder.Use<HellowWorldComponent>();
        }
    }

    public class HellowWorldComponent
    {
        private readonly AppFunc TheNextMethodInThePipeline;

        public HellowWorldComponent(AppFunc nextMethodInThePipeline)
        {
            TheNextMethodInThePipeline = nextMethodInThePipeline;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            var response = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response))
            {
                return writer.WriteAsync("Hello!!");
            }
            //return TheNextMethodInThePipeline(environment);
        }
    }
}
