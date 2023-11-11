//
// ${Melquiceded Balbi Villanueva}
//
// Author:
//       ${Melquiceded} <${melquiceded.balbi@gmail.com}>
//
// Copyright (c) ${2089} MIT
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using QuickTools.QCore;
using QuickTools.QIO;

using System;
using System.Security.Cryptography.X509Certificates;

namespace ClownShell.ErrorHandler
{
    public enum ErrorType
    {
        NotValidAction,
        NotValidType,
        NotValidParameter,
        ExecutionError,
        NotImplemented,
        FATAL
    }
    public class ErrorHandeler
    {

        private void PrintFormat()
        {
            Get.Yellow();
            Get.Write("Action ");
            Get.Blue();
            Get.Write("Type ");
            Get.Blue();
            Get.Write("Parameters\n");
        }
        public void DisplayError(ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.NotValidAction:
                    Get.Red("The Given Action was not valid!!!");
                    this.PrintFormat();
                    break;
                case ErrorType.NotValidType:
                    Get.Red("The Given Type was not valid!!!");
                    this.PrintFormat();
                    break;
                case ErrorType.NotValidParameter:
                    Get.Red("The given Parameter's were not valid");
                    this.PrintFormat();
                    break;
                case ErrorType.ExecutionError:
                    Get.Red($"There was an error while Executing the Code");
                    this.PrintFormat();
                    break;
                case ErrorType.NotImplemented:
                    Get.Yellow($"This Command was recognized but is either not implemented or disabled");
                    break;
                case ErrorType.FATAL:
                    Get.Red($"There was a [FATAL] error and the system will not be able to recover from it ");
                    break;
            }
        }
        
        public void DisplayError(ErrorType type, string message)
        {

            string error = $"####\n There Was an error with the given type of error: '{type}' '{message}' \n####";
            Log.Event("ErrorHandeler", error);
           Get.Red(error);
        }
        public void DisplayError(ErrorType type, string[] givenCommand)
        {

            string error = $"####\n There Was an error with the given type of error: '{type}' '{IConvert.ArrayToText(givenCommand)}' \n####";
            Log.Event("ErrorHandeler", error);
            Get.Red(error);
        }
    }
}
