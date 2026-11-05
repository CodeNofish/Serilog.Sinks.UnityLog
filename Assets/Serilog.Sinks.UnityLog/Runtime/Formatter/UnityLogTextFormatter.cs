using System.IO;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.UnityLog {

  public class UnityLogTextFormatter : ITextFormatter {
    private TextFormatterConfig Config { get; }
    private bool ShowLevel { get; }
    private bool ShowCaller { get; }
    private bool ShowTimestamp { get; }
    private bool Colorized { get; }
    private string VerboseColor { get; }
    private string DebugColor { get; }
    private string InfoColor { get; }
    private string WarningColor { get; }
    private string ErrorColor { get; }
    private string FatalColor { get; }
    private string TimeColor { get; }
    private string TagColor { get; }
    private string MessageColor { get; }
    private string ExceptionColor { get; }
    private string CallerColor { get; }

    public UnityLogTextFormatter(TextFormatterConfig config) {
      Config = config;
      ShowLevel = config.showLevel;
      ShowCaller = config.showCaller;
      ShowTimestamp = config.showTimestamp;
      Colorized = config.enableColorized;
      VerboseColor = config.verboseColor.ToHexRgba();
      DebugColor = config.debugColor.ToHexRgba();
      InfoColor = config.infoColor.ToHexRgba();
      WarningColor = config.warningColor.ToHexRgba();
      ErrorColor = config.errorColor.ToHexRgba();
      FatalColor = config.fatalColor.ToHexRgba();
      TimeColor = config.timestampColor.ToHexRgba();
      TagColor = config.tagColor.ToHexRgba();
      MessageColor = config.messageColor.ToHexRgba();
      ExceptionColor = config.exceptionColor.ToHexRgba();
      CallerColor = config.callerColor.ToHexRgba();
    }

    public void Format(LogEvent logEvent, TextWriter output) {
      if (ShowLevel) {
        var levelTag = logEvent.Level switch {
          LogEventLevel.Verbose => "[VRB]",
          LogEventLevel.Debug => "[DBG]",
          LogEventLevel.Information => "[INF]",
          LogEventLevel.Warning => "[WRN]",
          LogEventLevel.Error => "[ERR]",
          LogEventLevel.Fatal => "[FTL]",
          _ => "[???]"
        };

        if (Colorized) {
          levelTag = logEvent.Level switch {
            LogEventLevel.Verbose => WrapColor(VerboseColor, levelTag),
            LogEventLevel.Debug => WrapColor(DebugColor, levelTag),
            LogEventLevel.Information => WrapColor(InfoColor, levelTag),
            LogEventLevel.Warning => WrapColor(WarningColor, levelTag),
            LogEventLevel.Error => WrapColor(ErrorColor, levelTag),
            LogEventLevel.Fatal => WrapColor(FatalColor, levelTag),
            _ => "[???]"
          };
        }

        output.Write($"{levelTag} ");
      }

      if (ShowTimestamp) {
        var timestamp = $"[{logEvent.Timestamp:HH:mm:ss.fff}] ";
        if (Colorized) timestamp = WrapColor(TimeColor, timestamp);
        output.Write(timestamp);
      }

      if (ShowCaller) {
        if (logEvent.Properties.TryGetValue(Const.UnityCallerKey, out var callerValue) && callerValue is ScalarValue callerScalarValue && callerScalarValue.Value is string caller) {
          var callerTag = $"[{caller}] ";
          if (Colorized) callerTag = WrapColor(CallerColor, callerTag);
          output.Write(callerTag);
        }
      }

      if (logEvent.Properties.TryGetValue(Const.UnityTagKey, out var tagValue) && tagValue is ScalarValue sv && sv.Value is string tag) {
        var unityTag = $"[{tag}] ";
        if (Colorized) unityTag = WrapColor(TagColor, unityTag);
        output.Write(unityTag);
      }

      if (Colorized) output.Write($"<color=#{MessageColor}>");
      logEvent.RenderMessage(output);
      if (Colorized) output.Write("</color>");

      output.WriteLine();

      if (logEvent.Exception != null) {
        output.WriteLine();
        if (Colorized) output.Write($"<color=#{ExceptionColor}>");
        output.Write(logEvent.Exception);
        if (Colorized) output.Write("</color>");
      }
    }

    private static string WrapColor(string color, string text) {
      return $"<color=#{color}>{text}</color>";
    }
  }

}
