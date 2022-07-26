using System;
using Serilog.Core;
using Serilog.Events;
using Serilog.Configuration;
using Crestron.SimplSharp;

namespace Serilog.Sinks.Crestron
{
    
    /// <summary>
    /// A serilog Sink for Crestron Processors. Logs are outputted onto the processor's console.
    /// </summary>
    public class CrestronLogSink : ILogEventSink
    {
        //reference https://github.com/serilog/serilog/wiki/Developing-a-sink
        IFormatProvider FormatProvider { get; set; }

        public void Emit(LogEvent logEvent)
        {
            string msg = logEvent.RenderMessage(FormatProvider);
            switch(logEvent.Level)
            {
                case LogEventLevel.Verbose:                
                case LogEventLevel.Debug:
                    break;
                case LogEventLevel.Information:
                    ErrorLog.Notice(msg);
                    break;
                case LogEventLevel.Warning:
                    ErrorLog.Warn(msg); 
                    break;
                case LogEventLevel.Error:
                case LogEventLevel.Fatal:
                    ErrorLog.Error(msg);
                    break;
            }                                   
        }

        public CrestronLogSink(IFormatProvider formatProvider)
        {
            FormatProvider = formatProvider;
        }
    }

    public static class CrestronConsoleSinkExtensions
    {
        public static LoggerConfiguration CrestronConsoleSink(
              this LoggerSinkConfiguration loggerConfiguration,
              IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new CrestronConsoleSink(formatProvider));
        }
    }
}
