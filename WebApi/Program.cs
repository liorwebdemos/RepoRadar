using WebApi.BL.Contracts;
using WebApi.BL.Implementations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string allowAllCorsPolicy = "_allowAllCorsPolicy";
builder.Services.AddCors(
	options => options.AddPolicy(
		name: allowAllCorsPolicy,
		builder => builder
			.AllowAnyOrigin() // .WithOrigins("http://localhost:1234")
			.AllowAnyMethod()
			.AllowAnyHeader())
);

#region Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(options => { }); // Log Http Requests

//builder.Services.AddScoped<IReposBL, ReposBL>();
builder.Services.AddScoped<IReposBL, ReposMockBL>();
#endregion Services

WebApplication app = builder.Build();

#region Http Pipeline Configuration
if (app.Environment.IsDevelopment())
{
	app.UseHttpLogging();
	app.UseDeveloperExceptionPage();
	app.UseCors(allowAllCorsPolicy);
	app.UseSwagger();
	app.UseSwaggerUI();
}

#region Security
app.UseHttpsRedirection();
app.UseAuthorization();
#endregion Security

app.MapControllers();
#endregion Http Pipeline Configuration

app.Run();
