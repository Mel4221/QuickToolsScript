using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickToolsScript
{
    public class DefaultSettings
    {
        public string DefaultScriptSavePath { get; set; }
        public string DefaultTypeOfSettings = "internal";
        public string DefaultCacheLocation { get; set; }

        public DefaultSettings(bool setToDefaultSettings)
        {
            if (setToDefaultSettings)
            {

            }   
        }
        public DefaultSettings()
        {
           
        }
    }
}
