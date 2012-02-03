using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trollkit.Old {
    class Old {
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
    }
}
