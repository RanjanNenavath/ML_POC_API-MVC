using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ML_Appointments.Services;
using ML_Appointments.Models;

namespace ML_Appointments
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
            services.AddHttpContextAccessor();

            //services.AddDbContext(options =>
            //{
            //    options.UseCosmos(Configuration["CosmosDbSettings:EndPoint"].ToString(),
            //      Configuration["CosmosDbSettings:AccountKey"].ToString(),
            //       Configuration["CosmosDbSettings:DatabaseName"].ToString());
            //});

            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddSingleton<IMtoServices>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDbSettings")).GetAwaiter().GetResult());
            services.AddDbContext<ML_MasterDBContext>(c => c.UseSqlServer(Configuration.GetConnectionString("AzureConnection"), b => b.MigrationsAssembly(typeof(ML_MasterDBContext).Assembly.FullName)), ServiceLifetime.Scoped);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddTransient<IManagementServices, ManagementServices>();
            services.AddTransient<IQueueService, QueueService>();
            services.AddScoped<IDapperDbContext, DapperDbContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ML_Appointments", Version = "v1" });
              //  c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ML_Appointments v1"));
                app.UseExceptionHandler("/api/error");   //we use this for serilog
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MyPolicy");


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static async Task<MtoServices> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string accountEndpoint = configurationSection.GetSection("AccountEndPoint").Value;
            string key = configurationSection.GetSection("AccountKey").Value;
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(accountEndpoint, key);
            MtoServices slotService = new MtoServices(client, databaseName, containerName);
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            return slotService;
        }
    }
}
