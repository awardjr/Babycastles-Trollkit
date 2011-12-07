using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BabycastlesRunner
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
        public static string ProgramFilesx86Path()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }
}
