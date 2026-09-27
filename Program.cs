#pragma warning disable ASP0000

using Avalonia;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ngnet_native.ViewModels;
using System;
using System.Linq;

namespace ngnet_native;

sealed class Program
{
	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args)
	{
        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        builder.Services.AddCors((setup) => setup.AddDefaultPolicy((policy) => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
        ));

        ServiceCollection services = new();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<ICommandService, CommandService>();

		Container.Services = services.BuildServiceProvider();

		WebApplication app = builder.Build();

        app.Lifetime.ApplicationStarted.Register(() =>
        {
            IServer server = app.Services.GetRequiredService<IServer>();
            IServerAddressesFeature? feature = server.Features.Get<IServerAddressesFeature>();

            if (feature is null) return;

            MainViewModel vm = Container.Services.GetRequiredService<MainViewModel>();

            vm.BackendUri = args.Contains("--ng-live-server")
                ? new("http://[::1]:4200")
                : new(feature.Addresses.First());
        });

        app.MapPost("set-title", ([FromBody] SetTitlePayload payload) =>
        {
            ICommandService commands = Container.Services.GetRequiredService<ICommandService>();

            commands.SetTitle(payload);
        });

        app.UseCors();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapFallbackToFile("index.html");
        app.RunAsync("http://[::1]:0");

		BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
	}

	// Avalonia configuration, don't remove; also used by visual designer.
	public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
