using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using DeepL;
using Discord;
using Discord.WebSocket;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vanslate;
using Vanslate.Entities;
using Vanslate.Helpers;
using Vanslate.Services;
using Version = Vanslate.Version;

namespace Gommon
{
    public static partial class Extensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection coll) =>
            coll.AddBotServices()
                .AddSingleton<CancellationTokenSource>()
                .AddSingleton(new DeepLClient(Config.DeepL.FormattedToken, !Config.DeepL.UsePro))
                .AddSingleton(new HttpClient
                {
                    Timeout = 10.Seconds()
                })
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    AlwaysAcknowledgeInteractions = false,
                    LogLevel = Severity,
                    GatewayIntents = Intents,
                    AlwaysDownloadUsers = false,
                    ConnectionTimeout = 10000,
                    MessageCacheSize = 0
                }));

        private static LogSeverity Severity => Version.IsDevelopment ? LogSeverity.Debug : LogSeverity.Verbose;
        
        private static GatewayIntents Intents
            => GatewayIntents.Guilds;

        private static bool IsEligibleService(Type type) => type.Inherits<IVanslateService>() && !type.IsAbstract;

        public static IServiceCollection AddBotServices(this IServiceCollection serviceCollection)
            => serviceCollection.Apply(coll =>
            {
                //get all the classes that inherit IVanslateService, and aren't abstract.
                var l = typeof(Bot).Assembly.GetTypes()
                    .Where(IsEligibleService).Apply(ls => ls.ForEach(coll.TryAddSingleton));
                Logger.Info(LogSource.Bot, $"Injected {"service".ToQuantity(l.Count())} into the service provider.");
            });
    }
}