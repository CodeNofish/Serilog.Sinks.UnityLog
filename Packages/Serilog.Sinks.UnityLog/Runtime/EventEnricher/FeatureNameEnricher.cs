using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.UnityLog {

  /// <summary>
  /// 业务模块名增强
  /// </summary>
  public sealed class FeatureNameEnricher : ILogEventEnricher {
    private readonly LogEventProperty _property;

    public FeatureNameEnricher(string[] features) {
      var sequenceValue = new SequenceValue(features.Select(feature => new ScalarValue(feature)));
      _property = new LogEventProperty(Const.FeatureKey, sequenceValue);
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) {
      logEvent.AddPropertyIfAbsent(_property);
    }
  }

}
