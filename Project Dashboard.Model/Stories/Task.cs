using System;
namespace ProjectDashboard.Model.Stories
{
    public class Task
    {
        public int Story {get; set;}

        public string Text {get; set;}
        
        public bool  Complete {get; set;}

        public DateTime? FinishedDate { get; set; }

        public string FinishedBy { get; set; }
    }
}
