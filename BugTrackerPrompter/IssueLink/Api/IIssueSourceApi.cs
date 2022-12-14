using System;
using System.Threading.Tasks;
using BugTrackerPrompter.IssueLink.Api.Github;
using BugTrackerPrompter.IssueLink.Api.Gitlab;
using BugTrackerPrompter.IssueLink.Api.Redmine;
using BugTrackerPrompter.IssueLink.Tag;

namespace BugTrackerPrompter.IssueLink.Api
{
    public interface IIssueSourceApi
    {
        Task<IssueInfo?> GetIssueInfoAsync(IssueLinkTag tag);
    }

    public class IssueInfo
    {
        public string Title
        {
            get;
        }

        public string Description
        {
            get;
        }

        public IssueInfo(
            string title,
            string description
            )
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            Title = title;
            Description = description;
        }
    }


    public class IssueSourceApi : IIssueSourceApi
    {
        public static readonly IssueSourceApi Instance = new IssueSourceApi();

        /// <inheritdoc />
        public Task<IssueInfo?> GetIssueInfoAsync(
            IssueLinkTag tag
            )
        {
            switch (tag.IssueSource)
            {
                case IssueSourceEnum.Redmine:
                    return RedmineApi.Instance.GetIssueInfoAsync(tag);

                case IssueSourceEnum.Gitlab:
                    return GitlabApi.Instance.GetIssueInfoAsync(tag);

                case IssueSourceEnum.Github:
                    return GithubApi.Instance.GetIssueInfoAsync(tag);

                default:
                    throw new ArgumentOutOfRangeException(tag.IssueSource.ToString());
            }
        }
    }
}
