using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rc_templates.Models;
using TXTextControl.ReportingCloud;
using Microsoft.Extensions.Options;

namespace rc_templates.Controllers
{
    public class HomeController : Controller
    {
        private ReportingCloudSettings RCSettings { get; set; }

        public HomeController(ReportingCloudSettings settings)
        {
            RCSettings = settings;
        }

        public IActionResult Index()
        {
            // create a ReportingCloud object with stored API-Key
            ReportingCloud rc = new ReportingCloud(RCSettings.APIKey);

            // return a list of templates
            return View(rc.ListTemplates());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
