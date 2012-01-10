using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Trollkit
{
    /// <summary>
    /// Holds game configuration settings
    /// </summary>
    public class GameConfiguration
    {
        public String GameName { get; private set; } //had to make these properties to set it to Display/ValueMember, TODO: use ListDataItem instead
        public readonly String Author; //note: readonly variables can only be declared here or in the constructor
        public readonly String DownloadUrl;
        public readonly Boolean IsPortable; //sometimes called standalone, as opposed to a game that requires installation
        public readonly Boolean IsArchived;
        public readonly String GamePath;
        public readonly String JoyToKeyPath;
        public readonly Boolean HideMouse;
        public readonly Boolean UseJoyToKey;
        public readonly Boolean RepositionMouse;
        public readonly Boolean FullScreen;

        public GameConfiguration(String filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string parameterXml = streamReader.ReadToEnd();
            streamReader.Close();

            using (XmlReader reader = XmlReader.Create(new StringReader(parameterXml)))
            {
                reader.ReadToFollowing("gameName");
                GameName = reader.ReadElementContentAsString();

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

                reader.ReadToFollowing("hideMouse");
                HideMouse = reader.ReadElementContentAsBoolean();

                reader.ReadToFollowing("fullScreen");
                FullScreen = reader.ReadElementContentAsBoolean();
            }
        }
    }
}
