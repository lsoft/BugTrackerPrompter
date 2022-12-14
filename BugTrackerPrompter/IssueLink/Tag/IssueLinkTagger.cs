using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using BugTrackerPrompter.Support;
using BugTrackerPrompter.IssueLink.Api;
using System.Linq;

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
        public IssueLinkTagger(ITextBuffer buffer)
            : base(buffer, IssueSourceApi.Regexes)
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

            var api = IssueSourceApi.Apis.FirstOrDefault(api => api.IssueHeader == nameGroup.ToString());
            if(api == null)
            {
                return null;
            }

            return new IssueLinkTag(api.Source, issueNumber);
        }

    }
}
