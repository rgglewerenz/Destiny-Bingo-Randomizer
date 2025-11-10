using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Testing_with_electron.Interfaces;
using Testing_with_electron.Services;
using Destiny_Bingo_Randomizer.Data;
using Destiny_Bingo_Randomizer.Services;
using Discord;
using Destiny_Bingo_Randomizer.Interfaces;
using Discord.WebSocket;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = (await AddServices(builder, args)).Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");


        await app.StartAsync();


        app.WaitForShutdown();
    }


    private static async Task<WebApplicationBuilder> AddServices(WebApplicationBuilder builder, string[] args)
    {
        builder.WebHost.UseElectron(args);

        BrowserWindow window = null;

        if (HybridSupport.IsElectronActive)
        {
            var options = new BrowserWindowOptions()
            {
                DarkTheme = true,
                Frame = false,
                BackgroundColor = "#2d313a",
                Height = 1150,
                Width = 1050,
                WebPreferences = new WebPreferences()
                {
                    DevTools = true,
                    EnableRemoteModule = true
                }
            };

            window = await Electron.WindowManager.CreateWindowAsync(options);
            window.OnClosed += () =>
            {
                Electron.App.Quit();
            };



        }
        builder.Services.AddSingleton<IWindowManagerProvider, WindowManagerProvider>((services) =>
        {
            return new WindowManagerProvider(window);
        });
        builder.Services.AddSingleton<ICardItemGenerationService, CardItemGenerationService>();
        builder.Services.AddSingleton<IDiscordBotService, DiscordBotService>((services) =>
        {
            return new DiscordBotService(builder.Configuration);
        });


        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();

        return builder;
    }


    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}