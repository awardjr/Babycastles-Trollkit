using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Trollkit
{
    //this failed
    //point.show/hide did not work either
    class MousePointer
    {
        [DllImport("user32.dll")]
        static extern int ShowCursor(bool bShow);

        public MousePointer()
        {
            
        }

        public static void hide()
        {
            ShowCursor(false);
           
        }

        public static void show()
        {
            ShowCursor(true);
        }
    }
}
