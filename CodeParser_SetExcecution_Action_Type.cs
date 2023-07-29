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
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; 
using QuickTools.QIO;
using QuickTools.QNet;
using QuickTools.QData;
using QuickTools.QCore;
using QuickTools.QColors;
using QuickTools.QConsole;
using QuickTools.QSecurity;
using QuickTools.QSecurity.FalseIO;
using System.Threading;

namespace ClownShell
{
    public partial class CodeParser
    {
     

        /// <summary>
        /// This method set the exectution delegate Action that will handle the excution of the program
        /// </summary>
        /// <param name="action"></param>
        /// <param name="type"></param>
        public void SetExecution(string action, string type)
        {

            switch (action)
            {


                case "mkdir":
                     break;
                case "touch":
                case "create":
                case "echo":
                     break;
                case "rm":
                case "remove":
                case "delete":
                    
                    
                    break;
                case "ls":
                case "list":
                case "list-files":
 
                     break;
                case "sleep":
                     
                    break;
                case "cd":
                      
                    break;
                case "cat":
                   
                    break;
                case "size":
                case "du":
                    
                    break;
                case "select":
                case "-S":
                    
                    break;
                case "get-hash":
                case "hash":
                
                        break;
                case "cmd":
                    
                    break;
                case "vim":
                    
                    break;
                case "nano":
                    
                    break;
                case "xxd":
                    
                    break;
                case "edit":
                case "notepad":
                    
                    break; 
                default:
                    error.DisplayError(ErrorHandeler.ErrorType.NotValidAction, this.Code);
                    break;
                    
            }
        }
    }
}
/*
 
        public static void OpenBrowser(string url)
        {
            if (Get.IsWindow())
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
                return;
            }
            else
            {
                Process.Start("open", url);

            }
        }
 
 
 */
