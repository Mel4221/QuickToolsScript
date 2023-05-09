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
using QuickTools.QIO;
using QuickTools.QNet;
using QuickTools.QData;
using QuickTools.QCore;
using QuickTools.QColors;
using QuickTools.QConsole;
using QuickTools.QSecurity;
using QuickTools.QCore;
using QuickTools.QSecurity.FalseIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickToolsScript
{
    public partial class CodeParser
    {

   


 


        private string[] GetParameters(string[] parameters)
        {
            string[] param = null;


            return param; 
        }
   


        private void Parse(CodeType codeType)
        {
            string action, type;

            switch (codeType)
            {
                case CodeType.Action:
                    action = this.Code[0];
                    this.SetExecution(action);
                    break;
                case CodeType.ActionWithType:
                    action = this.Code[0];
                    type = this.Code[1];
                    this.SetExecution(action, type); 
                    break;
                case CodeType.Complete:
                    action = this.Code[0];
                    type = this.Code[1];
                    this.SetExecution(action, type,this.Code);
                    break; 
            }
        }
        
        public void Start()
        {
            switch(this.Code.Length)
            {
                case 0:
                    this.error = new ErrorHandeler();
                    this.error.DisplayError(ErrorHandeler.ErrorType.NotValidAction,this.Code);
                    break; 
                case 1:
                    this.Parse(CodeType.Action); 
                    break;
                case 2:
                    this.Parse(CodeType.ActionWithType);
                    break;
                default:
                    this.Parse(CodeType.Complete);
                    break;
            }

        }

        public CodeParser(string frofileOrCode)
        {
            this.Code = IConvert.TextToArray(frofileOrCode);
        }
        public CodeParser(string[] fromArgs)
        {
            this.Code = fromArgs;
        }

    }
}
