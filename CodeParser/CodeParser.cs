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
using ClownShell.Init;
using QuickTools.QCore;
using QuickTools.QIO;
using ClownShell.ErrorHandler;
namespace ClownShell
{
    public partial class CodeParser
    {
        public class CodeResult {
            public bool IsValid { get; set; } = false;
            public string Code { get; set; } = null; 
        }

        
        public CodeResult CheckFile(string file)
        {
            string content;
            if (!File.Exists(file))
            {
                return new CodeResult() { IsValid = false };
            }
            content = Reader.Read(file);
            if(content != null || content != "")
            {
                return new CodeResult()
                {
                    IsValid = true,
                    Code = content
                };
            }
            else
            {
                return new CodeResult() { IsValid = false };
            }

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
                    type = Get.FixPath(this.Code[1]);
                    switch (type)
                    {
                        case "obj":
                        case "object":
                        case "selected-value":
                        case "selected":
                            type = $"@{ShellLoop.SelectedOject}";
                            break;
                        default:
                            
                            break;
                    }
                    
                    this.SetExecution(action, type); 
                    break;
                case CodeType.Complete:
                    action = this.Code[0];
                    type = Get.FixPath(this.Code[1]);
                    switch (type)
                    {
                        case "obj":
                        case "object":
                        case "selected-value":
                        case "selected":
                            type = $"{ShellLoop.SelectedOject}";
                            break;
                            
                    }
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

    }
}
