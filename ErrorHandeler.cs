using QuickTools.QCore;
using QuickTools.QIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickToolsScript
{
    public class ErrorHandeler
    {

        public enum ErrorType
        {
            NotValidAction,
            NotValidType,
            NotValidParameter,
            ExeutionError
        }

        public void DisplayError(ErrorType type, string message)
        {

            string error = $"####\n There Was an error with the given type of error: {type} {message} \n####";
            Log.Event("ErrorHandeler", error);
            Get.Wrong(error);
        }
        public void DisplayError(ErrorType type, string[] givenCommand)
        {

            string error = $"####\n There Was an error with the given type of error: {type} {IConvert.ArrayToText(givenCommand)} \n####";
            Log.Event("ErrorHandeler", error);
            Get.Wrong(error);
        }
    }
}
