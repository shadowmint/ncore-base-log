using NLog;
using NLog.Config;

namespace NCore.Base.Log
{
  public interface IConfigBuilder
  {
    string ApplicationLogFolder { get; }

    bool EnableFileLogging { get; }

    bool EnableDebugLogging { get; }

    bool EnableConsoleLogging { get; }

    LogLevel FileLogLevel { get; }

    LogLevel ConsoleLogLevel { get; }

    LogLevel DebugLogLevel { get; }

    LoggingConfiguration Build();
  }
}