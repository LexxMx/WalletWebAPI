using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using WalletWebAPI.Repositories;

namespace WalletWebAPI {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services) {
			var storageType = Configuration["STORAGE_TYPE"];

			if (storageType == "file") {
				services.AddScoped<ITransactionRepository, TransactionFileRepository>();
			} else {
				var connection = Configuration.GetConnectionString("DefaultConnection");
				services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection).EnableSensitiveDataLogging());
				services.AddScoped<ITransactionRepository, TransactionDBRepository>();
				services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
			}

			services.AddSwaggerGen(x => {
				x.SwaggerDoc("v1", new OpenApiInfo { 
					Title = "Wallet web API" , 
					Version = "v1",
					Description = "API to perform transaction operations"
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				x.IncludeXmlComments(xmlPath);
			});
			services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
			);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();

			app.UseSwaggerUI(x => {
				x.SwaggerEndpoint("/swagger/v1/swagger.json", "Wallet API V1");
			});

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
