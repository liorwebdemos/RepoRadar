WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(options => { });
#endregion Services

string allowAllCorsPolicy = "_allowAllCorsPolicy";
builder.Services.AddCors(
	options => options.AddPolicy(
		name: allowAllCorsPolicy,
		builder => builder
			.AllowAnyOrigin() // .WithOrigins("http://localhost:1234")
			.AllowAnyMethod()
			.AllowAnyHeader())
);

WebApplication app = builder.Build();

#region Http Pipeline Configuration
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseCors(allowAllCorsPolicy);
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseHttpLogging();
}

#region Security
app.UseHttpsRedirection();
app.UseAuthorization();
#endregion Security

app.MapControllers();
#endregion Http Pipeline Configuration

app.Run();
