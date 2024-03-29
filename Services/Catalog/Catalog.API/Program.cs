using Autofac.Extensions.DependencyInjection;
using Catalog.API;
using Catalog.API.IntegrationEvents.EventHandling;
using Catalog.API.IntegrationEvents.Events;
using Catalog.API.Mappers;
using Catalog.BLL.Mappers;
using Catalog.BLL.Services;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Context;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories;
using Catalog.DAL.Repositories.Interfaces;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddIntegrationServicesAndEventBus(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.Authority = "http://skoruba-identityserver4-sts-identity";
    options.RequireHttpsMetadata = false;
    options.Audience = "catalog_api";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CatalogScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "catalog_api");
        policy.Build();
    });
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("http://localhost:9002/connect/authorize"),
                TokenUrl = new Uri("http://localhost:9002/connect/token"),
                Scopes = new Dictionary<string, string> { { "catalog_api", "catalog_api" } }
            }
        }
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});
builder.Services.AddAutoMapper(typeof(DtoToViewModelMappingProfile), typeof(ViewModelToDtoMappingProfile),
    typeof(DomainToDtoMappingProfile), typeof(DtoToDomainMappingProfile));
builder.Services.AddDbContext<CatalogContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddTransient<IGenericRepository<Entity>, GenericRepository<Entity>>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<ITypeService, TypeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.OAuthClientId("catalog_api");
        setup.OAuthAppName("catalog_api");
        setup.OAuthUsePkce();
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<OrderStockConfirmedIntegrationEvent, OrderStockConfirmedIntegrationEventHandler>();
eventBus.Subscribe<StoreBlockedIntegrationEvent, StoreBlockedIntegrationEventHandler>();
eventBus.Subscribe<StoreUnblockedIntegrationEvent, StoreUnblockedIntegrationEventHandler>();

app.Run();