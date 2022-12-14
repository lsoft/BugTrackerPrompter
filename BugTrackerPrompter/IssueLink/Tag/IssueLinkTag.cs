using System;
using Microsoft.VisualStudio.Text.Tagging;
using BugTrackerPrompter.Options;
using BugTrackerPrompter.IssueLink.Api;

namespace BugTrackerPrompter.IssueLink.Tag
{
    public class IssueLinkTag : ITag
    {
        public IssueSourceEnum IssueSource
        {
            get;
        }

        public int IssueNumber
        {
            get;
        }

        public IssueLinkTag(
            IssueSourceEnum issueSource,
            int issueNumber
            )
        {
            IssueSource = issueSource;
            IssueNumber = issueNumber;
        }

        public string ToVisual()
        {
            return $"{IssueSource} {IssueNumber}";
        }
    }
}
