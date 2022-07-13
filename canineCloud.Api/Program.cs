using canineCloud.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
  options.Authority = builder.Configuration["Auth0:Domain"];
  options.Audience = builder.Configuration["Auth0:Audience"];
});


builder.Services.AddAuthorization(options =>
{
  options.AddPolicy(
  "manipulate:dogs",
  policy => policy.Requirements.Add(
  new HasScopeRequirement("manipulate:dogs",
  builder.Configuration["Auth0:Domain"]))
  );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
