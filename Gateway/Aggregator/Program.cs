using Aggregator.Config;
using Aggregator.Filters;
using Aggregator.Infrastructure;
using Aggregator.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.Configure<UrlsConfig>(builder.Configuration.GetSection("Urls"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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
                Scopes = new Dictionary<string, string> { { "aggregator", "aggregator" }, { "ordering_api", "ordering_api" } }
            }
        }
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "http://skoruba-identityserver4-sts-identity";
    options.RequireHttpsMetadata = false;
    options.Audience = "aggregator";
});

builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient<IBasketService, BasketService>();

builder.Services.AddHttpClient<ICatalogService, CatalogService>();

builder.Services.AddHttpClient<IOrderApiClient, OrderApiClient>()
    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.OAuthClientId("aggregator");
        setup.OAuthAppName("aggregator");
        setup.OAuthUsePkce();
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
