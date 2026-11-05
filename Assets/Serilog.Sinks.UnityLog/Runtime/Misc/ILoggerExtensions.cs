using Serilog.Sinks.UnityLog;

namespace Serilog {

  public static class ILoggerExtensions {
    public static ILogger ForContext(this ILogger logger, UnityEngine.Object context) {
      return logger.ForContext(new UnityObjectEnricher(context));
    }

    public static ILogger WithTag(this ILogger logger, string tag) {
      return logger.ForContext(new UnityTagEnricher(tag));
    }

    public static ILogger WithCaller<T>(this ILogger logger) {
      return logger.ForContext<T>().ForContext(new UnityCallerEnricher(typeof(T)));
    }
  }

}
