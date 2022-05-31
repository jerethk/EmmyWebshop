using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webshop.Pages
{
    [BindProperties]
    public class ContactModel : PageModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string message { get; set; }

        public string errorMessage { get; set; }

        public void OnGet()
        {
            errorMessage = "";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            errorMessage = "No error";

            try
            {
                WebMail.SmtpServer = "mail.iinet.net.au";
                WebMail.SmtpPort = 587;
                WebMail.From = "jerethk@iinet.net.au";
                WebMail.UserName = "jerethk";
                WebMail.Password = "gROnIC2387gronic";

                string _to = "jerethk@iinet.net.au";
                string _subject = "From Emmy's shop";
                string _body = $"From {this.name} \n Email {this.email} \n Message \n {this.message}";

                WebMail.Send(_to, _subject, _body); 
            }
            catch (Exception e)
            {
                errorMessage = "Error occurred: " + e.Message;
            }

            return Page();
        }
    }
}
