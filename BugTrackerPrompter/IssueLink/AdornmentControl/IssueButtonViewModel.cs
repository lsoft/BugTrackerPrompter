using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BugTrackerPrompter.IssueLink.Api;
using BugTrackerPrompter.IssueLink.Tag;
using BugTrackerPrompter.Wpf;

namespace BugTrackerPrompter.IssueLink.AdornmentControl
{
    public class IssueButtonViewModel : BaseViewModel
    {
        private IssueLinkTag _tag;
        private IssueInfo? _issueInfo = null;

        public string ButtonContent => _tag.ToVisual();

        public ICommand GotoCommand
        {
            get;
        }

        public ICommand CopyToClipboardCommand
        {
            get;
        }

        public bool IssueButtonEnabled => _issueInfo != null;

        public string Title => _issueInfo?.Title ?? string.Empty;

        public string Description => _issueInfo?.Description ?? string.Empty;

        public Brush IssueButtonForeground => _issueInfo == null ? Brushes.Red : Brushes.Green;

        public IssueButtonViewModel(
            IssueLinkTag tag
            )
        {
            _tag = tag;

            GotoCommand = new RelayCommand(
                o =>
                {
                    System.Diagnostics.Process.Start(_tag.BuildIssueUrl());
                },
                o =>
                {
                    return _issueInfo != null;
                }
                );

            CopyToClipboardCommand = new RelayCommand(
                o =>
                {
                    Clipboard.SetText(
                        _tag.BuildIssueUrl()
                        );
                }
                );

            LoadIssueBodyAsync().FileAndForget(nameof(LoadIssueBodyAsync));
        }

        public void UpdateTag(
            IssueLinkTag tag
            )
        {
            _tag = tag;

            LoadIssueBodyAsync().FileAndForget(nameof(LoadIssueBodyAsync));
            OnPropertyChanged();
        }


        private async Task LoadIssueBodyAsync()
        {
            try
            {
                _issueInfo = await IssueSourceApi.Instance.GetIssueInfoAsync(_tag);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                OnPropertyChanged();
            }
        }

        private string GetTitle(
            string content
            )
        {
            const string title = "<title>";
            const string stitle = "</title>";

            var fi = content.IndexOf(title, StringComparison.Ordinal);
            if (fi < 0)
            {
                return "Cannot be parsed";
            }

            var si = content.IndexOf(stitle, StringComparison.Ordinal);
            if (si < 0)
            {
                return "Cannot be parsed";
            }

            return content.Substring(fi + title.Length, si - fi - title.Length);
        }

    }
}
