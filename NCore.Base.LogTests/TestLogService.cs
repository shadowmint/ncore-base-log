using NCore.Base.Log;
using NLog;
using Xunit;

namespace NCore.Base.LogTests
{
  public class TestLogService
  {
    private class TestConfigBuilder : DefaultConfigBuilder
    {
      public override bool EnableFileLogging => false;
    }
    
    [Fact]
    public void TestLoggingOverride()
    {
      var logging = new LogService();
      logging.ConfigureLogging<TestConfigBuilder>();

      var logger = LogManager.GetCurrentClassLogger();
      logger.Debug("test");
    }
  }
}