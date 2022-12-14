using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BugTrackerPrompter.IssueLink.Api.Github;
using BugTrackerPrompter.IssueLink.Api.Gitlab;
using BugTrackerPrompter.IssueLink.Api.Redmine;
using BugTrackerPrompter.IssueLink.Tag;

namespace BugTrackerPrompter.IssueLink.Api
{
    public class IssueSourceApi
    {
        public static readonly IssueSourceApi Instance = new IssueSourceApi();

        public static IIssueSourceApi[] Apis =
            new IIssueSourceApi[]
            {
                RedmineApi.Instance,
                GitlabApi.Instance,
                GithubApi.Instance
            };

        public static Regex[] Regexes
        {
            get
            {
                return Apis.Select(a => a.AdornmentRegex).ToArray();
            }
        }

        public string? BuildIssueUrl(IssueLinkTag tag)
        {
            var api = Apis.FirstOrDefault(api => api.Source == tag.IssueSource);
            if (api == null)
            {
                return null;
            }

            return api.BuildIssueUrl(tag.IssueNumber);
        }

        /// <inheritdoc />
        public Task<IssueInfo?> GetIssueInfoAsync(
            IssueLinkTag tag
            )
        {
            var api = Apis.FirstOrDefault(api => api.Source == tag.IssueSource);
            if(api == null)
            {
                return Task.FromResult((IssueInfo)null);
            }

            return api.GetIssueInfoAsync(tag);
        }
    }
}
