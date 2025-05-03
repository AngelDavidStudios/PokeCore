using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PokeCore.API.Api.Middlewares;
using PokeCore.API.Auth.Interfaces;
using PokeCore.API.Auth.Services;
using PokeCore.API.Core.Interfaces;
using PokeCore.API.Core.Services;
using PokeCore.API.Infrastructure.ExternalApis;
using PokeCore.API.Infrastructure.Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the JWT token in the format 'Bearer {token}'."
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
var bytes = Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JwtSecret"]!);
    
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(bytes),
        ValidAudience = builder.Configuration["Authentication:ValidAudience"],
        ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
    };
});
builder.Services.AddControllers();
builder.Services.AddCors();

// Services

// Repositories
builder.Services.AddScoped<IAuthService, SupabaseAuthService>();
builder.Services.AddScoped<IPokeApiService, PokeApiService>();
builder.Services.AddScoped<ComparadorPokemonService>();
builder.Services.AddScoped<SupabaseComparacionRepository>();
builder.Services.AddScoped<IValidacionService, ValidacionEquipoService>();
builder.Services.AddScoped<SupabaseValidacionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(static builder => 
    builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());
app.UseAuthentication();
app.UseMiddleware<SupabaseTokenMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();