using Devi.ParkingService.DataAccess;
using Devi.ParkingService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Devi.ParkingService
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();

            app.UseSession();
            
            app.UseMvc(routes =>
            {
                // Before all your routes
                routes.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Booking", action = "Index" }
                );
            });

            app.UseMiddleware<StaticFileMiddleware>();

            SeedDatabase(app);
        }

        private void SeedDatabase(IApplicationBuilder app)
        {
            using (var context = app.ApplicationServices.GetRequiredService<ParkingDbContext>())
            {
                // perform database delete
                context.Database.EnsureDeleted();

                // start seeding...
                context.Database.EnsureCreated();
                context.Database.Migrate();

                context.Customers.Add(new Customer
                {
                    Name = "devi"
                });
                var location1 = new Location
                {
                    Name = "Location 1"
                };
                var location2 = new Location
                {
                    Name = "Location 2"
                };
                location1.Areas.Add(new Area
                {
                    Name = "Area 1",
                    Capacity = 2
                });
                location1.Areas.Add(new Area
                {
                    Name = "Area 1",
                    Capacity = 3
                });
                location2.Areas.Add(new Area
                {
                    Name = "Area 1",
                    Capacity = 2
                });
                location2.Areas.Add(new Area
                {
                    Name = "Area 1",
                    Capacity = 2
                });
                context.Locations.Add(location1);
                context.Locations.Add(location2);

                context.SaveChanges();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            var connection = @"Filename=./parking.db";
            services.AddDbContext<ParkingDbContext>(options => options.UseSqlite(connection));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });
        }
    }
}
