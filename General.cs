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

        public class ListItemData<T>
        {
            public T Value { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}
