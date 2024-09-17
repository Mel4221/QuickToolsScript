using System;
using System.IO; 
using System.Collections.Generic;
using System.Threading;
using ErrorHandelers;
using QuickTools.QData;
using ScriptRunner;
using System.Diagnostics;
using States;
using QuickTools.QCore;
using QuickTools.QIO;
using Parser.Types.Functions;
using Parser.Types;
//using QuickTools.QSecurity.FalseIO;
//using System.Runtime;
using QuickTools.QMath;
//using System.Reflection;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public partial class CodeParser
    {

        /*
            The mechanisims that is in charge of transforming the - 
            Variable pointer into it's acual value has to be changed
         */
        public async Task SetExecution(string action,string type, string[] parameters)
        {    
            
            ErrorHandeler error = new ErrorHandeler();
            Runner runner = new Runner();
            //ProcessStartInfo process;
            string[] param = parameters;
            //Print.List(param); 
            ShellTrace.AddTrace($"Execution Started With Action: {action} Type: {type} Parameters: {IConvert.ArrayToText(param)}");
            //string file, path,outFile;

            Get.Green($"{this.Action} {this.Type} {IConvert.ArrayToText(this.Parameters)}");
            Get.Blue($"{action} {type} {IConvert.ArrayToText(param)}");
            /*
//<<<<<<< HEAD
            string path; 
            switch (action)
              {
                case "ls":
                case "list-files":
                    runner.Run(() => {
                        path = param[0]; 
                        /*
                        if (this.IsRootPath(path))
                        {
                            Get.Ls(type,null);
                            return;
                        }
       
                        if (type == "-l")
                        {
                            Get.Ls(path, null);
                            return;
                        }


                        if (type == ".")
                        {
                            Get.Ls(Shell.CurrentPath);
                            return;
                        }

                        if (Directory.Exists(this.GetPathWithType(type)))
                        {
                            Get.Ls(this.GetPathWithType(type));
                            return;
                        }
                        if (Helper.HasSpecialFolder(type) != null)
                        {
                            Get.Ls(Helper.HasSpecialFolder(type));
                            return;
                        }
                        if (type.Contains(".") && type.Length >= 2)
                        {
                            //path = ShellLoop.CurrentPath[ShellLoop.CurrentPath.Length-1]==Get.Slash()[0] ? $"{ShellLoop.CurrentPath}{type}" : $"{ShellLoop.CurrentPath}{Get.Slash()}{type}";
                            Get.Ls(this.GetPathWithType(type));
                            return;
                        }
                        else
                        {
                            Get.Ls(type);
                        }


                        //Get.Yellow($"{this.Target}     ClearTarget: {this.SubTarget} Type: {type}");
                        //Get.Blue(Path.GetDirectoryName(this.Target));
                        // Get.Yellow(ShellLoop.RelativePath);
                        // Get.Wait(type);
                        //this.SubTarget = type; 
                        //CodeParser helper = Helper.ResolvePath(this);
                        //this.Target = helper.Target;
                        //this.SubTarget = helper.SubTarget;
                        //Get.Cyan($"Target: {this.Target} SubTarget: {this.SubTarget}");

                        //if(type == "-l")
                        //{
                        //    Get.Ls(helper.Target, "");
                        //    return; 
                        //}
                        //if (type.Contains('*'))
                        //{
                        //    //Get.Wait($"{this.Target.Substring(0,this.Target.LastIndexOf("*"))} {type.Substring(1)}");
                        //    // Get.Wait(type);  //Get.FileExention(type)
                        //    //Get.Wait(this.Target.Substring(this.Target.LastIndexOf(Get.Slash())));
                        //    //this get all the files that has this given type 
                        //    Get.Ls(this.Target,this.SubTarget,true);
                        //    return;
                        //}
                        //else
                        //{
                        //    if (Directory.Exists(this.Target))
                        //    {
                        //        Get.Ls(this.Target);
                        //        return;
                        //    }

                        //}
                    });
                    break;
                case "status":
                case "porcent":
                    runner.Run(() => {
                        if (!Get.IsNumber(type) || !Get.IsNumber(parameters[0]))
                            {
                                error.DisplayError(ErrorType.NotValidParameter);
                                 return;
                            }
                            Get.Green(Get.Status(type, parameters[0]));
                    });
                    break;
                case "install":
                    error.DisplayError(ErrorType.NotImplemented);
=======
*/
            string fileArg, path;
            switch (action)
            {
                case "@":
                case "task":
                    runner.Run(() => 
                    {
                        //@ sleep 5000
                        //Print.List(this.Code);
                        /*
                         string[] code = new string[this.Code.Length - 1];
                         int index = 0; 
                         for(int item = 1; item < this.Code.Length; item++)
                         {
                             code[index] = this.Code[item];
                             index++;
                         }
                         */
                        //Print.List(param);
                        //this.Call(code); 
                        //this.Call(type, param[0], param);
                    },true);
                    break;
                case "current-shell-path":
                case "pwd":
                    runner.Run(() => {
                        Shell.CurrentPath = param[0];
                    });
                    break;
                case "clown":
                    runner.Run(() =>
                    {
                        switch (type)
                        {
                            case "install":
                            case "-i":
                                break;
                            case "update":
                            case "-up":
                                break;
                            case "uninstall":
                            case "-un":
                                break;
                            default:
                                error.DisplayError(ErrorType.NotValidParameter, $"'{type}' IS NOT A VALID PARAMETER FOR THE ACTION [{action}]");
                                return;
                        }
                    });
                    break;
                case "path":
                    runner.Run(() => {
                        path = param[0];
                        if(Directory.Exists(path))
                        {
                            ShellTrace.AddTrace($"Shell  Currentpath set to [{path}]");
                            Shell.CurrentPath = path;
                            Get.Yellow($"Current Path set to [{path}]");
                            return;
                        }
                        path = Helper.HasSpecialFolder(path);
                        if (path != "")
                        {
                            if(Directory.Exists(path))
                            {
                                ShellTrace.AddTrace($"Shell  Currentpath set to [{path}]");
                                Shell.CurrentPath = path;
                                return;
                            }

                        }
                        Get.Red($"The given path was not a valid Path: {path}");

                    });
                    break;
                case "zero-file":
                    runner.Run(() => {
                        //zero-file file.zero 10
                        string file = type;
                        if(Helper.HasSpecialFolder(file)!="")
                        {
                            file = Helper.HasSpecialFolder(file);
                        }
                        if (!this.IsRootPath(file))
                        {
                            file = this.GetPathWithType(type); 
                        }
                        if(!Get.IsNumber(param[0]))
                        {
                             error.DisplayError(ErrorType.NotValidParameter, $"Number Expected at: {action} {type} '{param[0]}'");
                             return;
                        }
                        Binary.CreateZeroFile(file, int.Parse(param[0]));
                    });
                    break;
                case "search":
                case "find":
                    runner.Run(() =>
                    {
                        //

                        path = Get.FolderFromPath(param[0]);
                        if(!this.IsRootPath(path))
                        {
                            /*
                            if(Directory.Exists(this.BindWithPath(Shell.CurrentPath,p)))
                            {
                               // path = 
                            }
                            */
                        }
                            FilesMaper maper = new FilesMaper(path);
                        maper.AllowDebugger = true;
                        maper.Map();
                        foreach(string file in maper.Files)
                        {
                            if (Get.FileExention(file) == Get.FileExention(param[0]))
                            {
                                Get.Yellow(file);
                            }
                        }
                    });
                    break;
                case "zip":
                    runner.Run(() => 
                    {

                        QZip zip;
                        string file; 
                        //zip -f file.txt
                        switch(type)
                        {
                            case "-f":
                            case "-F":
                            case "file":
                                zip = new QZip();
                                zip.AllowDebugger = true; 
                                file = param[0];
                                if (!File.Exists(file)) throw new FileNotFoundException($"File not found at: {file}");
                                zip.Compress(file);
                                break;
                            case "-a":
                            case "-A":
                            case "archive":
                                zip = new QZip();
                                path = param[0];
                                if (!Directory.Exists(path)) throw new DirectoryNotFoundException($"Directory not found at: {path}");
                                FilesMaper maper = new FilesMaper(path);
                                maper.AllowDebugger = true;
                                zip.AllowDebugger = true; 
                                maper.Map();
                                string[] files = maper.Files.ToArray();
                                zip.Zip(path, files);
                                break;
                        }
                    });

                    break;
                case "cp":
                    runner.Run(() => 
                    {
                        //cp folder/*.txt  e:/f/l/s/
                        //cp ~/Desktop/f.c ~/Documents/file.c
                        //_____________________________

                        //cp     folder/   box/
                        //action type      param[0]
                        //______________________________

                        //cp     c:/f/a/f.txt  e:/d/4/4/4//f.txt
                        //cp     file.txt    ../file.txt
                        //action type        param[0]
                        //______________________________
                        string source, destination;
                        source = type;
                        destination = param[0];
                        FilesTransferer transfer = new FilesTransferer();

                        switch (param.Length)
                        {
                            case 3:
                                switch (param[1])
                                {
                                    case "--read-write":
                                    case "-rw":
                                        int rw = 0;
                                        try
                                        {
                                            rw = IConvert.ToDataSize(param[2]);
                                        }
                                        catch 
                                        {
                                            error.DisplayError(ErrorType.NotValidParameter, $"NOT VALID FORMAT FOR THE -rw flag EXPECTED B,KB,MB,GB {param[2]} \nEXAMPLES: 1024B,1024KB,1024MB,1GB");
                                            return; 
                                        }
                                        if (File.Exists(source) && this.IsRootPath(source) && this.IsRootPath(destination))
                                        {
                                            transfer.AllowDebugger = true;
                                            transfer.ChuckSize = rw;
                                            //transfer.CheckFileIntegrity = true;
                                            transfer.TransferFile(source, destination);
                                            return;
                                        }
                                        else
                                        {
                                            error.DisplayError(ErrorType.NotImplemented, $"As a right now only ROOT PATH ALLOWED");
                                            return;
                                        }
                                    default:
                                        error.DisplayError(ErrorType.NotValidParameter, $"This does not look like a valid paramter: [{param[1]}]");
                                        break;
                                }
                                break;
                            case 2:
                                switch (param[1])
                                {
                                    case "--check-integrity":
                                    case "-c":
                                        if (File.Exists(source) && this.IsRootPath(source) && this.IsRootPath(destination))
                                        {
                                            transfer.AllowDebugger = true;
                                            transfer.CheckFileIntegrity = true;
                                            transfer.TransferFile(source, destination);
                                            return;
                                        }
                                        else
                                        {
                                            error.DisplayError(ErrorType.NotImplemented, $"As a right now only ROOT PATH ALLOWD");
                                            return;
                                        }
                                    default:
                                        error.DisplayError(ErrorType.NotValidParameter, $"The Parameter [{param[1]}] is not a valid parameter");
                                        break;
                                }
                                break;
                            default:
                                if (File.Exists(source) && this.IsRootPath(source) && this.IsRootPath(destination))
                                {
                                    transfer.AllowDebugger = true;
                                    transfer.CheckFileIntegrity = false;
                                    //transfer.WaitToAcknolegeTransfer = false; 
                                    transfer.TransferFile(source, destination);
                                    return;
                                }
                                else
                                {
                                    error.DisplayError(ErrorType.NotImplemented, $"As a right now only ROOT PATH ALLOWD");
                                    return;
                                }
                                //break;
                        }

                    });
                    break;
                case "shread":
                case "fdelete":
                    runner.Run(() => 
                    {
                        //fdelete -f file.txt 
                        //fdelete -r path/
                        //action type param[0]
                        FileShreder shreader;
                        ShellTrace.AddTrace("Shreading file initilize");
                        switch(type)
                        {
                            case "-f":               
 
                                ShellTrace.AddTrace($"Shreadding File Comfirmed");
                                    shreader = new FileShreder(param);
                                    shreader.AllowDebugger = true;
                                    shreader.Shread();
                                break;
                            case "-r":

                                    path = param[0];

                                    if(!this.IsRootPath(path)) 
                                    {
                                        if(Directory.Exists(this.GetPathWithType(path)))
                                        {
                                            path = this.GetPathWithType(path);
                                        }
                                    }
                                     
                                    if(!Directory.Exists(path))
                                    {
                                        Get.Red($"The Directory Does not exist or was not found {path}");
                                        return;
                                    }
                                    FilesMaper maper = new FilesMaper();
                                    maper.AllowDebugger = true;
                                    maper.Path = path;
                                    if(param.Length >= 4)
                                    {
                                        if(param[3] == "-s")
                                        {
                                            maper.AllowDebugger = false; 
                                        }
                                    }
                                    maper.Map();
                                    if (maper.Files.Count > 0)
                                    {
                                        shreader = new FileShreder();
                                        shreader.Files = maper.Files.ToArray();
                                        shreader.AllowDebugger = maper.AllowDebugger;
                                        shreader.Shread();
                                    }

          
                                    for(int dir = maper.Directories.Count - 1; dir > 0; dir --)
                                    {
                                        GC.Collect();
                                    try
                                    {
                                        Directory.Delete(maper.Directories[dir]);
                                    }
                                    catch { Get.Red($"Fail To Delete: {maper.Directories[dir]}"); }
                                }
                                //  maper.Directories.ForEach((directory) => {
                                        //GC.Collect();
                                //    });
                                    if(Directory.Exists(path))
                                    {
                                        Directory.Delete(path);
                                    }
                                break;
                        }
                    });
                    break;
                case "math":
                case "cal":
                    runner.Run(() => {
                        QMath math = new QMath();
                        string[] args = new string[param.Length+1];
                        int len, l;
                        len = param.Length;
                        l =0;
                        for(int ar = 0; ar < param.Length; ar++) 
                        {
                            if(ar == 0)
                            {
                                args[0] = type;
                            }
                            if(ar != 0)
                            {
                             args[ar] = param[l];
                                l++;
                            }
                        }

                        string input = IConvert.ArrayToText(args);
                        math.Parse(input);// action type param[0] 
                    });
                    break;
                case "minidb":

                    //Action Type param[0]
                    runner.Run(() =>
                    {
                    string db;
                    switch (type)
                    {
                        case "init":
                        case "start":
                            Shell.MiniDB = new MiniDB();
                            Get.Ok();
                            break;
                        case "unload":
                            Shell.MiniDB.Dispose();
                            Get.Ok();
                            break;
                        case "load":
                            if (File.Exists(this.GetPathWithType(param[0])))
                            {
                                param[0] = this.GetPathWithType(param[0]);
                                Get.White($"PATH: {param[0]}");
                            }
                            Shell.MiniDB = new MiniDB();
                            Shell.MiniDB.AllowDebugger = true;
                            bool loaded = Shell.MiniDB.Load(param[0]);
                            if (loaded) Get.Ok();
                            if (!loaded) Get.Red("FAILED TO LOAD");


                            break;
                        case "create":
                            Shell.MiniDB = new MiniDB();
                            db = Get.FileExention(param[0]) == "db" ? param[0] : param[0]+".db";
                            Shell.MiniDB.Create(db);
                            Get.White($"{db} CREATED SUCESSFULLY!!!");
                            break;
                        case "drop":
                            Shell.MiniDB = new MiniDB();
                            db = Get.FileExention(param[0]) == "db" ? param[0] : param[0]+".db";
                            Shell.MiniDB.Drop(db);
                            Get.White($"{db} DELETED SUCESSFULLY!!!");
                            break;
                        case "addHot":
                        case "addOnHot":
                            try
                            {
                                Shell.MiniDB.AddKeyOnHot(param[0], param[1], param[2]);
                                Get.White($"ADDED ON HOT: {Shell.MiniDB.DataBase[Shell.MiniDB.DataBase.Count -1].ToString()}");
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                error.DisplayError(ErrorType.ExecutionError, $"minidb only expect 3 Parameters: [Key] [Value] [Relation] \n So your code should look like this: \n[ minidb add keyName keyValue keyRelation ] \n{ex.Message}");
                                return;
                            }
                            catch (NullReferenceException ex)
                            {
                                error.DisplayError(ErrorType.ExecutionError, $"minidb not started or not loaded\n{ex.Message}");
                                return;
                            }
                            catch (Exception ex)
                            {
                                error.DisplayError(ErrorType.ExecutionError, $"There was an error not identified more info: \n{ex}");
                                return;
                            }
                            break;
                        case "add":
                            try
                            {
                                Shell.MiniDB.AddKey(param[0], param[1], param[2]);
                                Get.White($"ADDED: {Shell.MiniDB.DataBase[Shell.MiniDB.DataBase.Count -1].ToString()}");
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                error.DisplayError(ErrorType.ExecutionError, $"minidb only expect 3 Parameters: [Key] [Value] [Relation] \n So your code should look like this: \n[ minidb add keyName keyValue keyRelation ] \n{ex.Message}");
                                return;
                            } catch (NullReferenceException ex)
                            {
                                error.DisplayError(ErrorType.ExecutionError, $"minidb not started or not loaded\n{ex.Message}");
                                return;
                            }
                            catch (Exception ex)
                            {
                                error.DisplayError(ErrorType.ExecutionError, $"There was an error not identified more info: \n{ex}");
                                return;
                            }
                            break;
                        case "remove":
                            switch (param[0])
                            {
                                case "where-key=":
                                    Shell.MiniDB.RemoveAllByKey(param[1]);
                                    Get.Red($"DELETED WHERE KEY = {param[1]}");
                                    break;
                                case "where-id=":
                                    Shell.MiniDB.RemoveAllbyId(int.Parse(param[1]));
                                    Get.Red($"DELETED WHERE ID = {param[1]}");

                                    break;
                                case "where-relation=":
                                    Shell.MiniDB.RemoveAllByRelation(param[1]);
                                    Get.Red($"DELETED WHERE RELATION = {param[1]}");
                                    break;
                            }
                            break;
                        case "update":
                            switch (param[0])
                            {
                                case "where-key=":
                                    for (int item = 0; item < Shell.MiniDB.DataBase.Count; item++)
                                    {
                                        if (Shell.MiniDB.DataBase[item].Key == param[1])
                                        {
                                            if (param[2] != "?")
                                            {
                                                Shell.MiniDB.DataBase[item].Key = param[2];
                                                Get.Yellow($"UPDATED KEY = {param[2]}");
                                            }
                                            if (param[3] != "?")
                                            {
                                                Shell.MiniDB.DataBase[item].Value = param[3];
                                                Get.Yellow($"UPDATED VALUE = {param[3]}");
                                            }
                                            if (param[4] != "?")
                                            {
                                                Shell.MiniDB.DataBase[item].Relation = param[4];
                                                Get.Yellow($"UPDATED RELATION = {param[4]}");
                                            }
                                            Get.Yellow($"UPDATED WHERE KEY = {param[1]}");
                                        }
                                    }
                                    break;
                                case "where-id=":
                                    for (int item = 0; item < Shell.MiniDB.DataBase.Count; item++)
                                    {
                                        if (Shell.MiniDB.DataBase[item].Id == int.Parse(param[1]))
                                        {
                                            if (param[2] != "?")
                                            {
                                                Shell.MiniDB.DataBase[item].Key = param[2];
                                                Get.Yellow($"UPDATED KEY = {param[2]}");

                                            }
                                            if (param[3] != "?")
                                            {
                                                Shell.MiniDB.DataBase[item].Value = param[3];
                                                Get.Yellow($"UPDATED VALUE = {param[3]}");

                                            }
                                            if (param[4] != "?")
                                            {
                                                Shell.MiniDB.DataBase[item].Relation = param[4];
                                                Get.Yellow($"UPDATED RELATION = {param[4]}");
                                            }
                                            Get.Yellow($"UPDATED WHERE ID = {param[1]}");


                                        }
                                    }
                                    break;
                                case "where-relation=":
                                    for (int item = 0; item < Shell.MiniDB.DataBase.Count; item++)
                                    {
                                        if (Shell.MiniDB.DataBase[item].Relation == param[3])
                                        {
                                            if (param[1] != "?")
                                            {
                                                Shell.MiniDB.DataBase[item].Key = param[1];
                                                Get.Yellow($"UPDATED KEY = {param[1]}");

                                            }
                                            if (param[2] != "?")
                                            {
                                                Shell.MiniDB.DataBase[item].Value = param[2];
                                                Get.Yellow($"UPDATED VALUE = {param[2]}");

                                            }
                                            if (param[3] != "?")
                                            {
                                                Shell.MiniDB.DataBase[item].Relation = param[3];
                                                Get.Yellow($"UPDATED RELATION = {param[3]}");

                                            }
                                            Get.Yellow($"UPDATED WHERE RELATION = {param[0]}");
                                        }
                                    }
                                    break;
                            }
                            break;
                        case "select":
                            //string format = null;
                            
                            switch (param[0])
                            {
                                case "where-key=":
                                    switch (param.Length)
                                    {
                                        case 2:
                                             Shell.MiniDB.SelecAlltWhereKey(param[1]).ForEach(item => Get.White(item.ToString()));
                                            Get.Green($"SELECTED WHERE KEY = {param[1]}");
                                            break;
                                        case 3:
                                            Shell.MiniDB.SelecAlltWhereKey(param[1]).ForEach(item => Get.White(item.ToString(param[2])));
                                            Get.Green($"SELECTED WHERE KEY = {param[1]}");
                                            break;
                                        case 5:
                                            StringBuilder buffer = new StringBuilder();
                                            Shell.MiniDB.SelecAlltWhereKey(param[1]).ForEach((item) => {

                                                Get.White(item.ToString(param[2]));
                                                buffer.Append(item.ToString(param[2])+"\n");
                                            });
                                            fileArg = param[4];
                                            if (!this.IsRootPath(fileArg))
                                            {
                                                fileArg = this.GetPathWithType(fileArg);
                                            }
                                            Writer.Write(fileArg, buffer.ToString());
                                            Get.Green($"SELECTED WHERE KEY = {param[1]}");
                                            break;
                                    }
                                    break;
                                case "where-id=":
                                    switch (param.Length)
                                    {
                                        case 2:
                                                Get.Yellow(Shell.MiniDB.SelectWhereId(int.Parse(param[1])).ToString());
                                                Get.Green($"SELECTED WHERE KEY = {param[1]}");
                                                break;
                                            case 3:
                                                Shell.MiniDB.SelecAlltWhereKey(param[1]).ForEach(item => Get.White(item.ToString(param[2])));
                                                Get.Green($"SELECTED WHERE KEY = {param[1]}");
                                                break;
                                            case 5:
                                                StringBuilder buffer = new StringBuilder();
                                                Shell.MiniDB.SelecAlltWhereKey(param[1]).ForEach((item) => {

                                                    Get.White(item.ToString(param[2]));
                                                    buffer.Append(item.ToString(param[2])+"\n");
                                                });
                                                fileArg = param[4];
                                                if (!this.IsRootPath(fileArg))
                                                {
                                                    fileArg = this.GetPathWithType(fileArg);
                                                }
                                                Writer.Write(fileArg, buffer.ToString());
                                                Get.Green($"SELECTED WHERE KEY = {param[1]}");
                                                break;
                                        }
                                        break;
                                        case "where-value":
                                            break;
                                    }
                                break;
                            case "save":
                            case "save-changes":
                            case "saveChanges":
                                Shell.MiniDB.SaveChanges();
                                break;
                            default:
                                error.DisplayError(ErrorType.NotValidType, $"Not Valid type error at: {action} '{type}'");
                                break;
                        }
                    }); 
                        break;
                case "install":
                    runner.Run(() =>
                    {
                        error.DisplayError(ErrorType.NotImplemented);
                    }); 
//>>//> bb5d32b1d913ce6e91deed6fddcc01f141952ba8
                    break;
                case "trojan":
                    runner.Run(() => {
                        error.DisplayError(ErrorType.NotImplemented);

                        /*
                trojan file.txt 
                pack
                unpack


                    */

                        //Trojan trojan;
                        //string payload;
                        /*
                        file = null;
                        outFile = null;
                        path = param[0]; 
                        if(this.IsRootPath(path))
                        {
                            file = path;
                        }if(file == null)
                        {
                            file = this.GetPathWithType(path); 
                        }if(!File.Exists(file))
                        {
                            error.DisplayError(ErrorType.NotValidType, $"The file was not found: {file}");
                            return;
                        }if (param.Length == 3)
                        {

                            if (this.IsRootPath(param[2]))
                            {
                                outFile = param[2];
                            }
                            if (outFile == null){
                                outFile = this.GetPathWithType(param[2]);    
                            }
                        }

                        //trojan pack vide.mp4 > file.txt 
                        switch (type)
                        {
                            case "pack":
                            case "-p":
                                error.DisplayError(ErrorType.NotImplemented);
                                break;
                            case "unpack":
                            case "-u":
                                error.DisplayError(ErrorType.NotImplemented);
                                break;
                            case "info":
                            case "-i":
                                error.DisplayError(ErrorType.NotImplemented);
                                break;
                            default:
                                error.DisplayError(ErrorType.NotValidParameter);    
                                break;
                        }*/
                    });
                    break;
                case "int":
                case "long":
                case "double":
                case "float":
                    runner.Run(() => {
                        bool isNumber;
                        double number;
                        isNumber = double.TryParse(param[1],out number);
                        if(!isNumber && number < double.MaxValue && number > double.MinValue)
                        {
                            ShellTrace.AddTrace($"The Parameter '{param[1]}' was not recognized as a valid number");
                            error.DisplayError(ErrorType.NotValidParameter, $"Invalid Parameter: {ShellTrace.GetTrace()}");
                            return;
                        }
                        Shell.VStack.SetVariable(new Variable()
                        {
                            Name=type,
                            Value=param[1],
                            IsEmpty=false
                        });
                    });
                    break;
                case "list":
                case "array":
                    // [] = {};
                    runner.Run(() => {
                        //$pwd = /home/m/l/m/f
                        //list --files files = { file.txt , file.exe , Program.exe , file.xml , file.mp4 }
                        //list --files files = { $(pwd)file.txt , $(pwd)file.exe , $(pwd)Program.exe , $(pwd)file.xml , $(pwd)file.mp4 }
                        if (param[0] != "=")
                        {
                            error.DisplayError(ErrorType.InvalidOperator,$"{action} {type} '{param[0]}'");
                            return;
                        }
                        if (type == "--Files" ||
                            type == "-F"||
                            type == "f"
                            )
                        {
                            Get.Yellow($"Checking Files...");
                            //fil
                        }
                    });
                    break;
                case "shell-path":
                    runner.Run(() => {
                        Shell.CurrentPath = param[0];
                    });
                    break;
                case "var":
                    runner.Run(() => {
                        //Get.Yellow($"{type} = {param[1]};");
                        //var y = ls;
                        //var x = input;
                        //action  type
                        //shell-path = d:/path/folder/
                        //var     user = "melquiceded balbi villanueva"
                        Shell.VStack.SetVariable(new Variable(){
                            Name=type,
                            Value=param[1],
                            IsEmpty=false
                        });    
                    });
                    break;
                case "const":
                    runner.Run(() => {
                        //Get.Yellow($"{type} = {param[1]};");
                        //var y = ls;
                        //var x = input;
                        //action  type     
                        //var     user = "melquiceded balbi villanueva"
                        Shell.VStack.SetVariable(new Variable()
                        {
                            Name=type,
                            Value=param[1],
                            IsEmpty=false,
                            IsConstant = true
                        });
                    });
                    break;
                case "input":
                    runner.Run(() => {
                        //input => 
                        //input = variable
                        //action type param[0]

                        if(Shell.VStack.Exist(new Variable() {Name = param[0] }))
                        {
                            error.DisplayError(ErrorType.VariableAlreadySet, $"Variable already Set at {action} {type} '{param[0]}'");
                            return;
                        }
                        Shell.VStack.SetVariable(new Variable()
                        {
                            Name = param[0],
                            Value = Get.Input().Text,
                            IsConstant = false,
                            IsEmpty = false
                        });
                    });
                    break;
                case "rm":
                    runner.Run(() =>
                    {
                        //rm -r *
                        FilesMaper maper;
                        List<Error> errors = new List<Error>(); 
                        string[] files;
                        string[] dirs; 
                        if (type == "-r")
                        {
                            if (true)
                            //if(param[0][0] == '*')
                            {
                                //maper = new FilesMaper(Shell.CurrentPath);
                                maper = new FilesMaper(param[0]);

                                maper.AllowDebugger = true; 
                                maper.Map(); 
                                files = maper.Files.ToArray();
                                dirs = maper.Directories.ToArray();

                                
                             
                                for(int file = files.Length; file  > 0; file--)
                                {
                                    if(File.Exists(files[file - 1]))
                                    {
                                        try
                                        {
                                            File.SetAttributes(files[file-1], FileAttributes.Archive); 
                                            File.Delete(files[file - 1]);
                                            Get.Red($"FILE DELETED: {files[file - 1]}");
                                        }catch(Exception ex)
                                        {
                                            errors.Add(new Error()
                                            {
                                                Type = $"FailToDeleteFile: {files[file -1]}",
                                                Message = ex.Message
                                            });
                                        }
                                    }
                                }
                                    
                                for (int dir = dirs.Length; dir  > 0; dir--)
                                    {
                                        if (Directory.Exists(dirs[dir - 1]))
                                        {
                                            try
                                            {
                                            DirectoryInfo info = new DirectoryInfo(dirs[dir-1]);
                                            info.Delete(true);
                                            //    Directory.Delete(dirs[dir - 1]);
                                                Get.Blue($"DIR DELETED: {dirs[dir - 1]}");
                                            }catch(Exception ex)
                                            {
                                                errors.Add(new Error() { 
                                                    Type = $"FailToDeleteDirectory: {dirs[dir-1]}",
                                                    Message = ex.Message
                                                });
                                            }
                                        }
                                    }
                                    foreach (Error err in errors) Get.Yellow(err.ToString());
                            }

                        }
                    });
                    break;
                case "echo":
                    //Get.Wait($"{this.IsRootPath(type)} {this.IsRootPath(param[1])}");
                    runner.Run(() => {
                        // Get.Wait($"{this.GetPathWithType(param[1])}");

                        //echo "this text" > file.txt
                        //Print.List(param); 

                        if (this.IsRootPath(type) && this.IsRootPath(param[1]))
                        {
                            //Get.Wait("Rooted");
                            Binary.CopyBinaryFile(type, param[1]);

                            return;
                        }
                        else
                        {

                            //Get.Yellow($"{this.GetPathWithType(param[1])} > {type}");
                            string str = type.Replace('"'.ToString(), "");
                            Writer.Write(this.GetPathWithType(param[1]), str);
                            Get.Yellow($"{type} > {param[1]} {Get.FileSize(Get.Bytes(str))}");
                        }
                    });
                    break;
                default:
                    //name = "new value"
                    //number = 200
                    //number = "829-978-2244"
                    //2 / 4
                    //var * 7.
                    //x = var * 7
                    //$x = $b
                    //var x = 2
                    //*3 / 2
                    //b = x
                    //var name = value
                    //echo *name 
                    //free name
                    
                    //Get.Blue($"{this.Action.Substring(1)} {CodeTypes.IsVariable(this.Action.Substring(1))}");
                    if(CodeTypes.IsVariable(this.Action.Substring(1)))
                    {
                        CodeTypes types = new CodeTypes(this.Action, this.Type, this.Parameters);
                        types.RunAssingment();
                        return; 
                    }if(Functions.IsFunction(action))
                    {
                        error.DisplayError(ErrorType.NotImplemented);
                        return;
                    }
                    
                    ShellTrace.AddTrace($"Action Was not Recognized as a valid Action");
                    error.DisplayError(ErrorType.NotValidAction, $"At: Execution Action With Type and parameters '{action}' {type} {IConvert.ArrayToText(param)}Trace: \n{ShellTrace.GetTrace()}");

                    break; 
              }
        }
    }
}
