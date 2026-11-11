using System.IO;
using System.Linq;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.UnityLog {

  public class UnityLogTextFormatter : ITextFormatter {
    private TextFormatterConfig Config { get; }
    private bool ShowCaller { get; }
    private bool ShowUnityTag { get; }
    private bool ShowLevel { get; }
    private bool ShowFeatures { get; }
    private bool ShowTimestamp { get; }
    private bool Colorized { get; }
    private string VerboseColor { get; }
    private string DebugColor { get; }
    private string InfoColor { get; }
    private string WarningColor { get; }
    private string ErrorColor { get; }
    private string FatalColor { get; }
    private string TimeColor { get; }
    private string UnityTagColor { get; }
    private string CallerColor { get; }
    private string MessageColor { get; }
    private string ExceptionColor { get; }
    private string FeaturesColor { get; }

    public UnityLogTextFormatter(TextFormatterConfig config) {
      Config = config;
      ShowCaller = config.showCaller;
      ShowUnityTag = config.showUnityTag;
      ShowLevel = config.showLevel;
      ShowFeatures = config.showFeatures;
      ShowTimestamp = config.showTimestamp;
      Colorized = config.enableColorized;
      VerboseColor = config.verboseColor.ToHexRgba();
      DebugColor = config.debugColor.ToHexRgba();
      InfoColor = config.infoColor.ToHexRgba();
      WarningColor = config.warningColor.ToHexRgba();
      ErrorColor = config.errorColor.ToHexRgba();
      FatalColor = config.fatalColor.ToHexRgba();
      TimeColor = config.timestampColor.ToHexRgba();
      UnityTagColor = config.unityTagColor.ToHexRgba();
      CallerColor = config.callerColor.ToHexRgba();
      MessageColor = config.messageColor.ToHexRgba();
      ExceptionColor = config.exceptionColor.ToHexRgba();
      FeaturesColor = config.featuresColor.ToHexRgba();
    }

    public void Format(LogEvent logEvent, TextWriter output) {
      if (ShowTimestamp) {
        var timestamp = $"[{logEvent.Timestamp:HH:mm:ss.fff}] ";
        if (Colorized) timestamp = WrapColor(TimeColor, timestamp);
        output.Write(timestamp);
      }

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

      if (ShowUnityTag) {
        if (logEvent.Properties.TryGetValue(Const.UnityTagKey, out var tagValue) && tagValue is ScalarValue { Value: string tag }) {
          var unityTagMsg = $"[{tag}] ";
          if (Colorized) unityTagMsg = WrapColor(UnityTagColor, unityTagMsg);
          output.Write(unityTagMsg);
        }
      }
      
      if (ShowFeatures) {
        if (logEvent.Properties.TryGetValue(Const.FeatureKey, out var fv) && fv is SequenceValue fsv && fsv.Elements.Count > 0) {
          var features = fsv.Elements
            .Select(e => e is ScalarValue { Value: string feature } ? feature : null)
            .Where(f => !string.IsNullOrEmpty(f))
            .ToList();

          var featuresTagMsg = $"[{string.Join(",", features)}]";
          if (Colorized) featuresTagMsg = WrapColor(FeaturesColor, featuresTagMsg);
          output.Write(featuresTagMsg);
        }
      }
      
      if (ShowCaller) {
        if (logEvent.Properties.TryGetValue(Const.CallerKey, out var callerValue) && callerValue is ScalarValue { Value: string caller }) {
          var callerMsg = $"[{caller}] ";
          if (Colorized) callerMsg = WrapColor(CallerColor, callerMsg);
          output.Write(callerMsg);
        }
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
