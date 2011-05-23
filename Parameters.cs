using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;



namespace BabycastlesRunner
{
    class Parameters
    {


        public Boolean hideMouse;
        public Boolean useJoyToKey;
        public Boolean repositionMouse;
        public Boolean fullScreen;


        public int mouseX;
        public int mouseY;

        public String fullScreenSequence;

        public String gamePath;
        public String joyToKeyPath;

        public Parameters()
        {

        }

        public void loadParameters(String path)
        {

            StreamReader streamReader = new StreamReader(path);
            string parameterXml = streamReader.ReadToEnd();
            streamReader.Close();


            using (XmlReader reader = XmlReader.Create(new StringReader(parameterXml)))
            {
                reader.ReadToFollowing("gamePath");
                gamePath = reader.ReadElementContentAsString();
                
                reader.ReadToFollowing("useJoyToKey");
                useJoyToKey = reader.ReadElementContentAsBoolean();

                if (useJoyToKey)
                {
                    reader.ReadToFollowing("joyToKeyPath");
                    joyToKeyPath = reader.ReadElementContentAsString();
                }

                reader.ReadToFollowing("repositionMouse");
                repositionMouse = reader.ReadElementContentAsBoolean();

                if (repositionMouse)
                {
                    reader.ReadToFollowing("mouseX");
                    mouseX = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("mouseY");
                    mouseY = reader.ReadElementContentAsInt();
                }

                reader.ReadToFollowing("hideMouse");
               // hideMouse = reader.ReadElementContentAsBoolean();

                
            }

        }
    }
}
