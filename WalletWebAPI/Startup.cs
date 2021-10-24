using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
			}
			
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
