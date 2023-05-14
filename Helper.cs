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
        /// <param name="param"></param>
        /// <returns></returns>
        public static string CheckForPath(string param)
        {
            if (param[0] == '~')
            {
                string p = param.Substring(param.IndexOf(Get.Slash()) + 1).ToLower();
                string str2, str, str3, path;
                str = null;
                str2 = null;
                str3 = null;
                path = null;
                if (p.Contains(Get.Slash()))
                {
                    str2 = p.Substring(p.LastIndexOf(Get.Slash()));
                    p = p.Substring(0, p.IndexOf(Get.Slash()));
                    Get.Yellow(str2);
                }
                Get.Red(p);
                //Get.Wait();
                switch (p)
                {
                    case "desktop":
                        str3 = str2 == null ? null : str2;
                        return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + str3;

                    case "documents":
                        str3 = str2 == null ? null : str2;
                        return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + str3;

                    case "downloads":
                        str3 = str2 == null ? null : str2;
                        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        str = $"{path.Substring(0, path.LastIndexOf(Get.Slash()))}{Get.Slash()}Downloads";
                        return str + str3;

                    case "mycomputer":
                        return Environment.GetFolderPath(Environment.SpecialFolder.MyComputer) + str3;
                    default:
                        return param;
                }

            }
            else
            {
                return param;
            }
        }
    }
}
