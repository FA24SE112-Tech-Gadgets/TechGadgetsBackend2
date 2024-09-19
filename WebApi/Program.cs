using FluentValidation;
using WebApi.Common.Endpoints;
using WebApi.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"Credentials/account_service.json");

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddHttpContextAccessor();

builder.Services.AddJsonOptions();
builder.Services.AddEndpoints();
builder.Services.AddCorsPolicy();
builder.Services.AddServices();
builder.Services.AddConfigureSettings(builder.Configuration);
builder.Services.AddDbContextConfiguration(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddThrowOnBadRequest();

var app = builder.Build();

app.UseCorsPolicy();
app.UseOpenApi();
app.UseTechGadgetsExceptionHandler();
app.ApplyMigrations();
app.UseExceptionPageInLocal();

app.MapEndpoints(app.MapGroup("api"));

app.Run();
