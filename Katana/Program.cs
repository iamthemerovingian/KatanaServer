using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katana
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8080";

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Server Started!!");
                Console.ReadKey();
                Console.WriteLine("Server Stopped :( ");

            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWelcomePage();
            //app.Run(ctx => 
            //{
            //    return ctx.Response.WriteAsync("Hello World");
            //});
        }
    }
}
