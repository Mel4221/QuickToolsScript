using System;
using System.IO;
using QuickTools.QCore; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser.Types.Functions;
using States;
namespace Parser
{
	public partial class CodeParser
	{ 

	     public void ParseFromFile(string action)
		 {
 			string file = action; 
			//if c:/file.txt
			if(!this.IsRootPath(file))
			{
				    Get.Blue(file); 
					if(File.Exists(this.GetPathWithType(file)))
					{
						file = this.GetPathWithType(file);
					Get.Yellow(file); 
					}
					if(Helper.HasSpecialFolder(file) != null) 
					{
						file = Helper.HasSpecialFolder(file);
				    Get.Red(file);
						if(!File.Exists(file))
						{
							Get.Red($"The File '{file}' was not found or does not exist!!!");
                            return;
						}
					}
			}
			if(File.Exists(file))
			{
                /*
                    public class user
                    user.name = melquiceded
                    
                                    
                    f()
                    {
                        echo working...                   
                    }
                    loop()
                    {
                           f()                
                    }                   

                    input()
                    {
                        input > in
                        return in                       
                    }
                                      
                */
                ShellTrace.AddTrace($"Parsing code from File: {file}");
				string[] code = File.ReadAllLines(file);
				//Print.List(code);
				CodeParser parser;
				foreach (string cmd in code)
				{
                    switch(Functions.IsFunction(cmd))
                    {
                        case true:
                            break;
                        case false:
                            parser = new CodeParser(IConvert.TextToArray(cmd));
                            parser.Start();
                            break;
                    }

				}
				return;
			}
			else
			{
				Get.Red($"The File '{file}' was not found or does not exist!!!");
				return;
			}


		}
	}
}
