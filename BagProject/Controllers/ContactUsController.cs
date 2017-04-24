using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BagProject.Controllers
{
    public class ContactUsController : Controller
    {
        public ContactUsController()
        {

        }

        public ViewResult GetPage()
        {
            return View();
        }
    }
}
