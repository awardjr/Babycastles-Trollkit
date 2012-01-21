using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace Trollkit
{
    /// <summary>
    /// Contains some general classes and functions
    /// </summary>
    class General
    {
        public static class ListItemData
        {
            public static ListItemData<T> Create<T>(T value, string text)
            {
                return new ListItemData<T>
                {
                    Value = value,
                    Text = text
                };
            }
        }

        /// <summary>
        /// A convenient data structure to use with comboboxes
        /// </summary>
        public class ListItemData<T>
        {
            public T Value { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        /// <summary>
        /// Returns the path to program files x86 regardless of the windows architecture
        /// </summary>
        public static String ProgramFilesx86Path
        {
            get
            {
                if (8 == IntPtr.Size
                    || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
                {
                    return Environment.GetEnvironmentVariable("ProgramFiles(x86)") + "\\";
                }

                return Environment.GetEnvironmentVariable("ProgramFiles") + "\\";
            }
        }

        /// <summary>
        /// Returns Program Files\Troll Kit\
        /// </summary>
        public static String ApplicationFolderPath
        {
            get { return General.ProgramFilesx86Path + @"Troll Kit\"; }
        }

        /// <summary>
        /// Gets the handle, duh. Throws an exception if the handle is not found.
        /// </summary>
        public static IntPtr getHandleByProcessName(String processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes[0].MainWindowHandle;
        }

        /// <summary>
        /// Tries to kill the process
        /// </summary>
        public static void tryKillProcess(String processName)
        {
            foreach (Process p in Process.GetProcessesByName(processName))
            {
                try
                {
                    p.Kill();
                    p.WaitForExit(); // possibly with a timeout
                }
                catch (Win32Exception win32Exception)
                {
                    //process was terminating or can't be terminated - deal with it
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    //process has already exited - might be able to let this one go
                }

                //TODO: catch others? return isSuccessful?
            }
        }
    }
}
