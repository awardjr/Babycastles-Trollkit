using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace BabycastlesRunner
{
    /// <summary>
    /// Holds game configuration settings
    /// </summary>
   public  class GameConfiguration
    {
        public Boolean hideMouse;
        public Boolean useJoyToKey;
        public Boolean repositionMouse;
        public Boolean fullScreen;

        public int mouseX;
        public int mouseY;

        public String joyToKeyPath;

        private String gameName; //had to make these properties to set it to Display/ValueMember
        public String GameName { get { return gameName; } /*set { gameName = value; }*/ }
        private String gamePath; //upper case public members (fields?) and properties naming convention?
        public String GamePath { get { return gamePath; } /*set { gamePath = value; }*/ }

        public GameConfiguration(String filePath)
        {
            load(filePath);
        }

        public void load(String filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string parameterXml = streamReader.ReadToEnd();
            streamReader.Close();

            using (XmlReader reader = XmlReader.Create(new StringReader(parameterXml)))
            {
                reader.ReadToFollowing("gameName");
                gameName = reader.ReadElementContentAsString();

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
