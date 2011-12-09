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
                beginInArcadeMode(ref gameConfig);
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

        public void beginInArcadeMode(ref GameConfiguration gameConfig)
        {
            MousePointer pointer = new MousePointer();
            Boolean closed = true;
            Boolean stopRunner = false;

            Process game = new Process();
            Process joyToKey = new Process();

            GlobalMouseKeyboard globalMouseKeyboard = new GlobalMouseKeyboard();

            while (!stopRunner)
            {
                //restart the game
                if (globalMouseKeyboard.AIsPressed)
                {
                    game.Kill();
                    closed = true;
                }

                //stop the auto-handler
                if (globalMouseKeyboard.ZIsPressed)
                {
                    stopRunner = true;
                    Taskbar.Show();
                    pointer.show();
                    globalMouseKeyboard.Dispose();
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

                    System.Diagnostics.ProcessStartInfo psi = new ProcessStartInfo(gameConfig.GamePath);
                    System.Diagnostics.ProcessStartInfo psiJoy = new ProcessStartInfo(gameConfig.JoyToKeyPath);
                    psi.RedirectStandardOutput = false;
                    psi.WindowStyle = ProcessWindowStyle.Maximized; //TODO: only maximizes fully if the taskbar is set to auto-hide
                    psi.UseShellExecute = true;

                    if (gameConfig.UseJoyToKey)
                    {
                        psiJoy.RedirectStandardOutput = true;
                        psiJoy.WindowStyle = ProcessWindowStyle.Minimized;
                        psiJoy.UseShellExecute = false;

                        joyToKey = Process.Start(psiJoy);
                    }

                    game = Process.Start(psi);
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
