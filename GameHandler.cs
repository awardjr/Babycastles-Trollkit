using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System.ComponentModel;

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

        private void begin(ref GameConfiguration gameConfig)
        {
            //run JoyToKey
            runJoyToKey(ref gameConfig);

            //run game
            Process.Start(gameConfig.GamePath);

            //TODO: need to close JoyToKey after game exits
        }

        private void beginInArcadeMode(ref GameConfiguration gameConfig)
        {
            Boolean closed = true;
            Boolean stopRunner = false;
            Process game = new Process();
            Process joyToKey = new Process();
            GlobalMouseKeyboard globalMouseKeyboard = new GlobalMouseKeyboard();

            runJoyToKey(ref gameConfig);

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
                    globalMouseKeyboard.Dispose();
                    General.tryKillProcess("JoyToKey");
                }

                if (closed)
                {
                    if (gameConfig.HideMouse)
                        Cursor.Position = new Point(2000, 2000); //work around
                        //another work around, set the cursor graphic to a transparent one, http://forums.whirlpool.net.au/archive/1172326

                    ProcessStartInfo psi = new ProcessStartInfo(gameConfig.GamePath);
                    if (gameConfig.FullScreen)
                        psi.WindowStyle = ProcessWindowStyle.Maximized; //TODO: only maximizes fully if the taskbar is set to auto-hide
                    game = Process.Start(psi);

                    closed = false;
                }

                game.WaitForExit(100); //? to reduce cpu usage?

                if (game.HasExited)
                {
                    closed = true;
                }
            }
        }

        private void runJoyToKey(ref GameConfiguration gameConfig)
        {
            //close JoyToKey
            //TODO: should open a different config file instead of restarting the application
            General.tryKillProcess("JoyToKey");

            if (gameConfig.UsesJoyToKey)
            {
                //run JoyToKey
                String joyToKeyFolderPath = General.ApplicationFolderPath + @"JoyToKey\";
                String joyToKeyFilePath = joyToKeyFolderPath + "JoyToKey.exe";

                ProcessStartInfo joyToKeyPsi = new ProcessStartInfo(joyToKeyFilePath, '"' + gameConfig.GameName + ".cfg" + '"');
                joyToKeyPsi.WorkingDirectory = joyToKeyFolderPath;
                joyToKeyPsi.WindowStyle = ProcessWindowStyle.Minimized; //TODO: not working
                Process.Start(joyToKeyPsi);
            }
        }
    }
}
