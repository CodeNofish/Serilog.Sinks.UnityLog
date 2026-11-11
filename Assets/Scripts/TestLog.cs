using Serilog;
using Serilog.Events;
using Serilog.Sinks.UnityLog;
using UnityEngine;

namespace App {
  public class TestMono : MonoBehaviour {
    private void Start() {
      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .WriteTo.UnityLog(LogEventLevel.Verbose, new TextFormatterConfig())
        .CreateLogger();

      Log.Information("Hello World");
    }
  }
}