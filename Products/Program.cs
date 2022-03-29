using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ONYXProducts.Application.AdaptersPorts.Persistence;
using ONYXProducts.Application.AdaptersPorts.WebApi;
using ONYXProducts.Application.UseCases;
using ONYXProducts.Application.UseCases.UserAuthentication;
using ONYXProducts.Catalog.Adapters.Persistence;
using ONYXProducts.Catalog.Application.Services;
using ONYXProducts.Domain.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepository<IProduct>, ProductsRepository>();
builder.Services.AddTransient<IProductsCatalog, ProductCatalog>();

//Set the encryption token key
var encryptionKey = "This encryption key should be saved in the config, or within a secrets";
//Register authenticator for inyection
builder.Services.AddSingleton<IJwtONYXAuthenticator>(new JwtONYXAuthenticator(encryptionKey));

//setup the authentication through jwtbearer.
builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // We will use the default scheme to double check the received token sign is authentic
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(b =>
                {
                    b.RequireHttpsMetadata = false;
                    b.SaveToken = true;
                    b.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(encryptionKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
                 );

builder.Services.AddHealthChecks().AddCheck<ProductHealthCheck>("MemoryCheck");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/healthCheck");
        endpoints.MapControllers();
    }
);

app.Run();

