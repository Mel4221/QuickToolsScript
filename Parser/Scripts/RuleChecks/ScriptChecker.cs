using QuickTools.QCore;
using System.Collections.Generic; 
using QuickTools.QIO;
using System.Runtime.CompilerServices;
using System.Text;
using ClownShell.Init; 

namespace ClownShell.Parser.Scripts.RuleChecks
{
    public class ScriptChecker:CodeParser
    { 
        
        //public bool IsInternalFunction(string word)
        //{
        //    switch (word)
        //    {
        //        case "mv":
        //        case "cp":
        //        case "touch":
        //        case "create":
        //        case "clear":
        //        case "input":
        //        case "red":
        //        case "pink":
        //        case "blue":
        //        case "yellow":
        //        case "gray":
        //        case "secure-encrypt":
        //        case "secure-decrypt":
        //            return true;
        //        default:
        //            return false;
        //    }
        
        //public bool IsStoredFunction(string word)
        //{
            
      
        private List<string> CommandLines = new List<string>();
        private List<string> Parser(string code)
        {
            string temp = "";
            for(int c = 0; c < code.Length; c++)
            {
                if (code[c] == this.LineEndingChar)
                {
                    this.CommandLines.Add(temp.Replace("\n",""));
                    temp = ""; 
                }
                if (code[c] != this.LineEndingChar)
                {
                    if(code[c] != ' ')
                    {
                        temp += code[c];
                    }
                    if (code[c] == ' ')
                    {
                      if(code.Length > c +1)
                        {
                            if (code[c+1]  != ' ' && code[c-1] != ' ')
                            {
                                temp+= code[c];
                            }
                        }
                    }
                        
                }
            }
            return this.CommandLines; 
        }
      
     
        public void Check(string[] args)
        {
            if (args.Length == 0) return;
            args = IConvert.TextToArray(@"
                                        green ""Starting..."";
                                        touch ""New_File.txt"";
                                        green ""File_Created_Sucessfully"";
                                        mkdir box; 
                                        touch file.txt;
                                        yellow Copying_File; 
                                        cp file.txt box/file.txt; 
                                        green Task_Completed;
                                        var a = 22; 
                                        var x = 32; 
                                        ");
            string code = IConvert.ArrayToText(args);
            
            this.Parser(code); 
            Print.List(args);
            Get.Yellow("Ussable Lines"); 
            Print.List(this.CommandLines);
            //Get.Ok(3);
            //Get.Yellow(this.CommandLines[1].Replace("\n",""));





            //Print.List(this.Code);

            //Get.Blue($"Length: {this.Code.Length}");
            //Get.Wait(code);
            Get.Yellow("ForEach"); 
            foreach(string line in  this.CommandLines) 
            {
                this.Code = line.Split(' ');//IConvert.TextToArray(line.Replace("\n", ""));
                Print.List(this.Code);
                Get.Blue($"Length: {this.Code.Length}");
                Get.Wait(IConvert.ArrayToText(this.Code)); 
                this.Start();
            }

            Get.Wait(); 


            /*
             
                clear;
                mv file.txt f:file.txt;
                var password = input;
                function run = {
                    green "running a function";
                    sleep 5; 
                }
               run;
                var x = 1,2,3,4,5;
                var names = "melvin argus",mel,melquiceded;
                list bytes = read app.exe;
                const files = (ls > return;);
                
                
                
                function BackUpFiles from,to =
                {
                        cp from* to;
                        green "Task Completed!!!";
                        return -result;
                }
                function Share files[],address = 
                {
                    foreach -line in files[] = 
                    {
                         http post -line address -d;
                    }
                }

                BackUpFiles ~/Documents/ f:/;
                try BackUpFiles ~/Documents/ f:/;
                catch red "Something Went Wrong";
             

             */


        }
    }
}
