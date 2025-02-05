using System.Reflection;
using Abstractions;
using Core;

namespace Tracer.Serialization;

public class Tracing
{
    public void Serialize(TraceResult traceResult, Stream to)
    {
        var plugins = LoadPlugins();
        foreach (var plugin in plugins)
        {
            plugin.Serialize(traceResult, to);
        }
    }
    private IEnumerable<ITraceResultSerializer> LoadPlugins()
    {
        var pluginsDir = AppDomain.CurrentDomain.BaseDirectory;
        var plugins = new List<ITraceResultSerializer>();
        var pluginFiles = Directory.GetFiles(pluginsDir, "*.dll");
        foreach (var pluginFile in pluginFiles)
        {
            var assembly = Assembly.LoadFrom(pluginFile);
            var types = assembly.GetTypes()
                .Where(t => typeof(ITraceResultSerializer).IsAssignableFrom(t) && !t.IsAbstract);
            foreach (var type in types)
            {
                if (Activator.CreateInstance(type) is ITraceResultSerializer plugin)
                {
                    plugins.Add(plugin);
                }
            }
        }
        return plugins;
    }
}