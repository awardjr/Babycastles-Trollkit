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
        public readonly Boolean HideMouse; //note: readonly files can only be declared here or in the constructor
        public readonly Boolean UseJoyToKey;
        public readonly Boolean RepositionMouse;
        public readonly Boolean FullScreen;
        public readonly Int32 MouseX;
        public readonly Int32 MouseY;

        private String gameName; //had to make these properties to set it to Display/ValueMember
        public readonly String Author;
        public readonly String GamePath;
        public readonly String JoyToKeyPath;
        public readonly String DownloadUrl;
        public readonly Boolean IsPortable; //sometimes called standalone, as opposed to a game that requires installation
        public readonly Boolean IsArchived;

        public String GameName { get { return gameName; } /*set { gameName = value; }*/ }
        //upper case public members (fields?) and properties naming convention?

        public GameConfiguration(String filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string parameterXml = streamReader.ReadToEnd();
            streamReader.Close();

            using (XmlReader reader = XmlReader.Create(new StringReader(parameterXml)))
            {
                reader.ReadToFollowing("gameName");
                gameName = reader.ReadElementContentAsString();

                reader.ReadToFollowing("author");
                Author = reader.ReadElementContentAsString();

                reader.ReadToFollowing("downloadUrl");
                DownloadUrl = reader.ReadElementContentAsString();

                reader.ReadToFollowing("isPortable");
                IsPortable = reader.ReadElementContentAsBoolean();

                reader.ReadToFollowing("isArchived");
                IsArchived = reader.ReadElementContentAsBoolean();

                //TODO: if portable && not archived, C:\Portable Games\[GameName]
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
