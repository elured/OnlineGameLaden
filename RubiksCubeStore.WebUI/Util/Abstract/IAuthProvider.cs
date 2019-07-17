using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubiksCubeStore.WebUI.Util.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string userName, string password);
    }
}