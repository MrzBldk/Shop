using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Ordering.API;
using Ordering.API.Filters;
using Ordering.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<DomainExceptionFilter>();
    options.Filters.Add<ValidateModelAttribute>();
});
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
    options.Audience = "ordering_api";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OrderingScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "ordering_api");
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
                Scopes = new Dictionary<string, string> { { "ordering_api", "ordering_api" } }
            }
        }
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.OAuthClientId("ordering_api");
        setup.OAuthAppName("ordering_api");
        setup.OAuthUsePkce();
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
