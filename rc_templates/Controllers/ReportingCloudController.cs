using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rc_templates.Models;
using TXTextControl.ReportingCloud;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;

namespace rc_templates.Controllers
{
    public class ReportingCloudController : Controller
    {
        private ReportingCloudSettings RCSettings { get; set; }

        public ReportingCloudController(ReportingCloudSettings settings)
        {
            RCSettings = settings;
        }

        [HttpGet]
        public IActionResult Template([FromQuery] TemplateRequest templateRequest)
        {
            // create a ReportingCloud object with stored API-Key
            ReportingCloud rc = new ReportingCloud(RCSettings.APIKey);

            // download document from ReportingCloud template storage
            byte[] document = rc.DownloadTemplate(templateRequest.TemplateName);

            // return Base64 string version
            return new OkObjectResult(Convert.ToBase64String(document));
        }

        [HttpPost]
        public IActionResult Template([FromBody] TemplatePostData templatePostData)
        {
            // create a ReportingCloud object with stored API-Key
            ReportingCloud rc = new ReportingCloud(RCSettings.APIKey);

            // upload posted document
            rc.UploadTemplate(templatePostData.Name, Convert.FromBase64String(templatePostData.Document));

            return new OkResult();
        }
    }
}