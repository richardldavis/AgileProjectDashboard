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

    public interface ICommentRepository
    {
        void Add(int storyID, string text);
        //void GetComments();
    }

    public interface IStoryAnnotationRepository
    {
        void Save(StoryAnnotation storyAnnotation);
        StoryAnnotation Get(int storyID);
    }

    public interface IStoryCache
    {
        IList<Story> GetStories();
        IList<Story> AddStories(IList<Story> story);
        void ClearCache();
    }
}
