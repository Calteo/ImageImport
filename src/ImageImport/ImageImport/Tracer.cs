using System.Diagnostics;

namespace ImageImport
{
    internal static class Tracer 
    {
        private static TraceSource Instance { get; } = new TraceSource(typeof(Tracer).Name)
        {
            Switch = new SourceSwitch("Protocol", "Main protocol of application") { Level = SourceLevels.Verbose }
        };
        public static TraceListenerCollection Listener => Instance.Listeners;
        public static SourceSwitch Switch => Instance.Switch;

        public static void TraceInformation(string text)
        {
            Instance.TraceInformation(text);
        }

        public static void TraceException(Exception exception, int id = 0)
        {
            Instance.TraceEvent(TraceEventType.Critical, id, exception.Message);
        }

        public static void TraceStart(string message, int id = 0)
        {
            Instance.TraceEvent(TraceEventType.Start, id, message);
        }
        public static void TraceStop(string message, int id = 0)
        {
            Instance.TraceEvent(TraceEventType.Stop, id, message);
        }

        public static void StartOperation(string name)
        {
            Trace.CorrelationManager.StartLogicalOperation(name);
        }

        public static void StopOperation()
        {
            Trace.CorrelationManager.StopLogicalOperation();
        }

        internal static void TraceVerbose(string message, int id = 0)
        {
            Instance.TraceEvent(TraceEventType.Verbose, id, message);
        }
    }
}
