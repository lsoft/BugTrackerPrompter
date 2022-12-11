using System.Windows;
using System.Windows.Controls;

namespace BugTrackerPrompter.IssueLink.AdornmentControl
{
    /// <summary>
    /// Interaction logic for IssueControl.xaml
    /// </summary>
    public partial class IssueControl : UserControl
    {
        public IssueControl()
        {
            InitializeComponent();
        }


        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            IssueButton.IsOpen = true;
        }

    }
}
