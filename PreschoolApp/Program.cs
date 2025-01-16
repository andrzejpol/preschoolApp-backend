using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PreschoolApp.Configuration;
using PreschoolApp.Data;
using PreschoolApp.Services;
using PreschoolApp.Services.Interfaces;
using PreschoolApp.Tools;
using PreschoolApp.Tools.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Tools
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

//Configuration
AuthenticationConfiguration authenticationConfiguration = new();
builder.Configuration.GetSection(nameof(AuthenticationConfiguration)).Bind(authenticationConfiguration);
builder.Services.AddSingleton(authenticationConfiguration);

//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGroupService, GroupService>();


builder.Services.AddControllers();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationConfiguration.JwtIssuer,
        ValidAudience = authenticationConfiguration.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.JwtKey))
    };
});

builder.Services.AddDbContext<PreschoolDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.WithOrigins(allowedOrigin)
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

app.UseCors("myAppCors");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();