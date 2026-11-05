using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.UnityLog {

  public sealed class UnityObjectEnricher : ILogEventEnricher {
    public const string UnityContextKey = "%UnityContextKey%";

    private readonly LogEventProperty _property;

    public UnityObjectEnricher(UnityEngine.Object context) {
      _property = new LogEventProperty(UnityContextKey, new ScalarValue(context));
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) {
      logEvent.AddPropertyIfAbsent(_property);
    }
  }

}
