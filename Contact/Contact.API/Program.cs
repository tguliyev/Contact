using System.Text;
using Contact.Application;
using Contact.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
//14Ex8hL$Nzh4
var builder = WebApplication.CreateBuilder(args);

//Add api controller logic
builder.Services.AddControllers();

//add dependency injection of application layer
builder.Services.AddApplication();

//add dependency injection of infrastructure layer
builder.Services.AddInfrastructure(builder.Configuration);

//disable api default model validation errors
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//add jwt authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
        ValidAudience = builder.Configuration["JWTSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

//adding authorization
builder.Services.AddAuthorization();



//adding swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Contact API",
        Version = "v1",
        Description = "",
        Contact = new OpenApiContact
        {
            Name = "Tural Guiyev",
            Email = "tguliev9@gmail.com",
            Url = new Uri("https://mvc.wo-lo.com/"),
        },
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "ADD jwt token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

});



builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//give access to wwwroot
app.UseStaticFiles();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact API V1");
    c.RoutePrefix = string.Empty;
    c.DefaultModelsExpandDepth(-1);
});

app.UseAuthentication();

app.UseRouting();

app.MapControllers();

app.UseAuthorization();


app.Run();

//in order to use Program class in Integration test add partial Program class
public partial class Program { }