using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using BugTrackerPrompter.IssueLink.AdornmentControl;
using BugTrackerPrompter.Support;

namespace BugTrackerPrompter.IssueLink.Tag
{
    /// <summary>
    /// Provides color swatch adornments in place of color constants.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a sample usage of the <see cref="IntraTextAdornmentTagTransformer"/> utility class.
    /// </para>
    /// </remarks>
    internal sealed class IssueLinkAdornmentTagger
        : IntraTextAdornmentTagger<IssueLinkTag, IssueControl>

    {
        internal static ITagger<IntraTextAdornmentTag> GetTagger(IWpfTextView view, Lazy<ITagAggregator<IssueLinkTag>> tagger)
        {
            return view.Properties.GetOrCreateSingletonProperty<IssueLinkAdornmentTagger>(
                () => new IssueLinkAdornmentTagger(view, tagger.Value));
        }

        private readonly ITagAggregator<IssueLinkTag> _tagAggregator;

        private IssueLinkAdornmentTagger(IWpfTextView view, ITagAggregator<IssueLinkTag> tagAggregator)
            : base(view)
        {
            _tagAggregator = tagAggregator;
        }

        protected override void DoDispose()
        {
            _tagAggregator.Dispose();

            _view.Properties.RemoveProperty(typeof(IssueLinkAdornmentTagger));
        }

        // To produce adornments that don't obscure the text, the adornment tags
        // should have zero length spans. Overriding this method allows control
        // over the tag spans.
        protected override IEnumerable<Tuple<SnapshotSpan, PositionAffinity?, IssueLinkTag>> GetAdornmentData(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
                yield break;

            ITextSnapshot snapshot = spans[0].Snapshot;

            var tags = _tagAggregator.GetTags(spans);

            foreach (IMappingTagSpan<IssueLinkTag> dataTagSpan in tags)
            {
                NormalizedSnapshotSpanCollection colorTagSpans = dataTagSpan.Span.GetSpans(snapshot);

                // Ignore data tags that are split by projection.
                // This is theoretically possible but unlikely in current scenarios.
                if (colorTagSpans.Count != 1)
                    continue;

                SnapshotSpan adornmentSpan = new SnapshotSpan(colorTagSpans[0].End, 0);

                yield return Tuple.Create(adornmentSpan, (PositionAffinity?)PositionAffinity.Successor, dataTagSpan.Tag);
            }
        }

        protected override IssueControl CreateAdornment(IssueLinkTag dataTag, SnapshotSpan span)
        {
            var view = new IssueControl(
                );
            var viewmodel = new IssueButtonViewModel(
                dataTag
                );
            view.DataContext = viewmodel;

            return view;
        }

        protected override bool UpdateAdornment(IssueControl adornment, IssueLinkTag dataTag)
        {
            (adornment.DataContext as IssueButtonViewModel)?.UpdateTag(dataTag);
            return true;
        }
    }
}
