using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerPrompter.Options
{
    internal partial class OptionsProvider
    {
        [ComVisible(true)]
        public class IssueLinkOptions : BaseOptionPage<IssueLinkOptionsModel>
        {
        }
    }

    public class IssueLinkOptionsModel : BaseOptionModel<IssueLinkOptionsModel>
    {
        [Category("Redmine")]
        [DisplayName("Redmine API root")]
        [Description("An API root of the Redmine link.")]
        [DefaultValue("https://www.redmine.org/projects/redmine/")]
        public string RedmineApiRoot { get; set; } = "https://www.redmine.org/projects/redmine/";

        [Category("Redmine")]
        [DisplayName("Redmine WEB root")]
        [Description("A web root of the Redmine link.")]
        [DefaultValue("https://www.redmine.org/issues/{0}")]
        public string RedmineWebRoot { get; set; } = "https://www.redmine.org/issues/{0}";

        [Category("Redmine")]
        [DisplayName("Redmine API key")]
        [Description("Your API key for Redmine API.")]
        [DefaultValue("")]
        public string RedmineApiKey { get; set; } = "";





        [Category("Gitlab")]
        [DisplayName("Gitlab API root")]
        [Description("An API root of the Gitlab link.")]
        [DefaultValue("https://gitlab.com/api/v4/")]
        public string GitlabApiRoot { get; set; } = "https://gitlab.com/api/v4/";

        [Category("Gitlab")]
        [DisplayName("Gitlab WEB root")]
        [Description("A web root of the gitlab link.")]
        [DefaultValue("https://gitlab.com/gitlab-org/gitlab/-/issues/{0}")]
        public string GitlabWebRoot { get; set; } = "https://gitlab.com/gitlab-org/gitlab/-/issues/{0}";

        [Category("Gitlab")]
        [DisplayName("Gitlab project Id")]
        [Description("Gitlab project Id")]
        [DefaultValue(4)]
        public int GitlabProjectId { get; set; } = 278964;

        [Category("Gitlab")]
        [DisplayName("Gitlab API key")]
        [Description("Your API key for Gitlab API.")]
        [DefaultValue("")]
        public string GitlabApiKey { get; set; } = "";

    }
}
