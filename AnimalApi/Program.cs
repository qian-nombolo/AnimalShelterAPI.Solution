using AnimalApi.Models;
using Microsoft.EntityFrameworkCore;
// for version
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
// for auththorization
using AnimalApiCall.Keys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


// for cors
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add cors
builder.Services.AddCors(options =>
  {
    options.AddPolicy(name: MyAllowSpecificOrigins,
      policy  =>
      {
        policy.WithOrigins("http://localhost:5000/api/animals", "http://localhost:5001/api/animals")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("Custom-Header");
      });
  });

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AnimalApiContext>(
                  dbContextOptions => dbContextOptions
                    .UseMySql(
                      builder.Configuration["ConnectionStrings:DefaultConnection"], 
                      ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
                    )
                  )
                );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add versionining
builder.Services.AddApiVersioning(opt =>
  {
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("x-api-version"), new MediaTypeApiVersionReader("x-api-version"));

  });

builder.Services.AddVersionedApiExplorer(setup =>
{
  setup.GroupNameFormat = "'v'VVV";
  setup.SubstituteApiVersionInUrl = true;
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

// Add Authentication services for JWT tokens 
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidAudience = "http://localhost:5000/api/animals",
      ValidIssuer = "http://localhost:5000/api/animals",
      ClockSkew = TimeSpan.Zero,// It forces tokens to expire exactly at token expiration time instead of 5 minutes later
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.ApiKey))
    };
    
});

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
      foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
      {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
      }
    });
}
else 
{
  app.UseHttpsRedirection();
}

// for cors
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
// app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
