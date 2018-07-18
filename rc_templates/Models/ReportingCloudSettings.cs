using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rc_templates.Models
{
    public class ReportingCloudSettings
    {
        public ReportingCloudSettings()
        {

        }

        public string APIKey { get; set; }
    }

    public class TemplateRequest
    {
        public string TemplateName { get; set; }
    }

    public class TemplatePostData
    {
        public string Document { get; set; }
        public string Name { get; set; }
    }
}
