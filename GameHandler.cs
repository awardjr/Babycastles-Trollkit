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
using System.IO;

namespace Trollkit {
    class GameHandler {
        public GameHandler(ref GameConfiguration gameConfig, String joyToKeyConfigPath, Boolean arcadeMode, Boolean fullScreen, Boolean hideMouse) //TODO: need better boolean naming, use anonymous type to pass GUI parameters? Nah, use a struct.
        {
            if (arcadeMode)
                beginInArcadeMode(ref gameConfig, joyToKeyConfigPath, arcadeMode, fullScreen, hideMouse);
            else
                begin(ref gameConfig, joyToKeyConfigPath, arcadeMode, fullScreen, hideMouse);
        }

        private void begin(ref GameConfiguration gameConfig, String joyToKeyConfigPath, Boolean arcadeMode, Boolean fullScreen, Boolean hideMouse) {
            runJoyToKey(ref gameConfig, joyToKeyConfigPath);
            runGame(gameConfig, hideMouse, fullScreen);
            //TODO: need to close JoyToKey after game exits
        }

        private void beginInArcadeMode(ref GameConfiguration gameConfig, String joyToKeyConfigPath, Boolean inArcadeMode, Boolean fullScreen, Boolean hideMouse) {
            Boolean closed = true;
            Boolean stopRunner = false;
            Process game = new Process();
            Process joyToKey = new Process();
            GlobalMouseKeyboard globalMouseKeyboard = new GlobalMouseKeyboard();

            runJoyToKey(ref gameConfig, joyToKeyConfigPath);

            while (!stopRunner) {
                if (globalMouseKeyboard.F2IsPressed) {
                    //restart the game
                    game.Kill();
                    closed = true;
                    globalMouseKeyboard.F2IsPressed = false;
                }

                if (globalMouseKeyboard.F4IsPressed) {
                    //end arcade mode
                    game.Kill();
                    stopRunner = true;
                    globalMouseKeyboard.Dispose();
                    Rahil.Shared.tryKillProcess("JoyToKey");
                }

                if (closed) {
                    game = runGame(gameConfig, hideMouse, fullScreen);

                    closed = false;
                }

                game.WaitForExit(100); //? to reduce cpu usage?

                if (game.HasExited) {
                    closed = true;
                }
            }
        }

        private Process runGame(GameConfiguration gameConfig, Boolean hideMouse, Boolean fullScreen) {
            if (hideMouse) //TODO: || gameConfig.HideMouse
                Cursor.Position = new Point(2000, 2000); //work around
            //another work around, set the cursor graphic to a transparent one, http://forums.whirlpool.net.au/archive/1172326

            ProcessStartInfo psi = new ProcessStartInfo(gameConfig.Path);
            if (fullScreen) //TODO: || gameConfig.FullScreen
                psi.WindowStyle = ProcessWindowStyle.Maximized; //TODO: only maximizes fully if the taskbar is set to auto-hide

            return Process.Start(psi);
        }

        private void runJoyToKey(ref GameConfiguration gameConfig, String joyToKeyConfigPath) {
            //close JoyToKey
            //TODO: should open a different config file instead of restarting the application
            Rahil.Shared.tryKillProcess("JoyToKey");

            if (joyToKeyConfigPath != String.Empty) {
                //run JoyToKey
                String joyToKeyFolderPath = Global.ApplicationFolderPath + @"JoyToKey\";
                String joyToKeyFilePath = joyToKeyFolderPath + "JoyToKey.exe";

                ProcessStartInfo joyToKeyPsi = new ProcessStartInfo(joyToKeyFilePath, '"' + Path.GetFileNameWithoutExtension(joyToKeyConfigPath) + '"');
                joyToKeyPsi.WorkingDirectory = joyToKeyFolderPath;
                joyToKeyPsi.WindowStyle = ProcessWindowStyle.Minimized; //TODO: not working
                //MinimizeWindow.minimize("JoyToKey"); //fail
                Process.Start(joyToKeyPsi);
            }
        }
    }
}
