using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BabycastlesRunner
{
    class GameHandler
    {
        public GameHandler()
        {

        }

        public void begin(string pathToGameConfig)
        {
            Cursor.Hide();
            Boolean closed = true;
            Boolean stopRunner = false;
            MousePointer pointer = new MousePointer();

            GameConfiguration gameConfig = new GameConfiguration(pathToGameConfig);

            System.Diagnostics.Process game;
            System.Diagnostics.Process joyToKey;

            gameConfig.useJoyToKey = false;
            gameConfig.hideMouse = true;

            game = new System.Diagnostics.Process();
            joyToKey = new System.Diagnostics.Process();


            while (!stopRunner)
            {
                //restart the game
                if (Keyboard.IsKeyDown(Keys.A))
                {
                    game.Kill();
                    closed = true;
                }

                //stop the auto-handler
                if (Keyboard.IsKeyDown(Keys.Z))
                {
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

                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(gameConfig.GamePath);
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
