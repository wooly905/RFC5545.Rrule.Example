using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RruleTool.Abstractions;
using RruleTool.Cache;
using RruleTool.Repository;
using RruleTool.Rules;

namespace RruleTool
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            SetupDIObjects(services);
        }

        private void SetupDIObjects(IServiceCollection services)
        {
            services.AddScoped<IRruleService, RruleService>();
            services.AddSingleton<IDataAccessRepository, DataAccessRepository>();
            services.AddSingleton<ISimpleDataCache, SimpleDataCache>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
