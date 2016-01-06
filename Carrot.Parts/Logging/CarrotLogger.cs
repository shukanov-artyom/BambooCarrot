using System;
using System.ComponentModel.Composition;
using Carrot.Contracts;
using Serilog;

namespace BuildAlertTrayDemon.Parts.Logging
{
    [Export(typeof(IExport))]
    public class CarrotLogger : MarshalByRefObject, IExportLogger
    {
        private static readonly ILogger logger;

        private static readonly string machineName;

        private static readonly string SeqUrl = "http://tv-server-new:5341/";

        private static CarrotLogger instance;

        static CarrotLogger()
        {
            machineName = Environment.MachineName;
            var conf = new LoggerConfiguration();
            conf.WriteTo.Log4Net("carrot.logger");
            conf.WriteTo.Seq(SeqUrl);
            logger = conf.CreateLogger();
        }

        public void Info(string format, params string[] args)
        {
            logger.Information(MarkLogEntryMachine(format), machineName, args);
        }

        public void Warn(string format, params string[] args)
        {
            logger.Warning(MarkLogEntryMachine(format), machineName, args);
        }

        public void Error(string format, params string[] args)
        {
            logger.Error(MarkLogEntryMachine(format), machineName, args);
        }

        public void Error(Exception e, string format, params string[] args)
        {
            logger.Error(e, MarkLogEntryMachine(format), machineName, args);
        }

        private static string MarkLogEntryMachine(string format)
        {
            return String.Format("{0}: {1}",
                "{machine}", 
                format);
        }
    }
}
