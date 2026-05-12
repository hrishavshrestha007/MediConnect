using System.Text;
using ClinicWeb.Data;
using ClinicWeb.Models.Config;
using ClinicWeb.Services;
using ClinicWeb.Services.AppointmentCategories;
using ClinicWeb.Services.Appointments;
using ClinicWeb.Services.Auth;
using ClinicWeb.Services.Clinics;
using ClinicWeb.Services.Doctors;
using ClinicWeb.Services.Patients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IClinicService, ClinicService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentCategoryServices, AppointmentCategoryServices>();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MediConnect API",
        Description = "An ASP.NET Core Web API for managing doctors, patients, and appointments in a clinic setting.",
        
    });
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Paste your JWT token here. Do not include the word 'Bearer'.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(openApiDocument =>
    {
        // Create a reference to the previously defined "bearer" scheme
        var bearerSchemeReference =
            new OpenApiSecuritySchemeReference("bearer", openApiDocument);

        // Create a container for security requirements
        var securityRequirement = new OpenApiSecurityRequirement();

        // JWT bearer auth does not use scopes here, so the list is empty
        securityRequirement[bearerSchemeReference] = new List<string>();

        return securityRequirement;
    });
});


//JWT
var jwtSettings = new JwtSetting();
builder.Configuration.Bind("JwtSettings", jwtSettings);

if (string.IsNullOrEmpty(jwtSettings.SecretKey))
    throw new InvalidOperationException("JWT SecretKey is not configured in appsettings.json");
    
builder.Services.AddSingleton(jwtSettings);

builder.Services
    .AddAuthentication(options =>
    {
        // Use JWT bearer tokens when trying to identify the user
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        // Use JWT bearer tokens when access is denied and a challenge is needed
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey)
            )
        };
    });

//Add DbContext with SQL Server provider
builder.Services.AddDbContext<ClinicDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
