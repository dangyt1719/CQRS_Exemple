using FluentValidation.AspNetCore;
using Infrastructure.Implementation;
using Infrastructure.Implementation.QuartsService.Jobs.CreateCandidate;
using Infrastructure.Implementation.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.QuartsServiceInterfaces;
using Infrastructure.Interfaces.RepositoryInterfaces;
using MediatR;
using NeocaseProviderLibrary;
using NLog.Web;
using Quartz;
using System.Net;
using UseCases.Candidates.Commands.CreateCandidateToNeocase;
using UseCases.Candidates.Utils;
using UseCases.PowerAttorneyRegistry.Queries.GetPowerAttorneyExcelReport;
using WebApi.Filters;
using WebApi.Settings;
using UseCases.SetHrRcPartner.Commands.SetHrRcPartner;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(p => p.Filters.Add<ApiExceptionFilterAttribute>()).AddFluentValidation();
builder.Services.AddFluentValidation(p => p.RegisterValidatorsFromAssemblyContaining<CreateCandidateToNeocaseCommandValidator>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var credentials = builder.Configuration.GetSection("Credentials").Get<Credentials>();
builder.Services.AddHttpClient<IHttpService, HttpService>(client =>
{
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.UserAgent.Clear();
    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    UseDefaultCredentials = true,
    Credentials = new NetworkCredential(credentials.Login, credentials.Password),
    AllowAutoRedirect = true
});

builder.Services.AddMediatR(typeof(CreateCandidateToNeocaseCommand));
builder.Services.AddMediatR(typeof(GetPowerAttorneyExcelReportQuery));
builder.Services.AddMediatR(typeof(SetHrRcPartnerCommand));
builder.Services.AddNeocaseProviders();

builder.Services.AddQuartz(q =>
{
    q.UsePersistentStore(s =>
    {
        s.UseProperties = false;
        s.RetryInterval = TimeSpan.FromSeconds(15);
        s.UseSqlServer(sql =>
        {
            sql.ConnectionString = builder.Configuration.GetConnectionString("QuartzServer");
            sql.TablePrefix = "QRTZ_";
        });
        s.UseJsonSerializer();
    });
    q.UseMicrosoftDependencyInjectionJobFactory();
    q.SchedulerName = "QuartzSchedulerService";
}).AddQuartzHostedService(q => q.WaitForJobsToComplete = false);

builder.Services.AddScoped<ICreateCandidateJob, CreateCandidateJob>();
builder.Services.AddScoped<ICreateCandidateOrbitJob, CreateCandidateOrbitJob>();
builder.Services.AddAutoMapper(typeof(CandidateProfile));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IHrDirectoryRepository, HrDirectoryRepository>();
builder.Services.AddScoped<IRegionCenterRepository, RegionCenterRepository>();
builder.Services.AddScoped<IOrgUnitRepository, OrgUnitCachedDbRepository>();
builder.Services.AddScoped<IPowerAttorneyRegistryRepository, PowerAttorneyRegistryRepository>();
builder.Services.AddScoped<ILegalDepartmentPriceRepository, LegalDepartmentPriceRepository>();
builder.Services.AddMemoryCache();

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

var CorsAllowedOrigins = "_allowNeocase";

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsAllowedOrigins,
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(CorsAllowedOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();