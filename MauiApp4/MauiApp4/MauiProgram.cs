using CorrelationId.DependencyInjection;
using CorrelationId.HttpClient;
using MauiApp4.Services;
using MauiApp4.Shared;
using MauiApp4.Shared.Services;
using Microsoft.Extensions.Logging;

namespace MauiApp4
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the MauiApp4.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddCorrelationId();

            builder.Services.AddScoped<StorageService>();

            builder.Services.AddTransient<TokenHandler>();


            builder.Services.AddHttpClient<IAPIGateway, APIGateway>(httpclient =>
            {

                httpclient.BaseAddress = new Uri(AppSettings.APIGatewayBaseUrl);


            }).AddCorrelationIdForwarding().AddHttpMessageHandler<TokenHandler>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
