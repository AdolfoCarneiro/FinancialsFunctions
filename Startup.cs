using FinancialsFunctions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FinancialsFunctions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddTransient<IEmailService,EmailService>();
    }
    //public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    //{
    //    var hostBuilderContext = builder.GetContext();
    //    //builder.ConfigurationBuilder.AddJsonFile(Path.Combine(hostBuilderContext.ApplicationRootPath, GetValueAppSettings()), optional: true, reloadOnChange: false);
    //    builder.ConfigurationBuilder.Build();
    //    _configuration = builder.ConfigurationBuilder.Build();
    //}

    //protected string GetValueAppSettings()
    //{
    //    return "appsettings.json";
    //}
}
