using Discord;
using System;

namespace Vanslate.Entities
{
    public sealed class LogEventArgs : EventArgs
    {
        public string Message { get; }
        public string Source { get; }
        public LogSeverity Severity { get; }
        public LogMessage LogMessage { get; }

        public LogEventArgs(global::Discord.LogMessage message)
        {
            Message = message.Message;
            Source = message.Source;
            Severity = message.Severity;
            LogMessage = LogMessage.FromDiscordLogMessage(message);
        }
    }
}