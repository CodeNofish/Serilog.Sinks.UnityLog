using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.UnityLog {

  /// <summary>
  /// 业务模块名增强
  /// </summary>
  public sealed class FeaturesEnricher : ILogEventEnricher {
    private readonly LogEventProperty? _property;

    public FeaturesEnricher(string[] features) {
      if (features.Length != 0) {
        var sequenceValue = new SequenceValue(features.Select(feature => new ScalarValue(feature)));
        _property = new LogEventProperty(Const.FeatureKey, sequenceValue);
      }
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) {
      if (_property != null) {
        logEvent.AddPropertyIfAbsent(_property);
      }
    }
  }

}
