using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Domain;
using Expenses.Domain.Models;
using Expenses.Domain.Repositories;
using Expenses.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Expenses.API
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
            services.AddControllers();
            
            services.AddDbContext<ExpensesDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("budget_dev"),
                    sqlServerOptionsAction: sqlOptions =>
                    { 
                        //Configuring Connection Resiliency:
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Expenses API",
                    Description = "Expenses API using Microservices Architecture",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Jose P",
                        Email = "jos.polanco@gmail.com",
                        Url = new Uri("https://twitter.com/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //todo verify if AddTransient should be used or instead another for IUnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddMediatR(typeof(Startup));

            
            // services.AddTransient<IRepository<Expense>, BaseRepository<Expense>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BudgetAPI"); });
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
