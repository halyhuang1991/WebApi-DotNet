using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Consul;
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

            String ip = "127.0.0.1";//Configuration["ip"];//部署到不同服务器的时候不能写成127.0.0.1或者0.0.0.0,因为这是让服务消费者调用的地址
            Int32 port = 5000;//Int32.Parse(Configuration["port"]);
            //向consul注册服务
            ConsulClient client = new ConsulClient(ConfigurationOverview);
            Task<WriteResult> result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "apiservice1" + Guid.NewGuid(),//服务编号，不能重复，用Guid最简单
                Name = "apiservice1",//服务的名字
                Address = ip,//我的ip地址(可以被其他应用访问的地址，本地测试可以用127.0.0.1，机房环境中一定要写自己的内网ip地址)
                Port = port,//我的端口
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务停止多久后反注册
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                    HTTP = $"http://{ip}:{port}/api/Account?usrname=halyhuang",//健康检查地址,
                    Timeout = TimeSpan.FromSeconds(5)
                }
            });
           
        }
        private static void ConfigurationOverview(ConsulClientConfiguration obj)
        {
            obj.Address = new Uri("http://127.0.0.1:8500");
            obj.Datacenter = "dc1";
        }
    }
   
    
}
