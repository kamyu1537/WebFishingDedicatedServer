using System.Reflection;
using WFDS.Common;
using WFDS.Common.Network;


namespace WFDS.Server.Core.Network;

internal static class PacketHandlerExtensions
{
    private static readonly ILogger Logger = Log.Factory.CreateLogger(nameof(PacketHandlerExtensions));
    
    public static IServiceCollection AddPacketHandlers(this IServiceCollection service)
    {
        var packetHandlerType = typeof(Common.Network.PacketHandler);
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetExportedTypes()).Distinct();
        var count = types
            .Where(t => t.IsClass && !t.IsAbstract && packetHandlerType.IsAssignableFrom(t))
            .Select(t => (t.GetCustomAttribute<PacketTypeAttribute>(), t))
            .Where(x => x is { Item1: not null, Item2: not null })
            .Select(x => x.Item2)
            .Select(service.AddTransient).ToArray().Length;
        
        Logger.LogInformation("added {PacketHandlerCount} packet handlers", count);
        return service;
    }
}