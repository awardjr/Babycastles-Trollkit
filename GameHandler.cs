using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

namespace Trollkit
{
    class GameHandler
    {
        public GameHandler(ref GameConfiguration gameConfig, Boolean inArcadeMode)
        {
            if (inArcadeMode)
                beginInArcadeMode(ref gameConfig);
            else
                begin(ref gameConfig);
        }

        public void begin(ref GameConfiguration gameConfig) //TODO: duplicate code, could put in one function
        {
            //run Joy2Key
            if (gameConfig.UseJoyToKey) //TODO: need to test this, come back to this later
            {
                ProcessStartInfo joy2KeyPsi = new ProcessStartInfo(gameConfig.JoyToKeyPath);
                joy2KeyPsi.UseShellExecute = false;
                joy2KeyPsi.RedirectStandardOutput = true;
                joy2KeyPsi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                Process.Start(joy2KeyPsi);
            }

            //run game
            //startGame(ref gameConfig);
            ProcessStartInfo gamePsi = new ProcessStartInfo(gameConfig.GamePath);
            gamePsi.RedirectStandardOutput = false;
            gamePsi.UseShellExecute = true;
            Process.Start(gamePsi);
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
                if (globalMouseKeyboard.F2IsPressed)
                {
                    //restart the game
                    game.Kill();
                    closed = true;
                    globalMouseKeyboard.F2IsPressed = false;
                }

                if (globalMouseKeyboard.F4IsPressed)
                {
                    //end arcade mode
                    game.Kill();
                    stopRunner = true;
                    Taskbar.Show();
                    //Cursor.Show();
                    //pointer.show();
                    globalMouseKeyboard.Dispose();
                }

                if (closed)
                {
                    if (gameConfig.HideMouse)
                    {
                        //Cursor.Hide(); //fail
                        //pointer.hide(); //fail
                        Cursor.Position = new Point(2000, 2000); //work around
                        //another work around, set the cursor graphic to a transparent one, http://forums.whirlpool.net.au/archive/1172326
                    }

                    //if (gameConfig.UseJoyToKey)
                    //{
                    //    ProcessStartInfo psiJoy = new ProcessStartInfo(gameConfig.JoyToKeyPath);
                    //    psiJoy.UseShellExecute = false;
                    //    psiJoy.RedirectStandardOutput = true;
                    //    psiJoy.WindowStyle = ProcessWindowStyle.Minimized;
                    //    joyToKey = Process.Start(psiJoy);
                    //}

                    ProcessStartInfo psi = new ProcessStartInfo(gameConfig.GamePath);
                    psi.UseShellExecute = true;
                    psi.RedirectStandardOutput = false;
                    //TODO: if full screen, or center window
                    Taskbar.Hide();
                    psi.WindowStyle = ProcessWindowStyle.Maximized; //TODO: only maximizes fully if the taskbar is set to auto-hide
                    game = Process.Start(psi);

                    //Titlebar.Hide(); //fail
                    //FullScreen.set(game.MainWindowHandle); //fail

                    closed = false;
                }

                //game.WaitForExit(100); //?

                if (game.HasExited)
                {
                    closed = true;
                }
            }
        }

        //TODO
        private Process startGame(ref GameConfiguration gameConfig)
        {
            ProcessStartInfo gamePsi = new ProcessStartInfo(gameConfig.GamePath);
            gamePsi.RedirectStandardOutput = false;
            gamePsi.UseShellExecute = true;

            return Process.Start(gamePsi);
        }
    }
}
