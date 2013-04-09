namespace ProjectDashboard.Domain
{
    using System;
    using System.Collections.Generic;

    public interface IStoryRepository
    {
        List<Story> GetStories();
        void SwapTag(string currentTag, string newTag);
        List<string> GetTags();
    }

    public interface IStoryAnnotationRepository
    {
        void Save(StoryAnnotation storyAnnotation);
        StoryAnnotation Get(int storyID);
    }
}
