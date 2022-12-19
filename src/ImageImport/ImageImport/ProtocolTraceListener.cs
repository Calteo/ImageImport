using System.Diagnostics;
using System.Text;

namespace ImageImport
{
    internal class ProtocolTraceListener : TraceListener
    {
        public ProtocolTraceListener(TextBox textBox)
        {
            TextBox = textBox;
            TraceOutputOptions = TraceOptions.DateTime|TraceOptions.LogicalOperationStack|TraceOptions.ThreadId;
        }

        public TextBox TextBox { get; }

        public override void TraceData(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, object? data)
        {
            base.TraceData(eventCache, source, eventType, id, data);
        }

        public override void TraceData(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, params object?[]? data)
        {
            base.TraceData(eventCache, source, eventType, id, data);
        }

        public override void TraceEvent(TraceEventCache? eventCache, string source, TraceEventType eventType, int id)
        {
            base.TraceEvent(eventCache, source, eventType, id);
        }

        public override void TraceEvent(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, string? format, params object?[]? args)
        {
            if (!Write(eventCache, source, eventType, id, format, args))
                base.TraceEvent(eventCache, source, eventType, id, format, args);
        }

        public override void TraceEvent(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, string? message)
        {
            if (!Write(eventCache, source, eventType, id, message))
                base.TraceEvent(eventCache, source, eventType, id, message);            
        }

        public override void Fail(string? message)
        {
            base.Fail(message);
        }

        public override void Fail(string? message, string? detailMessage)
        {
            base.Fail(message, detailMessage);
        }

        private bool Write(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, string? format, params object?[]? args)
        {
            if (eventCache != null)
            {
                var builder = new StringBuilder();

                if (TraceOutputOptions.HasFlag(TraceOptions.DateTime))
                    builder.Append($"[{eventCache.DateTime}] ");

                if (TraceOutputOptions.HasFlag(TraceOptions.ThreadId))
                    builder.Append($"T#{eventCache.ThreadId} ");

                if (TraceOutputOptions.HasFlag(TraceOptions.LogicalOperationStack))
                    builder.Append(string.Join("/", eventCache.LogicalOperationStack.ToArray().Reverse()));


                if (builder.Length > 0) builder.Append(": ");

                builder.Append($"{eventType}");
                if (format != null)
                {
                    builder.Append(" - ");
                    if (args != null)                    
                        builder.AppendFormat(format, args);
                    else
                        builder.Append(format);
                }

                WriteLine(builder.ToString());
            }
            return eventCache != null;
        }

        public override void Write(string? message)
        {
            Append(message);
        }

        public override void WriteLine(string? message)
        {       
            Append(message+Environment.NewLine);
        }

        private void Append(string? message)
        {
            if (message == null) return;

            if (TextBox.InvokeRequired)
                TextBox.BeginInvoke(() => Append(message));
            else
            {
                TextBox.AppendText(message);

                TextBox.SelectionLength = 0;
                TextBox.SelectionStart = TextBox.TextLength;
                TextBox.ScrollToCaret();
            }
        }
    }
}
