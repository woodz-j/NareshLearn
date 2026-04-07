using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using NareshLearn.Application.Auth;
using NareshLearn.Application.Auth.Login;
using NareshLearn.Application.Auth.Register;
using NareshLearn.Application.Users;
using NareshLearn.Application.Courses;
using NareshLearn.Application.Courses.Create;
using NareshLearn.Application.Courses.List;
using NareshLearn.Infrastructure.Auth;
using NareshLearn.Infrastructure.Data;
using NareshLearn.Infrastructure.Users;
using NareshLearn.Infrastructure.Courses;
using System.Text;
using System.Collections.Generic;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Swagger (API documentation)
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Paste the JWT token only (no quotes, no 'Bearer ')."
    });
    
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = new List<string>()
    });
    /*
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearer"
                }
            },
            new List<string>()
        }
    });*/
});
// Application services
builder.Services.AddScoped<RegisterUserService>();

// TEMP Infrastructure (replace with EF Core later)
//builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, DevPasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

//builder.Services.AddSingleton<IPasswordHasher, DevPasswordHasher>();

builder.Services.AddScoped<RegisterUserService>();
builder.Services.AddScoped<LoginUserService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<CreateCourseService>();
builder.Services.AddScoped<ListCoursesService>();

// JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// Auth
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure middleware pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AngularFrontend");

app.Use(async (context, next) =>
{
    var auth = context.Request.Headers.Authorization.ToString();
    Console.WriteLine($"[DEBUG] Authorization header: {auth}");
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

// Enables controller endpoints
app.MapControllers();

app.Run();
