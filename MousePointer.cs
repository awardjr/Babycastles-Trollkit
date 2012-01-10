using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Trollkit
{
    class MousePointer
    {
        [DllImport("user32.dll")]
        static extern int ShowCursor(bool bShow);

        public MousePointer()
        {
            
        }

        public void hide()
        {
            ShowCursor(false);
           
        }

        public void show()
        {
            ShowCursor(true);
        }
    }
}
