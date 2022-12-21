using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BugTrackerPrompter.IssueLink.Tag;
using BugTrackerPrompter.Support;

namespace BugTrackerPrompter.IssueLink.Api
{
    public interface IIssueSourceApi
    {
        bool IsEnabled
        {
            get;
        }

        IssueSourceEnum Source
        {
            get;
        }

        string IssueHeader
        {
            get;
        }

        Regex2 AdornmentRegex
        {
            get;
        }

        string BuildIssueUrl(int issueNumber);

        Task<IssueInfo?> GetIssueInfoAsync(IssueLinkTag tag);
    }
}
