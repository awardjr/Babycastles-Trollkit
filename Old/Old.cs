using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trollkit.Old {
    class Old {
        /*
        //install JoyToKey if it does not exist
        if (!File.Exists(General.ApplicationFolderPath + @"JoyToKey\JoyToKey.exe")) {
            #if (DEBUG)
            String archivePath = @"..\..\JoyToKey.zip";
            #else
            String archivePath = @"JoyToKey.zip";
            #endif

            using (ZipFile zip = ZipFile.Read(archivePath)) {
                zip.ExtractAll(General.ApplicationFolderPath, ExtractExistingFileAction.DoNotOverwrite); //the archive contains the folder JoyToKey
            }
        }
        */

        /*
        //save settings for last game played
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "LastGamePlayed", gameConfig.Path); //TODO: should just use a XML file instead, application config
        //for x64 adds to HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Trollkit
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "PathOfLastJoyToKeyConfigUsed", (String)joyToKeyComboBox.SelectedValue);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "FullScreen", fullScreenCheckBox.Checked);
        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "HideMouse", hideMouseCheckBox.Checked);
        */

        #region ClickOnce autostart
        /*
            if (autostartCheckBox.Checked) {
                //add application shortcut to startup folder
                #region old ClickOnce code
                //#if (!DEBUG)
                //ClickOnce.AppShortcut.AutoStart(true);
                //#endif
                #endregion

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "AutostartLastGame", true);
            }
            else {
                //remove application shortcut from startup folder
                #region old ClickOnce code
                #if (!DEBUG)
                ClickOnce.AppShortcut.AutoStart(false);
                #endif
                #endregion

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Trollkit", "AutostartLastGame", false);
            }
            */
        #endregion

        /*
            //example of writing to xml file
            XmlDocument configDoc = new XmlDocument();
            //configDoc.Load(configFilePath);
                
            //XmlNode xmlNode = configDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");

            //create <Root> Node
            //XmlElement rootElement = configDoc.CreateElement("configuration");
            //configDoc.AppendChild(rootElement);
                
            //create <InstallationId> Node
            XmlElement installationElement = configDoc.CreateElement("InstallationId");
            XmlText installationIdText = configDoc.CreateTextNode(Guid.Empty.ToString());
            installationElement.AppendChild(installationIdText);
            configDoc.ChildNodes.Item(0).AppendChild(installationElement);
            */
    }
}
