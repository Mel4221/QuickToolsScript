using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickTools.QIO;
using QuickTools.QNet;
using QuickTools.QData;
using QuickTools.QCore;
using QuickTools.QColors;
using QuickTools.QConsole;
using QuickTools.QSecurity;
using QuickTools.QSecurity.FalseIO;
namespace QuickToolsScript
{
    static class Helper
    {




        /// <summary>
        /// this method take advantage of a path that it's given 
        /// gets the name path name and identify if is an enviroment path 
        /// and add it to the path and if it contains a file in it it does not delet it it add it up 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string HasSpecialFolder(string path)
        {
            if (path[0] == '~')
            {
                path = Get.FixPath(path);
                string file, subFolder, specialCase, slash;
                file = null;
                subFolder = null;
                if (path.Contains("."))
                {
                    file = Get.FileNameFromPath(path);
                    path = Get.FolderFromPath(path);


                }
                //removes the first slash
                path = path.Substring(path.IndexOf(Get.Slash()) + 1).ToLower();

                if (path.Contains(Get.Slash()))
                {
                    subFolder = path.Substring(path.IndexOf(Get.Slash()));
                }
                if (path.Contains(Get.Slash()))
                {
                    path = path.Substring(0, path.IndexOf(Get.Slash()));

                }
                //Get.Wait(path);

                switch (path)
                {
                    case "desktop":
                        //str3 = str2 == null ? null : str2;
                        return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"{subFolder}{file}";
                    case "documents":
                        //  str3 = str2 == null ? null : str2;
                        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"{subFolder}{file}";
                    case "downloads":
                        //slash = subfolde
                        specialCase = $"{Get.Slash()}..{Get.Slash()}Downloads{subFolder}{file}";
                        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + specialCase;
                    case "pictures":
                        //slash = subfolde
                        return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + $"{subFolder}{file}";
                    case "music":
                        //slash = subfolde
                        return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + $"{subFolder}{file}";
                    case "videos":
                        //slash = subfolde
                        return Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + $"{subFolder}{file}";
                    case "mycomputer":
                        return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer) + $"{subFolder}{file}";
                    default:
                        return null;
                }

            }
            else
            {
                return null;
            }
        }

    }
}
