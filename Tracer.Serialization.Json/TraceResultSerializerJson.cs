using Core;

namespace Json;

public class TraceResultSerializerJson : ITraceResultSerializer
{
    public string Format { get; }
    public void Serialize(TraceResult traceResult, Stream to)
    {
        throw new NotImplementedException();
    }
}