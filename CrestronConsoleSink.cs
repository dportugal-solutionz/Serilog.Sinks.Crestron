using System;
using Serilog.Core;
using Serilog.Events;
using Serilog.Configuration;
using Crestron.SimplSharp;

namespace Serilog.Sinks
{
    /// <summary>
    /// A serilog Sink for Crestron Processors. Logs are outputted onto the processor's console.
    /// </summary>
    public class CrestronConsoleSink : ILogEventSink
    {
        //reference https://github.com/serilog/serilog/wiki/Developing-a-sink
        IFormatProvider FormatProvider { get; set; }

        public void Emit(LogEvent logEvent)
        {
            string msg = logEvent.RenderMessage(FormatProvider);
            CrestronConsole.PrintLine(msg);
        }        

        public CrestronConsoleSink(IFormatProvider formatProvider)
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
