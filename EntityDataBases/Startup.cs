using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityDataBases.Managers.CarsModels;
using EntityDataBases.Managers.CategorysParts;
using EntityDataBases.Managers.Citys;
using EntityDataBases.Managers.Manufacturers;
using EntityDataBases.Managers.Orders;
using EntityDataBases.Managers.Parts;
using EntityDataBases.Managers.Storages;
using EntityDataBases.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EntityDataBases
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _configurationRoot = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).AddJsonFile("AutoPartsStoreDbSettings.json").Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AutoPartsStoreDataContext>(options => options.UseSqlServer(_configurationRoot.GetConnectionString("AutoPartsStoreDb")));
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddTransient<ICarModelManager, CarModelManager>();
            services.AddTransient<ICategoryPartsManager, CategoryPartsManager>();
            services.AddTransient<IManufacturerManager, ManufacturerManager>();
            services.AddTransient<ICityManager, CityManager>();
            services.AddTransient<IStorageManager, StorageManager>();
            services.AddTransient<IPartManager, PartManager>();
            services.AddTransient<IOrderManager, OrderManager>();
        }


        //This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=MainPage}/{action=Main}/{id?}");
            });
        }
    }
}
