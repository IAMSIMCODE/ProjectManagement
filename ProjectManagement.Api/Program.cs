using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectManagement.Api.Config.GlobalError;
using ProjectManagement.Api.Config.Swagger;
using ProjectManagement.Domain.IRepository;
using ProjectManagement.Domain.Services;
using ProjectManagement.Domain.Services.Interfaces;
using ProjectManagement.Domain.Utility;
using ProjectManagement.Infrastructure.Data;
using ProjectManagement.Infrastructure.Repository;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IDeveloperService, DeveloperService>();
builder.Services.AddScoped<IAchievementService, AchievementService>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddHttpClient();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Swagger Versioning Configuration 
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(option =>
{
    option.OperationFilter<SwaggerDefaultValues>();
});

//Configure Versioning 
builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = ApiVersion.Default; // OR new ApiVersion(1, 0); Either options work 
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("api-version"),
        new UrlSegmentApiVersionReader());
}).AddApiExplorer(option =>
{
    option.GroupNameFormat = "'v'V";
    option.SubstituteApiVersionInUrl = true;
});

//Initialise Global Error
builder.Services.AddExceptionHandler<ErrorHandler>();
builder.Services.AddProblemDetails();

//Configure Serilog
//builder.Host.UseSerilog((context, configuration) =>
//    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        var descriptions = app.DescribeApiVersions();

        //Build a Swagger enpoint for each discovered Api Version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            opt.SwaggerEndpoint(url, name);
        }
    });
}

//app.UseSerilogRequestLogging();   

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
