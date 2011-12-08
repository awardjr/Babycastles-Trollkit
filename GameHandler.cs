using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using MouseKeyboardActivityMonitor; //global/low level input hook library, http://globalmousekeyhook.codeplex.com/
using MouseKeyboardActivityMonitor.WinApi;

namespace BabycastlesRunner
{
    class GameHandler
    {
        public GameHandler(ref GameConfiguration gameConfig, Boolean inArcadeMode)
        {
            if (inArcadeMode)
            {
                hookInputs();
                beginInArcadeMode(ref gameConfig);
                unhookInputs();
            }
            else
                begin(ref gameConfig);
        }

        public void begin(ref GameConfiguration gameConfig) //TODO: duplicate code, could put in one function
        {
            ProcessStartInfo psi = new ProcessStartInfo(gameConfig.GamePath);
            ProcessStartInfo psiJoy = new ProcessStartInfo(gameConfig.JoyToKeyPath);

            psi.RedirectStandardOutput = false;
            psi.UseShellExecute = true;

            if (gameConfig.UseJoyToKey)
            {
                psiJoy.RedirectStandardOutput = true;
                psiJoy.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                psiJoy.UseShellExecute = false;

                Process joyToKey = Process.Start(psiJoy);
            }

            Process game = Process.Start(psi);
        }

        //TODO: should write a wrapper class for all of this keyboard crap...*cough* Arthur =)
        //GlobalKeyboard.start() and .end()
        private Boolean restartButtonIsPressed = false;
        private Boolean stopButtonIsPressed = false;

        #region keyboard crap, don't look!
        private KeyboardHookListener keyboardHookManager; //TODO: should be readonly

        public void hookInputs()
        {
            keyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            keyboardHookManager.Enabled = true;
            keyboardHookManager.KeyPress += HookManager_KeyPress;

        }

        public void unhookInputs()
        {
            keyboardHookManager.KeyPress -= HookManager_KeyPress;
            keyboardHookManager.Enabled = false;
            //m_KeyboardHookManager.Dispose(); //invalid hook handle error
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a' || e.KeyChar == 'A') //TODO: use escape and F8?
            {
                Debug.WriteLine("key press caught by global class");
                restartButtonIsPressed = true;
                e.Handled = true;
            }

            if (e.KeyChar == 'z' || e.KeyChar == 'Z')
            {
                stopButtonIsPressed = true;
                e.Handled = true;
            }
        }
        #endregion

        public void beginInArcadeMode(ref GameConfiguration gameConfig)
        {
            MousePointer pointer = new MousePointer();
            Boolean closed = true;
            Boolean stopRunner = false;

            Process game = new System.Diagnostics.Process();
            Process joyToKey = new System.Diagnostics.Process();

            while (!stopRunner)
            {
                //restart the game
                if (restartButtonIsPressed)
                {
                    restartButtonIsPressed = false;
                    game.Kill();
                    closed = true;
                }

                //stop the auto-handler
                if (stopButtonIsPressed)
                {
                    stopButtonIsPressed = false;
                    stopRunner = true;
                    Taskbar.Show();
                    pointer.show();
                }

                if (closed)
                {
                    Taskbar.Hide();

                    if (gameConfig.RepositionMouse)
                        Cursor.Position = new Point(gameConfig.MouseX, gameConfig.MouseY);

                    if (gameConfig.HideMouse)
                    {
                        Cursor.Hide();
                        pointer.hide();
                    }

                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(gameConfig.GamePath);
                    System.Diagnostics.ProcessStartInfo psiJoy = new System.Diagnostics.ProcessStartInfo(gameConfig.JoyToKeyPath);
                    psi.RedirectStandardOutput = false;
                    psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized; //TODO: only maximizes fully if the taskbar is set to auto-hide
                    psi.UseShellExecute = true;

                    if (gameConfig.UseJoyToKey)
                    {
                        psiJoy.RedirectStandardOutput = true;
                        psiJoy.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                        psiJoy.UseShellExecute = false;

                        joyToKey = System.Diagnostics.Process.Start(psiJoy);
                    }

                    game = System.Diagnostics.Process.Start(psi);
                    //Titlebar.Hide(); //fail

                    closed = false;
                }

                game.WaitForExit(100);
                if (game.HasExited)
                {
                    closed = true;
                }
            }
        }
    }
}
