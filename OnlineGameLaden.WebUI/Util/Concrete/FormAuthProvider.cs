﻿using OnlineGameLaden.WebUI.Util.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OnlineGameLaden.WebUI.Util.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        public bool Authenticate(string userName, string password)
        {
            bool result = FormsAuthentication.Authenticate(userName, password);
            if (result)
                FormsAuthentication.SetAuthCookie(userName, false);
            return result;
        }
    }
}