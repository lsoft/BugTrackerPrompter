using System;
using Microsoft.VisualStudio.Text.Tagging;
using BugTrackerPrompter.Options;

namespace BugTrackerPrompter.IssueLink.Tag
{
    /// <summary>
    /// Data tag indicating that the tagged text represents a color.
    /// </summary>
    /// <remarks>
    /// Note that this tag has nothing directly to do with adornments or other UI.
    /// This sample's adornments will be produced based on the data provided in these tags.
    /// This separation provides the potential for other extensions to consume color tags
    /// and provide alternative UI or other derived functionality over this data.
    /// </remarks>
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

        public string BuildIssueUrl()
        {
            switch (IssueSource)
            {
                case IssueSourceEnum.Redmine:
                    return string.Format(IssueLinkOptionsModel.Instance.RedmineWebRoot, IssueNumber);

                case IssueSourceEnum.Gitlab:
                    return string.Format(IssueLinkOptionsModel.Instance.GitlabWebRoot, IssueNumber);

                default:
                    throw new ArgumentOutOfRangeException(IssueSource.ToString());
            }
        }
    }
}
