namespace BugTrackerPrompter.IssueLink.Api
{
    public class IssueInfo
    {
        public string Title
        {
            get;
        }

        public string Description
        {
            get;
        }

        public IssueInfo(
            string title,
            string description
            )
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            Title = title;
            Description = description;
        }
    }
}
