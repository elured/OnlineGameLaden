using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineGameLaden.WebUI.Models
{
        public class PagingInfo
        {
        // Artikelmenge
        public int TotalItems { get; set; }

        // Artikelmenge auf einer seite
        public int ItemsPerPage { get; set; }

        //Aktuelle Seite
        public int CurrentPage { get; set; }

        // Gesamte Artikelmenge
        public int TotalPages
            {
                get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
            }
    }
}