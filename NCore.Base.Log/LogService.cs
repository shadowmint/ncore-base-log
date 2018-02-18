using System;
using System.IO;
using NLog;
using NLog.Common;

namespace NCore.Base.Log
{
  /// <summary>
  /// LogService is a common high level API for interacting with NLog
  /// using a programatical api.
  /// </summary>
  public class LogService
  {
    public string LogFolder => _configSettings?.ApplicationLogFolder;

    private IConfigBuilder _configSettings;

    public void ConfigureLogging<T>() where T : IConfigBuilder
    {
      _configSettings = Activator.CreateInstance<T>();
      CreateLogFolder(_configSettings);
      LogManager.Configuration = _configSettings.Build();
    }

    public void ConfigureLogging()
    {
      ConfigureLogging<DefaultConfigBuilder>();
    }

    public void EnableDebugging()
    {
      InternalLogger.LogToConsole = true;
      InternalLogger.LogLevel = LogLevel.Trace;

      LogManager.ThrowConfigExceptions = true;
      LogManager.ThrowExceptions = true;
    }

    /// <summary>
    /// It may not actually be possible to create a log folder, and we don't want
    /// to explicitly fail if it's a system level path like /var/log
    /// </summary>
    private void CreateLogFolder(IConfigBuilder configSettings)
    {
      try
      {
        if (!Directory.Exists(configSettings.ApplicationLogFolder))
        {
          Directory.CreateDirectory(configSettings.ApplicationLogFolder);
        }
      }
      catch (IOException)
      {
      }
    }
  }
}