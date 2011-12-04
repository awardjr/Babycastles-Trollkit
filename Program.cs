using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing;


namespace BabycastlesRunner
{
    class Program
    {
       
      

        static void Main(string[] args)
        {

            Cursor.Hide();
            Boolean closed = true;
            Boolean stopRunner = false;
            MousePointer pointer = new MousePointer();
            
            Parameters myParameters = new Parameters();
            myParameters.loadParameters("C:/Users/Latio/Documents/My Dropbox/Projects/Babycastles/BabycastlesRunner/BabycastlesRunner/test.xml");

            System.Diagnostics.Process game;
            System.Diagnostics.Process joyToKey;

   
            myParameters.useJoyToKey = false;
            myParameters.hideMouse = true;
         
            game = new System.Diagnostics.Process();
            joyToKey = new System.Diagnostics.Process();

          
            while (!stopRunner)
            {

                
                if (Keyboard.IsKeyDown(Keys.Add))
                {
                    game.Kill();
                    closed = true;
                    
                }


                if (Keyboard.IsKeyDown(Keys.Subtract))
                {
                    stopRunner = true;
                    Taskbar.Show();
                    pointer.show();

                }
                pointer.hide();

                if (closed)
                {
                    Taskbar.Hide();

                    if (myParameters.repositionMouse)
                        Cursor.Position = new Point(myParameters.mouseX, myParameters.mouseY);

                    //if (myParameters.hideMouse)
                        pointer.hide();

                        Cursor.Position = new Point(500000, 500000);

                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(@myParameters.gamePath);
                    System.Diagnostics.ProcessStartInfo psiJoy = new System.Diagnostics.ProcessStartInfo(@myParameters.joyToKeyPath);
                    psi.RedirectStandardOutput = false;
                    psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                    psi.UseShellExecute = true;
                    if (myParameters.useJoyToKey)
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
            Cursor.Show();
        }

     
     
    }
}