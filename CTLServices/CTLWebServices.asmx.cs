using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BussinessLogicLayer;
using System.Data;
using DataAccessLogicLayer;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Newtonsoft.Json;
using System.Configuration;
using System.Timers;

namespace CTLServices
{
    /// <summary>
    /// Summary description for CTLWebServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class CTLWebServices : System.Web.Services.WebService
    {
        #region LoginServices
        [WebMethod(Description = "Get Login Access Details", EnableSession = true)]
        public string GetLoginAccessDetails(string UserName)
        {
            LoginServices dsh = new LoginServices();
            return dsh.GetLoginAccessDetails(UserName);
        }

        [WebMethod(Description = "Get Login Access Details", EnableSession = true)]
        public string GetLoginAccessName()
        {
            LoginServices dsh = new LoginServices();
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            string name = HttpContext.Current.Request.LogonUserIdentity.Name.ToString();
            String[] breakApart = name.Split('\\');
            //name = user + name + System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return breakApart[1].ToString();
        }
        #endregion
    }
}
