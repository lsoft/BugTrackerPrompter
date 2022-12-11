using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace BugTrackerPrompter.IssueLink.Tag
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("code")]
    [TagType(typeof(IssueLinkTag))]
    internal sealed class IssueLinkTaggerProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            return buffer.Properties.GetOrCreateSingletonProperty(() => new IssueLinkTagger(buffer)) as ITagger<T>;
        }
    }
}
