
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
// THE SOFTWARE.using System;
using System; 
using System.IO;
using System.Diagnostics;
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
using System.Threading;
using QuickTools.QSecurity.FalseIO;

namespace ClownShell
{
    public partial class CodeParser
    {
        private string[] GetParameters(string[] parameters)
        {
            string[] param;
            int current, goal;
            goal = parameters.Length;
            string cmds = ""; 
            for (current = 2; current < goal; current++)
            {
                cmds +=  Get.FixPath(parameters[current])+" ";
            }
            param = IConvert.TextToArray(cmds);
            return param;
        }

        public void SetExecution(string action, string type, string[] parameters)
        {
           
 
            switch (action)
            {
                case "mv":
                   



                    break;
                case "create":
                 

                    break;
                case "rm":
                  

                    break;
                case "find":
                case "search":
                  

                    break;
                case "cp":
                  
                    break;
                case "ls":
                case "list":
                case "list-files":
                 
                       
                    break;
                case "exe":
               
                    break;
                case "wget":
                    break;
                case "secure":
                    switch (type)
                    {
                        case "encrypt":
                        case "-e":
                        case "-E":
                            
                            break;
                        case "decrypt":
                        case "-d":
                        case "-D":
                            Get.Red("Not Done Yet");
                            break;
                    }                    
                    break;
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
            }
        }

    }
}
