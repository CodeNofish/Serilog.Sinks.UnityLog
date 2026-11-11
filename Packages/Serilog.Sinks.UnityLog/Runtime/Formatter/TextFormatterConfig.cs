using System;
using UnityEngine;

namespace Serilog.Sinks.UnityLog {

  [Serializable]
  public class TextFormatterConfig {
    public bool showCaller = true; 
    public bool showUnityTag = true; 
    public bool showLevel = true;
    public bool showFeatures = true;
    public bool showTimestamp = true;
    public bool enableColorized = true;

    public Color verboseColor = new(0.41568628f, 0.4509804f, 0.49019608f, 1f);
    public Color debugColor = new(0.3372549f, 0.6117647f, 0.8392157f, 1f);
    public Color infoColor = new(0.30588236f, 0.7882353f, 0.6901961f, 1f);
    public Color warningColor = new(0.84313726f, 0.7294118f, 0.49019608f, 1f);
    public Color errorColor = new(0.95686275f, 0.2784314f, 0.2784314f, 1f);
    public Color fatalColor = new(1f, 0.41960785f, 0.41960785f, 1f);

    public Color featuresColor = new(0.70980394f, 0.80784315f, 0.65882355f, 1f);
    public Color timestampColor = new(0.5019608f, 0.5019608f, 0.5019608f, 1f);
    public Color callerColor = new(0.6117647f, 0.8627451f, 0.99607843f, 1f);
    public Color unityTagColor = new(0.6117647f, 0.8627451f, 0.99607843f, 1f);
    public Color messageColor = new(0.83137256f, 0.83137256f, 0.83137256f, 1f);
    public Color exceptionColor = new(0.80784315f, 0.5686275f, 0.47058824f, 1f);
  }

}
