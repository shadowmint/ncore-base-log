using System;
using NCore.Base.Log;
using NLog;

namespace NCore.Base.LogDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new LogService();
            service.ConfigureLogging();
            // service.EnableDebugging();

            var logger = LogManager.GetCurrentClassLogger();
            logger.Debug("Debug message!");
            logger.Info("Info!");
            logger.Error("Something was an error");

            Console.WriteLine($"Logged to: {service.LogFolder}");
        }
    }
}
