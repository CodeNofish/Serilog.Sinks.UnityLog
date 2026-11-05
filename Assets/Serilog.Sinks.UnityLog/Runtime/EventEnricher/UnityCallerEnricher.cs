using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.UnityLog {

  public sealed class UnityCallerEnricher : ILogEventEnricher {
    public const string UnityCallerKey = Const.UnityCallerKey;

    private readonly LogEventProperty _property;

    public UnityCallerEnricher(Type type) {
      _property = new LogEventProperty(UnityCallerKey, new ScalarValue(type.FullName));
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) {
      logEvent.AddPropertyIfAbsent(_property);
    }
  }

}
