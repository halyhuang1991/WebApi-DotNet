using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CSharp
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
            services.AddMvc(options =>
            {
                options.Filters.Add<ActionFilter>();
            });
            services.AddCors(options =>
            options.AddPolicy("AllowSameDomain",
        builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials())
            );//设定所有跨域
            services.AddOptions();
            services.Configure<SettingOptions>(Configuration.GetSection("ConnectionStrings"));
            var hostname = "XPHP0004\\HALY";
            var password = "admin";
            var connString = $"Data Source={hostname};Initial Catalog=test;User ID=admin;Password={password};";
            
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(connString));
            //------------------------------------------
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/api/Account");
                o.AccessDeniedPath = new PathString("/Error/Forbidden");
            });//用户认证
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
            
        }
    }
   
    
}
