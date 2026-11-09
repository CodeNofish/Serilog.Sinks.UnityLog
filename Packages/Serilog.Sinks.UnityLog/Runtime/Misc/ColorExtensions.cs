using UnityEngine;

namespace Serilog.Sinks.UnityLog {

  internal static class ColorExtensions {
    /// <summary>
    /// 转为 8 位十六进制字符串：RRGGBBAA
    /// </summary>
    internal static string ToHexRgba(this Color color) {
      byte r = (byte)Mathf.RoundToInt(color.r * 255f);
      byte g = (byte)Mathf.RoundToInt(color.g * 255f);
      byte b = (byte)Mathf.RoundToInt(color.b * 255f);
      byte a = (byte)Mathf.RoundToInt(color.a * 255f);
      return $"{r:X2}{g:X2}{b:X2}{a:X2}";
    }
  }

}
