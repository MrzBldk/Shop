using Identity.Admin.EntityFramework.Shared.DbContexts;
using Identity.STS.Identity.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.STS.Identity.Configuration.Test
{
    public class StartupTest : Startup
    {
        public StartupTest(IWebHostEnvironment environment, IConfiguration configuration) : base(environment, configuration)
        {
        }

        public override void RegisterDbContexts(IServiceCollection services)
        {
            services.RegisterDbContextsStaging<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, IdentityServerDataProtectionDbContext>();
        }
    }
}







