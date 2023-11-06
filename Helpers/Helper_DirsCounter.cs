using QuickTools.QCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClownShell.Helpers
{
    public partial class Helper
    {
        /// <summary>
        /// This counts the number of directories in a path that 
        /// requireds to move back
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int CountDirs(string input)
        {
            int dots, dir;
            dots = 0;
            dir = 0;
            //box/../../../../
            for (int ch = input.Length-1; ch > 0; ch--)
            {
                if (input[ch] == '.')
                {
                    dots++;
                    if (dots==2)
                    {
                        dir++;
                        dots = 0;
                        //Get.Wait($"Dirs: {dir} Dots: {dots}");
                    }
                }

            }
            return dir;
        }
        /// <summary>
        /// Removes the directoies that is required to move bakc 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RemoveDirs(string path, int count)
        {
            if (!path.Contains('.'))
            {
                return null;
            }

            //Get.Blue(path);
            //Get.Yellow($"Dots Count: {count}");
            int length, index;
            string str;
            str = path;
            length = path.Length - count;


            str = str.Substring(0, length);
            str = str.Substring(0, str.IndexOf('.')-1);
            //Get.Red(str);
            //str = str.Substring(0, str.LastIndexOf(Get.Slash()));
            index = 0;
            for (int ch = str.Length; ch > 0; ch--)
            {
                str = str.Substring(0, str.LastIndexOf(Get.Slash()));
                //Get.Yellow($"{str} Index:{index} ch:{ch} Dirs:{count}");

                index++;
                if (index == count)
                {
                    return str;
                }

            }
            return path;
        }
    }
}
