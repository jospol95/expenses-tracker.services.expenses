using System;
using System.Linq;
using Expenses.Contracts.HealthChecks;
using Expenses.Domain;
using Expenses.Domain.Repositories;
using Expenses.Infrastructure;
using Expenses.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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

            services.AddCustomHealthChecks(Configuration);
            
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins(Environment.GetEnvironmentVariable("SPA_APP_URL"))
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            
            services.AddDbContext<ExpensesDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("budget_db"),
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
            
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BudgetAPI"); });
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();

            }
            else
            {
                app.UseReverseProxyHttpsEnforcer();
            }

            //todo: stract with own extension method
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckResponse
                    {
                        Status = report.Status.ToString(),
                        HealthChecks = report.Entries.Select(x => new HealthCheck()
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.ToString()
                        }),
                        Duration = report.TotalDuration
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            });
            
            app.UseRouting();
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
