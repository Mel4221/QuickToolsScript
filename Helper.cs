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
using System.IO;

namespace QuickToolsScript
{
    static class Helper
    {


        /// <summary>
        /// returns the disks in the system
        /// </summary>
        /// <returns></returns>
        public static string[] Disks() => Environment.GetLogicalDrives();


        /*
                 foreach (DriveInfo d in allDrives)
        {
            Console.WriteLine("Drive {0}", d.Name);
            Console.WriteLine("  Drive type: {0}", d.DriveType);
            if (d.IsReady == true)
            {
                Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                Console.WriteLine("  File system: {0}", d.DriveFormat);
                Console.WriteLine(
                    "  Available space to current user:{0, 15} bytes",
                    d.AvailableFreeSpace);

                Console.WriteLine(
                    "  Total available space:          {0, 15} bytes",
                    d.TotalFreeSpace);

                Console.WriteLine(
                    "  Total size of drive:            {0, 15} bytes ",
                    d.TotalSize);
            }
        }
         
         */
       
 


        /// <summary>
        /// provides the inforamtion wether the input path has a direct reference to a disk
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ReferToDisk(string input)
        {
            bool refer = false;
            string path, drive;
            path = Get.FixPath(input);
            drive = path.Substring(0, path.IndexOf(Get.Slash()) + 1);
            foreach (string disk in Disks())
            {
                //Get.Green(disk);
                if (disk == drive.ToUpper())
                {
                    return true;
                    //Get.Yellow(drive);
                }
            }

            return refer;
        }

        /// <summary>
        /// Resolve the path of the given file or folder 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ResolvePath(string path)
        {
            
            return path;
        }

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
