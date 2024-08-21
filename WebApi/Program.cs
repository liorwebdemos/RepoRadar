using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Formatters;
using WebApi.BL.Contracts;
using WebApi.BL.Implementations;
using WebApi.DAL.Contracts;
using WebApi.DAL.Implementations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string allowAllCorsPolicy = "_allowAllCorsPolicy";
builder.Services.AddCors(
	options => options.AddPolicy(
		name: allowAllCorsPolicy,
		builder => builder
			.WithOrigins("http://localhost:4200", "https://localhost:4200") // .AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials())
);

#region Services
#region Security
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.ExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(builder.Configuration["Jwt:ExpiresInMinutes"]!));
		options.SlidingExpiration = true;
		// override the default redirection to login for unauthorized requests
		options.Events = new CookieAuthenticationEvents
		{
			OnRedirectToLogin = context =>
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				return Task.CompletedTask;
			}
		};
	});

builder.Services.AddAuthorization();
#endregion Security

#region DAL
builder.Services.AddScoped<IReposDal, ReposHttpDal>();
//builder.Services.AddScoped<IReposDal, ReposMockDal>();
#endregion DAL

#region BL
builder.Services.AddScoped<IAuthBL, AuthBL>();
builder.Services.AddScoped<IReposBL, ReposBL>();
#endregion BL

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options =>
{
	// useful defaults, source: https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-8.0#special-case-formatters-2
	options.OutputFormatters.RemoveType<StringOutputFormatter>(); // text/plain or text/html -> json
	options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>(); // 204 No Content -> response with null body
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(options => { }); // log http requests
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
app.UseAuthentication();
app.UseAuthorization();
#endregion Security

app.MapControllers();
#endregion Http Pipeline Configuration

app.Run();
