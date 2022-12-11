using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using BugTrackerPrompter.Support;

namespace BugTrackerPrompter.IssueLink.Tag
{
    /// <summary>
    /// Determines which spans of text likely refer to color values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a data-only component. The tagging system is a good fit for presenting data-about-text.
    /// The <see cref="IssueLinkAdornmentTagger"/> takes color tags produced by this tagger and creates corresponding UI for this data.
    /// </para>
    /// </remarks>
    internal sealed class IssueLinkTagger : RegexTagger<IssueLinkTag>
    {
        private const string RedminePrefix = "RM-";
        private const string GitlabPrefix = "#";

        public IssueLinkTagger(ITextBuffer buffer)
            : base(buffer, new[]
            {
                new Regex(@"(^|\s|\W)(?'name'RM-)(?'id'\d{1,7})($|\s|\W)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase),
                new Regex(@"(^|\s|\W)(?'name'#)(?'id'\d{1,7})($|\s|\W)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase),
            })
        {
        }

        /// <inheritdoc />
        protected override void DoDispose()
        {
            //nothing to do
        }

        protected override IssueLinkTag TryCreateTagForMatch(Match match)
        {
            var idGroup = match.Groups["id"];
            if (!idGroup.Success)
            {
                return null;
            }

            if (!int.TryParse(idGroup.ToString(), out var issueNumber) || issueNumber <= 0)
            {
                return null;
            }

            var nameGroup = match.Groups["name"];
            if (!nameGroup.Success)
            {
                return null;
            }

            if (nameGroup.ToString() == RedminePrefix)
            {
                return new IssueLinkTag(IssueSourceEnum.Redmine, issueNumber);
            }
            if (nameGroup.ToString() == GitlabPrefix)
            {
                return new IssueLinkTag(IssueSourceEnum.Gitlab, issueNumber);
            }

            return null;
        }

    }
}
