using QST.MicroERP.Data;
using QST.MicroERP.Models;
using QST.MicroERP.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using QST.Scheduling;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using System.Text;
using QST.MicroERP.WebAPI.Token;
using QST.MicroERP.Core.Entities.Security;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.DAL.IDAL;
using QST.MicroERP.DAL;
using QST.MicroERP.Service.IServices;
using QST.MicroERP.Service;

var builder = WebApplication.CreateBuilder (args);
builder.Services.AddControllers ();
builder.Services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator> ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

ConfigurationManager configuration = builder.Configuration;
//builder.Services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));
var serverVersion = new MySqlServerVersion (new Version (8, 0, 26)); // Get the value from SELECT VERSION()
builder.Services.AddDbContext<ApplicationDbContext> (c => c.UseMySql (configuration.GetConnectionString ("ConnStr"), serverVersion));

LogManager.LoadConfiguration (string.Concat (Directory.GetCurrentDirectory (), "/NLog.config"));
builder.Services.AddIdentity<User, IdentityRole> (
        options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@._-";
        }
        )
    .AddEntityFrameworkStores<ApplicationDbContext> ();

builder.Services.AddAuthentication (cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer (options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (configuration["Token:Key"])),
        ValidIssuer = configuration["Token:Issuer"],
        ValidAudience = configuration["Token:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = true,
    };
});

builder.Services.AddAuthorization (options =>
{
    options.AddPolicy ("ManagerOnly", policy =>
    {
        policy.RequireRole ("Manager");
    });

});

#region Quartz.NET Schedule

var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
builder.Services.AddSingleton(scheduler);
//services.AddHostedService<TOPX.Notifications.Sender.CustomQuartzHostedService>();
builder.Services.AddSingleton(scheduler);

builder.Services.AddSingleton<IJobFactory, CustomJobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<MainJob>();

builder.Services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(MainJob), "MainJob", "0/10 * * * * ?"));
builder.Services.AddHostedService<QuartzHostedService>();

#endregion
#region Register Services 

builder.Services.AddScoped<IBaseService<AppointmentDE>, AppointmentService> ();
builder.Services.AddScoped<IApptService, AppointmentService> ();
builder.Services.AddScoped<IBaseService<DoctorDE>, DoctorService> ();
builder.Services.AddScoped<IBaseService<PermissionDE>, PermissionsService> ();
builder.Services.AddScoped<IPermissionService, PermissionsService> ();
builder.Services.AddScoped<IBaseService<SMTPCredentialsDE>, SMTPCredentialsService> ();

#endregion
#region Register  DAL

builder.Services.AddScoped<IBaseDAL<AppointmentDE>, AppointmentDAL> ();
builder.Services.AddScoped<IApptDAL, AppointmentDAL> ();
builder.Services.AddScoped<IBaseDAL<DoctorDE>, DoctorDAL> ();
builder.Services.AddScoped<IBaseDAL<PermissionDE>, PermissionsDAL> ();
builder.Services.AddScoped<IBaseDAL<SMTPCredentialsDE>, SMTPCredentialsDAL> ();

#endregion
var app = builder.Build ();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment ())
//{
    app.UseSwagger ();
    app.UseSwaggerUI ();
//}
app.UseHsts ();
app.UseHttpsRedirection ();
app.UseDefaultFiles ();
app.UseStaticFiles ();
app.UseCors (x => x

          .AllowAnyOrigin ()
          .AllowAnyMethod ()
          .AllowAnyHeader ());
app.UseHttpsRedirection ();
app.UseAuthentication ();
app.UseAuthorization ();
app.MapControllers ();
app.Run ();
