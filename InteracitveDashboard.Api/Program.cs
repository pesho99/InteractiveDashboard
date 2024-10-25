using FluentValidation;
using InteractiveDashboard.Api.Extensions;
using InteractiveDashboard.Api.Middleware;
using InteractiveDashboard.Application;
using InteractiveDashboard.Application.Behaviors;
using InteractiveDashboard.Application.Services;
using InteractiveDashboard.Application.Validators;
using InteractiveDashboard.Infrastructure;
using MediatR;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .SetIsOriginAllowed((host) => true);
    });
});


// Add services to the container.

builder.Services.RunInstallers(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
// Register FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(RegisterUserValidator).Assembly);

// Add ValidationBehavior to the MediatR pipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<TickerHub>("/tickerhub");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseMiddleware<ExceptionMidlleware>();
app.UseAuthorization();
app.UseAuthentication();
app.UseCors("CorsPolicy");

await app.Services.InitializeInfrastructureServices();

app.Run();
