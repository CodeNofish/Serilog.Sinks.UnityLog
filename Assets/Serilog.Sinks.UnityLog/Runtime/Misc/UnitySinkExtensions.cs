using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.UnityLog {

  public static class UnitySinkExtensions {
    public static LoggerConfiguration UnityLog(
      this LoggerSinkConfiguration config,
      LogEventLevel minLevel,
      ITextFormatter formatter,
      LoggingLevelSwitch? levelSwitch = null,
      UnityEngine.ILogger? unityLogger = null) {

      unityLogger ??= UnityEngine.Debug.unityLogger;

      return config.Sink(new UnityLogEventSink(formatter, unityLogger), minLevel, levelSwitch);
    }
  }

}
