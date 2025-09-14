using CleanArchitecture.System.Application.Common.Interfaces;
using CleanArchitecture.System.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();
builder.AddInfrastructureServices<IApplicationDbContext, ApplicationDbContext>();
builder.AddApiServices<ApplicationDbContext>();
builder.Services.AddScoped<ApplicationDbContextInitialiser>();

var app = builder.Build();
app.InitialiseDatabase<ApplicationDbContext>();
app.UseApiServices();  // đây expose Swagger UI + JSON
app.Run();
