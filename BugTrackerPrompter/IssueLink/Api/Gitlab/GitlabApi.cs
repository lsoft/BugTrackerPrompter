using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BugTrackerPrompter.IssueLink.Tag;
using BugTrackerPrompter.Options;
using System.Text.RegularExpressions;
using BugTrackerPrompter.Support;

namespace BugTrackerPrompter.IssueLink.Api.Gitlab
{
    public class GitlabApi : IIssueSourceApi
    {
        public static readonly GitlabApi Instance = new GitlabApi();

        public bool IsEnabled => IssueLinkOptionsModel.Instance.GitlabEnabled;

        public IssueSourceEnum Source => IssueSourceEnum.Gitlab;

        public string IssueHeader => IssueLinkOptionsModel.Instance.GitlabIssueHeader;

        public Regex2 AdornmentRegex => new Regex2(
            new Regex(@"(^|\s|\W)(?'name'" + IssueHeader + @")(?'id'\d{1,7})($|\s|\W)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase),
            () => IsEnabled
            );

        public string BuildIssueUrl(int issueNumber) => string.Format(IssueLinkOptionsModel.Instance.GitlabWebRoot, issueNumber);

        public async Task<IssueInfo?> GetIssueInfoAsync(IssueLinkTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            using var client = new HttpClient();

            var response = await client.GetAsync(
                $"{IssueLinkOptionsModel.Instance.GitlabApiRoot}projects/{IssueLinkOptionsModel.Instance.GitlabProjectId}/issues/{tag.IssueNumber}?private_token={IssueLinkOptionsModel.Instance.GitlabApiKey}"
                );

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            var contentXml = JsonSerializer.Create().Deserialize<IssueRootObject>(
                new JsonTextReader(
                    new StringReader(
                        content
                        )
                    )
                );

            return
                new IssueInfo(
                    contentXml.title,
                    contentXml.description
                    );
        }
    }
}
