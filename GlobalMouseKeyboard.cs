using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System.Windows.Forms;

namespace BabycastlesRunner
{
    public class GlobalMouseKeyboard //TODO: should probably make this static instead...
    {
        private readonly KeyboardHookListener keyboardHookManager;

        private Boolean aIsPressed;
        public Boolean AIsPressed { get { return aIsPressed; } }
        private Boolean zIsPressed;
        public Boolean ZIsPressed { get { return zIsPressed; } }

        public GlobalMouseKeyboard()
        {
            keyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            keyboardHookManager.Enabled = true;
            hookInputs();
        }

        private void hookInputs()
        {
            keyboardHookManager.KeyPress += hookManager_KeyPress;
        }

        private void unhookInputs()
        {
            keyboardHookManager.KeyPress -= hookManager_KeyPress;
        }

        private void hookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            aIsPressed = false; //TODO: why is this working?
            zIsPressed = false;

            if (e.KeyChar == 'a' || e.KeyChar == 'A') //TODO: use escape and F8?
            {
                aIsPressed = true;
                e.Handled = true;
            }

            if (e.KeyChar == 'z' || e.KeyChar == 'Z')
            {
                zIsPressed = true;
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
