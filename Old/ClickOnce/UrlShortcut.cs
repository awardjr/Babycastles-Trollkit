using System;
using System.IO;

namespace ClickOnce
{
    internal class UrlShortcut
    {
        public static void AutoStart(bool enable)
        {
            if (!ClickOnceHelper.IsApplicationNetworkDeployed)
                throw new Exception("This application was not installed using ClickOnce.");
            
            String startupShortcut = ClickOnceHelper.GetStartupShortcut(ClickOnceHelper.AssemblyProductName,
                                                                            ClickOnceHelper.ShortcutType.Url);

            // Always remove the startup shortcut if it exists.  
            // This will handling disabling the run at startup functionality 
            // or ensure the most recent shortcut is copied into the Startup folder if we're enabling.
            if (File.Exists(startupShortcut))
            {
                File.Delete(startupShortcut);
            }

            if (!enable)
            {
                return;
            }

            using (var writer = new StreamWriter(startupShortcut))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=" + ClickOnceHelper.StartUpUri);
                writer.WriteLine("IconIndex=0");
                writer.WriteLine("IconFile=" + ClickOnceHelper.IconLocation);
                writer.Flush();
            }
        }
    }
}