using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using challenge_aa.Models;
using challenge_aa.Controllers;

namespace challenge {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            services.AddSignalR();
            services.AddControllers();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var connection = @"Host=challenge01.c8c1uywxrgq4.us-east-2.rds.amazonaws.com; Database=postgres;Username=challenge01;Password=challenge01";
            services.AddDbContext<postgresContext>(options => options.UseNpgsql(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors("CorsPolicy");
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHub<MessageController>("/message");
            });
        }
    }
}
