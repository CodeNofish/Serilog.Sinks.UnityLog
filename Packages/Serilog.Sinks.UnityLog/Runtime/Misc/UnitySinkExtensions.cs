using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.UnityLog {

  public static class UnitySinkExtensions {
    public static LoggerConfiguration UnityLog(
      this LoggerSinkConfiguration config,
      LogEventLevel minLevel,
      TextFormatterConfig formatterConfig,
      LoggingLevelSwitch? levelSwitch = null,
      UnityEngine.ILogger? unityLogger = null) {

      unityLogger ??= UnityEngine.Debug.unityLogger;
      var formatter = new UnityLogTextFormatter(formatterConfig);

      return config.Sink(new UnityLogEventSink(formatter, unityLogger), minLevel, levelSwitch);
    }
  }

}
