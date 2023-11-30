using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(opts =>
opts.ResolveConflictingActions(apiDec => apiDec.First())
);

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection"))
);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
				app.UseDeveloperExceptionPage();
			else
				app.UseExceptionHandler("/error");


			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();

			// Minimal API
			app.MapGet("/error/test", () => { throw new Exception("test"); });
		}
	}
}