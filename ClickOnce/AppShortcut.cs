using System;
using System.IO;

namespace ClickOnce
{
    internal class AppShortcut
    {
        public static void AutoStart(bool enable)
        {
            if (!ClickOnceHelper.IsApplicationNetworkDeployed)
                throw new Exception("This application was not installed using ClickOnce.");

            String startupShortcut = ClickOnceHelper.GetStartupShortcut(ClickOnceHelper.AssemblyProductName,
                                                                        ClickOnceHelper.ShortcutType.Application);

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

            String programShortcut = ClickOnceHelper.GetProgramShortcut(ClickOnceHelper.AssemblyCompanyName,
                                                                        ClickOnceHelper.AssemblyProductName);

            if (File.Exists(programShortcut))
            {
                // Enable run at startup by copying the progam shortcut into the startup folder.
                File.Copy(programShortcut, startupShortcut);
            }
        }
    }
}