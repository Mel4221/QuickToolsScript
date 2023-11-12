using QuickTools.QCore;
using QuickTools.QIO;
using System;

    namespace ErrorHandelers
    {
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
