using BugTrackerPrompter.IssueLink.Tag;
using BugTrackerPrompter.Options;
using BugTrackerPrompter.Support;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BugTrackerPrompter.IssueLink.Api.Github
{
    public class GithubApi : IIssueSourceApi
    {
        public static readonly GithubApi Instance = new GithubApi();

        public bool IsEnabled => IssueLinkOptionsModel.Instance.GithubEnabled;

        public IssueSourceEnum Source => IssueSourceEnum.Github;

        public string IssueHeader => IssueLinkOptionsModel.Instance.GithubIssueHeader;

        public Regex2 AdornmentRegex => new Regex2(
            new Regex(@"(^|\s|\W)(?'name'" + IssueHeader + @")(?'id'\d{1,7})($|\s|\W)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase),
            () => IsEnabled
            );

        public string BuildIssueUrl(int issueNumber) => string.Format(IssueLinkOptionsModel.Instance.GithubWebRoot, IssueLinkOptionsModel.Instance.GithubUserName, IssueLinkOptionsModel.Instance.GithubRepositoryName, issueNumber);

        public async Task<IssueInfo?> GetIssueInfoAsync(IssueLinkTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            //https://docs.github.com/en/rest/overview/resources-in-the-rest-api?apiVersion=2022-11-28#rate-limiting
            //When using GITHUB_TOKEN, the rate limit is 1,000 requests per hour per repository.
            //For unauthenticated requests, the rate limit allows for up to 60 requests per hour.
            if (!string.IsNullOrEmpty(IssueLinkOptionsModel.Instance.GithubApiKey))
            {
                await Task.Delay(2000);
            }
            else
            {
                await Task.Delay(5000); //technically we need to wait 1 minute here, but such behaviour is unusable
            }

            using var client = new HttpClient();
            if (!string.IsNullOrEmpty(IssueLinkOptionsModel.Instance.GithubApiKey))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IssueLinkOptionsModel.Instance.GithubApiKey);
            }
            client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.json");
            client.DefaultRequestHeaders.Add("User-Agent", "request");
            client.BaseAddress = new Uri($"https://api.github.com/");

            var response = await client.GetAsync(
                $"repos/{IssueLinkOptionsModel.Instance.GithubUserName}/{IssueLinkOptionsModel.Instance.GithubRepositoryName}/issues/{tag.IssueNumber}"
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
                    contentXml.body
                    );
        }
    }
}
