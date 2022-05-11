using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RezhDumaASPCore_Backend.Model;
using RezhDumaASPCore_Backend.Options;
using RezhDumaASPCore_Backend.Repositories;
using RezhDumaASPCore_Backend.Services;

namespace RezhDumaASPCore_Backend
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
            //services.AddEntityFrameworkSqlite().AddDbContext<UserContext>();
            services.AddDbContext<UserContext>(
                c => c.UseSqlite("Filename=DB\\rezhdb.db;Foreign Keys=False"),
                ServiceLifetime.Scoped);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddScoped<ApplicationRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<AnswerRepository>();
            services.AddScoped<DistrictRepository>();

            services.AddSwaggerGen();
            services.AddSignalR();

            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IEmailService, EmailService>();
            services.Configure<EmailSenderOptions>(options =>
            {
                options.HostAddress = Configuration["EmailConfiguration:SmtpServer"];
                options.HostPort = Convert.ToInt32(Configuration["EmailConfiguration:SmtpPort"]);
                options.HostUsername = Configuration["EmailConfiguration:SmtpUsername"];
                options.HostPassword = Configuration["EmailConfiguration:SmtpPassword"];
                options.SenderName = Configuration["EmailConfiguration:SenderName"];
            });

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RezhDuma");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
