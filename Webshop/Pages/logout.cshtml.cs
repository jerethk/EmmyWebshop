using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webshop.Pages
{
    public class logoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Remove("userLoggedIn");
        }
    }
}
