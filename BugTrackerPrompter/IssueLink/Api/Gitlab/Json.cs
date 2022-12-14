using System;

namespace BugTrackerPrompter.IssueLink.Api.Gitlab
{
    public class IssueRootObject
    {
        public int id { get; set; }
        public int iid { get; set; }
        public int project_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string state { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object closed_at { get; set; }
        public object closed_by { get; set; }
        public object[] labels { get; set; }
        public object milestone { get; set; }
        public User[] assignees { get; set; }
        public User author { get; set; }
        public string type { get; set; }
        public User assignee { get; set; }
        public int user_notes_count { get; set; }
        public int merge_requests_count { get; set; }
        public int upvotes { get; set; }
        public int downvotes { get; set; }
        public object due_date { get; set; }
        public bool confidential { get; set; }
        public object discussion_locked { get; set; }
        public string issue_type { get; set; }
        public string web_url { get; set; }
        public Time_Stats time_stats { get; set; }
        public Task_Completion_Status task_completion_status { get; set; }
        public bool has_tasks { get; set; }
        public string task_status { get; set; }
        public _Links _links { get; set; }
        public References references { get; set; }
        public bool subscribed { get; set; }
        public object moved_to_id { get; set; }
        public object service_desk_reply_to { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string state { get; set; }
        public string avatar_url { get; set; }
        public string web_url { get; set; }
    }

    public class Time_Stats
    {
        public int time_estimate { get; set; }
        public int total_time_spent { get; set; }
        public object human_time_estimate { get; set; }
        public object human_total_time_spent { get; set; }
    }

    public class Task_Completion_Status
    {
        public int count { get; set; }
        public int completed_count { get; set; }
    }

    public class _Links
    {
        public string self { get; set; }
        public string notes { get; set; }
        public string award_emoji { get; set; }
        public string project { get; set; }
    }

    public class References
    {
        public string _short { get; set; }
        public string relative { get; set; }
        public string full { get; set; }
    }

}
