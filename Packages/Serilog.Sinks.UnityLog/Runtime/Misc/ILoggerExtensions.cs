using Serilog.Sinks.UnityLog;

namespace Serilog {

  public static class ILoggerExtensions {
    public static ILogger ForContext(this ILogger logger, UnityEngine.Object context) {
      return logger.ForContext(new UnityObjectEnricher(context));
    }

    public static ILogger WithTag(this ILogger logger, string tag) {
      return logger.ForContext(new UnityTagEnricher(tag));
    }

    public static ILogger WithFeatures(this ILogger logger, params string[] features) {
      return logger.ForContext(new FeatureNameEnricher(features));
    }

    public static ILogger WithCaller(this ILogger logger, string caller) {
      return logger.ForContext(new CallerEnricher(caller));
    }
  }

}
