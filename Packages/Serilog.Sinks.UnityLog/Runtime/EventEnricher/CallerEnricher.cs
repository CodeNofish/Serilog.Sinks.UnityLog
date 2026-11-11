using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.UnityLog {

  public sealed class CallerEnricher : ILogEventEnricher {
    private readonly LogEventProperty _property;

    public CallerEnricher(string caller) {
      _property = new LogEventProperty(Const.CallerKey, new ScalarValue(caller));
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) {
      logEvent.AddPropertyIfAbsent(_property);
    }
  }

}
