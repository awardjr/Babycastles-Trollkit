using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MouseKeyboardActivityMonitor; //http://globalmousekeyhook.codeplex.com/
using MouseKeyboardActivityMonitor.WinApi;
using System.Windows.Forms;

namespace Trollkit
{
    class GlobalMouseKeyboard //TODO: should probably make this static instead...
    {
        private readonly KeyboardHookListener keyboardHookManager;

        public Boolean F2IsPressed { get; set; } //should be readonly / private set;
        public Boolean F4IsPressed { get; set; }

        public GlobalMouseKeyboard()
        {
            keyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            keyboardHookManager.Enabled = true;
            hookInputs();
        }

        private void hookInputs()
        {
            //keyboardHookManager.KeyPress += hookManager_KeyPress; //non-character keys are not captured by KeyPress
            keyboardHookManager.KeyUp += hookManager_KeyUp;
        }

        private void unhookInputs()
        {
            //keyboardHookManager.KeyPress -= hookManager_KeyPress;
            keyboardHookManager.KeyUp -= hookManager_KeyUp;
        }

        private void hookManager_KeyUp(object sender, KeyEventArgs e)
        {
            //F2IsPressed = false; //this worked for KeyPress somehow
            //F4IsPressed = false;

            if (e.KeyCode == Keys.F2) //TODO: control + R, control + S or E
            {
                F2IsPressed = true;
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F4)
            {
                F4IsPressed = true;
                e.Handled = true;
            }
        }

        public void Dispose() //TODO: need to learn .NET disposal
        {
            unhookInputs();
            keyboardHookManager.Dispose();
        }
    }
}
