using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Gma.UserActivityMonitor;
using System.Diagnostics;
//using Gma.UserActivityMonitor //global/low level input hook library, http://www.codeproject.com/KB/cs/globalhook.aspx?msg=3505023#xx3505023xx

namespace BabycastlesRunner
{
    class GameHandler
    {
        public GameHandler(GameConfiguration gameConfig)
        {
            hookInputs();
            begin(gameConfig);
            unhookInputs();
            //TODO: destroy this afterwards!
        }

        //TODO: should write a wrapper class for all of this keyboard crap...*cough* Arthur =).
        //unless I just suck and Keyboard.cs was working after all?
        private Boolean restartButtonIsPressed = false;
        private Boolean stopButtonIsPressed = false;

        public void hookInputs()
        {
            HookManager.KeyPress += HookManager_KeyPress;
        }

        public void unhookInputs()
        {
            HookManager.KeyPress -= HookManager_KeyPress;
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a' || e.KeyChar == 'A')
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

        public void begin(GameConfiguration gameConfig)
        {
            Cursor.Hide();
            Boolean closed = true;
            Boolean stopRunner = false;
            MousePointer pointer = new MousePointer();

            System.Diagnostics.Process game;
            System.Diagnostics.Process joyToKey;

            game = new System.Diagnostics.Process();
            joyToKey = new System.Diagnostics.Process();


            while (!stopRunner)
            {
                //restart the game
                
                //if (Keyboard.IsKeyDown(Keys.A))
                if (restartButtonIsPressed)
                {
                    restartButtonIsPressed = false;
                    game.Kill();
                    closed = true;
                }

                //stop the auto-handler
                //if (Keyboard.IsKeyDown(Keys.Z))
                if (stopButtonIsPressed)
                {
                    stopButtonIsPressed = false;
                    //Debug.WriteLine("key press caught by simple class");
                    stopRunner = true;
                    Taskbar.Show();
                    pointer.show();
                }
                
                pointer.hide();

                if (closed)
                {
                    Taskbar.Hide();

                    if (gameConfig.repositionMouse)
                        Cursor.Position = new Point(gameConfig.mouseX, gameConfig.mouseY);

                    // if (myParameters.hideMouse)
                    pointer.hide();

                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(gameConfig.gamePath);
                    System.Diagnostics.ProcessStartInfo psiJoy = new System.Diagnostics.ProcessStartInfo(gameConfig.joyToKeyPath);
                    psi.RedirectStandardOutput = false;
                    psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                    psi.UseShellExecute = true;
                    if (gameConfig.useJoyToKey)
                    {
                        psiJoy.RedirectStandardOutput = true;
                        psiJoy.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                        psiJoy.UseShellExecute = false;

                        joyToKey = System.Diagnostics.Process.Start(psiJoy);
                    }
                    game = System.Diagnostics.Process.Start(psi);

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
