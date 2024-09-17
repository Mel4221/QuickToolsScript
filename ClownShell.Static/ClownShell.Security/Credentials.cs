using System;
using QuickTools.QSecurity;
using System.Security.Principal;
using QuickTools.QCore;
using QuickTools.QIO;
using Settings;


namespace Security
{
 
    public class Credentials
    {
        protected bool AllowNoneAdminCredentials { get; set; } = true; 
        public void SingUp()
        {
            bool isAdmin = true; 
            //try
            //{
            WindowsIdentity human = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(human);
            if (!AllowNoneAdminCredentials)
            {
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
                switch (isAdmin)
            {
                case true:
                    string path, userFile, user, password, str; 
                    byte[] pass, random, iv, key; 
                    user = Get.Input("user").Text;
                    path = ShellSettings.Path; 

                    userFile = path+$"{user}.pubkey";
                    if (System.IO.File.Exists(userFile))
                    {
                        Get.Red($"User {user} already exist!!!");
                        return;
                    }
                    random = IRandom.RandomByteArray(16);
                    //Get.Green($"RandomKey: {random}");
                    Secure secure = new Secure();
                    password = Get.Password();
                    pass = Secure.CreatePassword(password);
                    iv = Secure.CreatePassword(user);
                    key = secure.Encrypt(random, pass, iv);
                    str = IConvert.BytesToString(key);
                    Writer.Write(userFile, str); 

                    break;
                case false:
                    Get.Red("To login it is required to run the shell as an admin");
                    break;
            }
        }
       public void Login()
        {
            bool isAdmin = true; 
            //try
            //{
            WindowsIdentity human = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(human);
            if (!AllowNoneAdminCredentials)
            {
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            switch (isAdmin)
            {
                case true:
                    string path, userFile, user, password, str;
                    byte[] pubkey, pass, iv;

                    user = Get.Input("user").Text;
                    path = ShellSettings.Path;
                    userFile = path+$"{user}.pubkey";
                    if (!System.IO.File.Exists(userFile))
                    {
                        Get.Red($"The user '{user}' does not Exist");
                        return; 
                    }

                    pubkey = IConvert.StringToBytesArray(Reader.Read(userFile)); 
                    Secure secure = new Secure();
                    password = Get.Password();
                    pass = Secure.CreatePassword(password);
                    iv = Secure.CreatePassword(user); 
                    try
                    {

                        str =  secure.Decrypt(pubkey, pass, iv);
                        if(str.Length >= 16)
                        {
                            ShellUser.Name = user;
                            ShellUser.PublickKey = pubkey;
                            str = IRandom.RandomText(30); 
                            secure.Dispose();
                            return; 
                        }
                        else
                        {
                            throw new Exception(); 
                        }
                      //  return; 
                    }
                    catch
                    {
                        Get.Red("Invalid username or password"); 
                    }

                    break;
                case false:
                        Get.Red("To login it is required to run the shell as an admin"); 
                    break; 
            }
            ///*    User.UserName = user.Name;*/
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    isAdmin = false;
            //}
            //catch (Exception ex)
            //{
            //    isAdmin = false;
            //}
        }
    }
}
