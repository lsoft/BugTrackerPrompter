using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BugTrackerPrompter.IssueLink.Tag;

namespace BugTrackerPrompter.IssueLink.Api
{
    public interface IIssueSourceApi
    {
        IssueSourceEnum Source
        {
            get;
        }

        string IssueHeader
        {
            get;
        }

        Regex AdornmentRegex
        {
            get;
        }

        string BuildIssueUrl(int issueNumber);

        Task<IssueInfo?> GetIssueInfoAsync(IssueLinkTag tag);
    }
}
