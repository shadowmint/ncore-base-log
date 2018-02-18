using System;
using System.IO;
using System.Reflection;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NCore.Base.Log
{
  /// <summary>
  /// DefaultConfigBuilder implements a basic configuration of IConfigBuilder.
  /// You can simply override it to provide a custom log folder, or implement
  /// IConfigBuilder manually.
  /// </summary>
  public class DefaultConfigBuilder : IConfigBuilder
  {
    protected const string DefaultLogFolder = "log";

    public virtual string ApplicationLogFolder => Path.Combine(ApplicationFolder, DefaultLogFolder);

    public virtual bool EnableDebugLogging => true;

    public virtual bool EnableConsoleLogging => true;

    public virtual bool EnableFileLogging => true;

    public virtual LogLevel FileLogLevel => LogLevel.Debug;

    public virtual LogLevel DebugLogLevel => LogLevel.Trace;

    public virtual LogLevel ConsoleLogLevel => LogLevel.Info;

    protected string ApplicationFolder
    {
      get
      {
        var codeBase = Assembly.GetEntryAssembly().CodeBase;
        var uri = new UriBuilder(codeBase);
        var path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path);
      }
    }

    public LoggingConfiguration Build()
    {
      var configuration = new LoggingConfiguration();
      ConfigureFileLogging(configuration);
      ConfigureConsoleLogging(configuration);
      ConfigureDebugLogging(configuration);
      return configuration;
    }

    private void ConfigureFileLogging(LoggingConfiguration configuration)
    {
      if (!EnableFileLogging) return;

      var rootPath = ApplicationLogFolder;
      var harvesterTarget = new FileTarget
      {
        Name = "CoreBaseLogFileLogger",
        FileName = Path.Combine(rootPath, "${shortdate}.log"),
        Layout = "${longdate} ${uppercase:${level}} ${message}",
        ArchiveFileName = Path.Combine(rootPath, "${shortdate}.{#}.log"),
        ArchiveNumbering = ArchiveNumberingMode.Rolling,
        MaxArchiveFiles = 7,
        ConcurrentWrites = true,
        ArchiveEvery = FileArchivePeriod.Day
      };

      configuration.AddTarget(harvesterTarget);
      var harvesterRule = new LoggingRule("*", FileLogLevel, harvesterTarget);
      configuration.LoggingRules.Add(harvesterRule);
    }

    /// <summary>
    /// This is only for attached debugging tools
    /// </summary>
    private void ConfigureDebugLogging(LoggingConfiguration configuration)
    {
      if (!EnableDebugLogging) return;
      var debugTarget = new DebugTarget("CoreBaseLogDebugLogger");
      configuration.AddTarget(debugTarget);
      configuration.LoggingRules.Add(new LoggingRule("*", DebugLogLevel, debugTarget));
    }

    /// <summary>
    /// Just dump everything to the console, even in release builds
    /// </summary>
    private void ConfigureConsoleLogging(LoggingConfiguration configuration)
    {
      if (!EnableConsoleLogging) return;
      var consoleTarget = new ConsoleTarget("CoreBaseLogConsoleLogger");
      configuration.AddTarget(consoleTarget);
      configuration.LoggingRules.Add(new LoggingRule("*", ConsoleLogLevel, consoleTarget));
    }
  }
}