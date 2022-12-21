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
        [DisplayName("Redmine bugtracker status")]
        [Description("Enable or disable Redmine issue tracker.")]
        [DefaultValue(false)]
        public bool RedmineEnabled { get; set; } = false;

        [Category("Redmine")]
        [DisplayName("Redmine issue header")]
        [Description("A header for Redmine issue link in your sources.")]
        [DefaultValue("RM-")]
        public string RedmineIssueHeader { get; set; } = "RM-";

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
        [DisplayName("Gitlab bugtracker status")]
        [Description("Enable or disable Gitlab issue tracker.")]
        [DefaultValue(false)]
        public bool GitlabEnabled { get; set; } = true;

        [Category("Gitlab")]
        [DisplayName("Gitlab issue header")]
        [Description("A header for Gitlab issue link in your sources ")]
        [DefaultValue("GL-")]
        public string GitlabIssueHeader { get; set; } = "GL-";

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
        [Description("Your API key for Gitlab API. Keep in as granular as possible.")]
        [DefaultValue("")]
        public string GitlabApiKey { get; set; } = "";



        [Category("Github")]
        [DisplayName("Github bugtracker status")]
        [Description("Enable or disable Github issue tracker.")]
        [DefaultValue(false)]
        public bool GithubEnabled { get; set; } = false;

        [Category("Github")]
        [DisplayName("Github issue header")]
        [Description("A header for Github issue link in your sources ")]
        [DefaultValue("#")]
        public string GithubIssueHeader { get; set; } = "#";


        [Category("Github")]
        [DisplayName("Github WEB root")]
        [Description("A web root of the Github link.")]
        [DefaultValue("https://gitlab.com/gitlab-org/gitlab/-/issues/{0}")]
        [Browsable(false)]
        public string GithubWebRoot { get; set; } = "https://github.com/{0}/{1}/issues/{2}";

        [Category("Github")]
        [DisplayName("Github user name")]
        [Description("Your user name on Github.")]
        [DefaultValue("octocat")]
        public string GithubUserName { get; set; } = "octocat";

        [Category("Github")]
        [DisplayName("Github repository")]
        [Description("Your Github repository name")]
        [DefaultValue("hello-worId")]
        public string GithubRepositoryName { get; set; } = "hello-worId";

        [Category("Github")]
        [DisplayName("Github API key")]
        [Description("Your API key for Github API. Keep in as granular as possible. Github sets a rate limit for unauth requests to the very low value, so it is strongly recommended to use access token!")]
        [DefaultValue("")]
        public string GithubApiKey { get; set; } = "";


    }
}
