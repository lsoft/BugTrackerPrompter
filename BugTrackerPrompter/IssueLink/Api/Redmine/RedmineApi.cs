using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BugTrackerPrompter.IssueLink.Tag;
using BugTrackerPrompter.Options;

namespace BugTrackerPrompter.IssueLink.Api.Redmine
{
    public class RedmineApi : IIssueSourceApi
    {
        public static readonly RedmineApi Instance = new RedmineApi();

        public async Task<IssueInfo?> GetIssueInfoAsync(IssueLinkTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            using var client = new HttpClient();

            string url;
            if (!string.IsNullOrEmpty(IssueLinkOptionsModel.Instance.RedmineApiKey))
            {
                url = $"{IssueLinkOptionsModel.Instance.RedmineApiRoot}issues.json?issue_id={tag.IssueNumber}&status_id=*&key={IssueLinkOptionsModel.Instance.RedmineApiKey}";
            }
            else
            {
                url = $"{IssueLinkOptionsModel.Instance.RedmineApiRoot}issues.json?issue_id={tag.IssueNumber}&status_id=*";
            }

            var response = await client.GetAsync(
                url
                );

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();

            var contentXml = JsonSerializer.Create().Deserialize<Rootobject>(
                new JsonTextReader(
                    new StringReader(
                        content
                        )
                    )
                );

            var issue = contentXml?.issues.FirstOrDefault();

            if(issue == null)
            {
                return null;
            }
            if (issue.id != tag.IssueNumber)
            {
                return null;
            }

            return
                new IssueInfo(
                    issue.subject,
                    issue.description
                    );
        }
    }
}
