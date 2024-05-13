using System.Collections.Generic;
using ErrorHandelers;
using QuickTools.QCore;
using States;

namespace Parser
{
    public partial class CodeParser
    {

        private void Parse(CodeType codeType)
        {
            string action, type;
            string[] param;
            ErrorHandeler error = new ErrorHandeler();

            switch (codeType)
            {
                case CodeType.Action:
                    action = this.Code[0];
                    this.Action = action;
                    this.SetExecution(action);
                    break;
                case CodeType.ActionWithType:
                    action = this.Code[0];
                    type = Get.FixPath(this.Code[1]);
                    this.Action = action;
                    this.Type = type;
                    switch (type)
                    {
                        case "obj":
                        case "object":
                        case "selected-value":
                        case "selected":
                            //error.DisplayError(ErrorType.NotImplemented);
                            type = Shell.SelectedObject == "" ? "NONE" : Shell.SelectedObject;
                            break;
                       
                    }
                    type = this.CheckTypeForVariables(type);
                    //Get.Yellow($"{type}");
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
                            //error.DisplayError(ErrorType.NotImplemented);
                            type = Shell.SelectedObject == null ? "NONE" : Shell.SelectedObject;
                            break;

                    }
                    this.Action = action;
                    this.Type = type;
                    this.Parameters = this.GetParameters(this.Code);
                    //Get.Box($"{Action} {Type} {IConvert.ArrayToText(Parameters)} Lenth: {Parameters.Length}");
                    type = this.CheckTypeForVariables(type);
                    param = this.Parameters;
                    //Get.Box($"{Action} {Type} {IConvert.ArrayToText(param)}");

                    param = this.CheckParamForVariables(param);
                    //Get.Box($"{Action} {Type} {IConvert.ArrayToText(param)} Lenth: {param.Length}");

                    param = this.FormatStrings(param);
                    //Get.Box($"{Action} {Type} {IConvert.ArrayToText(param)}");

                    this.SetExecution(action, type, param);
                    break;
            }
        }
    }
}