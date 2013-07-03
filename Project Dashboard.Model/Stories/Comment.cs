namespace ProjectDashboard.Model.Stories
{
    using System;

    public class Comment
    {
        public int Story {get; set;}

        public string Text {get; set;}
        
        public string  Who {get; set;}
        
        public DateTime Date { get; set; }
    }
}
