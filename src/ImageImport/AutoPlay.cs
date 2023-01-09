using Microsoft.Win32;

namespace ImageImport
{
    /// <summary>
    /// Registers the auto play handler in the registry
    /// </summary>
    static class AutoPlay
    {
        public static bool IsRegistered()
        {
            var path = Environment.ProcessPath;

            using var autoplayKey = GetAutoplayKey(Registry.CurrentUser);
            using var handlersKey = GetKey(autoplayKey, HandlersKey);
            using var handlerKey = handlersKey.OpenSubKey(HandlerKey);

            if (handlerKey == null)
            {
                Tracer.TraceVerbose($@"Handler entry {handlersKey}\{HandlerKey} not found.");
                return false;
            }

            var initCmd = handlerKey.GetValue("InitCmdLine")?.ToString();
            if (path != initCmd?.Trim('"'))
            {
                Tracer.TraceVerbose($@"Handler cmd '{initCmd}'<>'{path}'.");
                return false;
            }
        
            return true;
        }

        public static void Register()
        {
            var path = Environment.ProcessPath;

            using var autoplayKey = GetAutoplayKey(Registry.CurrentUser, true);
            
            using var handlersKey = GetKey(autoplayKey, HandlersKey, true);
            using var handlerKey = handlersKey.CreateSubKey(HandlerKey, true);

            handlerKey.SetValue("Action", "Import Images", RegistryValueKind.ExpandString);
            handlerKey.SetValue("Provider", "Calteo Image Import", RegistryValueKind.ExpandString);
            handlerKey.SetValue("DefaultIcon", $"\"{path}\",0", RegistryValueKind.ExpandString);
            handlerKey.SetValue("InitCmdLine", $"\"{path}\"", RegistryValueKind.String);
            handlerKey.SetValue("InvokeProgID", ProgId, RegistryValueKind.ExpandString);
            handlerKey.SetValue("InvokeVerb", "open", RegistryValueKind.String);                

            using var showPicturesKey = GetKey(autoplayKey, ShowPicturesOnArrivalKey, true);

            showPicturesKey.SetValue(HandlerKey, "");            

            using var classesKey = GetClassesKey(Registry.CurrentUser, true);
            using var progIdKey = classesKey.CreateSubKey(ProgId, true);
            using var commandKey = progIdKey.CreateSubKey(@"shell\open\command", true);

            commandKey.SetValue("", $"\"{path}\" %1");

            Tracer.TraceInformation("registered.");
        }

        internal static void Unregister()
        {
            using var autoplayKey = GetAutoplayKey(Registry.CurrentUser, true);
            using var handlersKey = GetKey(autoplayKey, HandlersKey, true);

            handlersKey.DeleteSubKeyTree(HandlerKey);
                
            using var showPicturesKey = GetKey(autoplayKey, ShowPicturesOnArrivalKey, true);

            showPicturesKey.DeleteValue(HandlerKey);
            
            using var classesKey = GetClassesKey(Registry.CurrentUser, true);

            classesKey.DeleteSubKeyTree(ProgId);

            Tracer.TraceInformation("unregistered.");
        }

        private const string HandlersKey = "Handlers";
        private const string ShowPicturesOnArrivalKey = @"EventHandlers\ShowPicturesOnArrival";

        private const string HandlerKey = "CalteoImageImportHandler";
        private const string ProgId = "Calteo.Image.Import";

        private static RegistryKey GetKey(RegistryKey root, string name, bool writable = false)
        {
            var key = root.OpenSubKey(name, writable);

            if (key == null)
            {
                throw new NullReferenceException(@$"Registry key '{root.Name}\{name}' failed to open.");
            }

            return key;
        }

        private static RegistryKey GetAutoplayKey(RegistryKey root, bool writable = false)
        {
            return GetKey(root, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\AutoplayHandlers", writable);
        }

        private static RegistryKey GetClassesKey(RegistryKey root, bool writable = false)
        {
            return GetKey(root, @"SOFTWARE\Classes", writable);
        }
    }
}

