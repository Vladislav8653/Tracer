using Core;
using Tracer.Serialization;

namespace Example;
class Program
{
    private static void Main()
    {
        var tracer = new Core.Tracer();
        var bar = new Bar(tracer);
        tracer.StartTrace();
        Test1(300);
        bar.InnerMethod(200);
        tracer.StopTrace();
        var result = tracer.GetTraceResult();
        var serializers = PluginLoader.LoadPlugins();
        foreach (var serializer in serializers)
        {
            var fileName = Path.Combine("Tracers", $"tracer.{serializer.Format}");
            using var fileStream = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(result, fileStream);
        }
    }

    private static void Test1(int time)
    {
        Thread.Sleep(time);
    }
}

class Bar
{
    private ITracer _tracer;

    internal Bar(ITracer tracer)
    {
        _tracer = tracer;
    }
    
    public void InnerMethod(int time)
    {
        _tracer.StartTrace();
        Thread.Sleep(time);
        _tracer.StopTrace();
    } 
}
