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
    public class GameConfiguration
    {
        public Boolean HideMouse;
        public Boolean UseJoyToKey;
        public Boolean RepositionMouse;
        public Boolean FullScreen;

        public Int32 MouseX;
        public Int32 MouseY;

        public String GamePath;
        public String JoyToKeyPath;
        public String DownloadUrl;
        public Boolean IsPortable; //sometimes called standalone, as opposed to 

        private String gameName; //had to make these properties to set it to Display/ValueMember
        public String GameName { get { return gameName; } /*set { gameName = value; }*/ }
        //upper case public members (fields?) and properties naming convention?

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

                reader.ReadToFollowing("downloadUrl");
                DownloadUrl = reader.ReadElementContentAsString();

                reader.ReadToFollowing("isPortable");
                UseJoyToKey = reader.ReadElementContentAsBoolean();

                reader.ReadToFollowing("gamePath");
                GamePath = reader.ReadElementContentAsString();
                
                reader.ReadToFollowing("useJoyToKey");
                UseJoyToKey = reader.ReadElementContentAsBoolean();

                if (UseJoyToKey)
                {
                    reader.ReadToFollowing("joyToKeyPath");
                    JoyToKeyPath = reader.ReadElementContentAsString();
                }

                reader.ReadToFollowing("repositionMouse");
                RepositionMouse = reader.ReadElementContentAsBoolean();

                if (RepositionMouse)
                {
                    reader.ReadToFollowing("mouseX");
                    MouseX = reader.ReadElementContentAsInt();

                    reader.ReadToFollowing("mouseY");
                    MouseY = reader.ReadElementContentAsInt();
                }

                reader.ReadToFollowing("hideMouse");
                //hideMouse = reader.ReadElementContentAsBoolean();
            }
        }
    }
}
