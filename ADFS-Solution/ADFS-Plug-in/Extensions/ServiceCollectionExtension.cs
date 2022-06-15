using ADFS_Plug_in.Checkers;
using ADFS_Plug_in.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFS_Plug_in.Extensions
{
    internal static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMFACheck(this IServiceCollection collection)
        {
            collection.AddScoped<IChecker, ActiveDirectoryCheck>();
            collection.AddScoped<IChecker, DummyServiceCheck>();

            collection.AddLogging();

            Log.Logger = new LoggerConfiguration().MinimumLevel.Information().Enrich.FromLogContext().WriteTo.EventLog("MySource", "EventViewerArea").CreateLogger();

            return collection;
        }
    }
}
