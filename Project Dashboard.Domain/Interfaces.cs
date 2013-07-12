﻿namespace ProjectDashboard.Domain
{
    using ProjectDashboard.Domain.TimeZoneIntegration;
    using System;
    using System.Collections.Generic;
    using Model.Stories;

    public interface IStoryRepository
    {
        List<Story> GetStories();
        void SwapTag(string currentTag, string newTag);
        List<string> GetTags();
        void ReplaceTextInDetails(string oldValue, string newValue);
    }

    public interface ICommentRepository
    {
        void Add(int storyId, string text);
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

    public interface ITimeZoneCache
    {
        IList<TimeZoneEntry> GetEntries();
        IList<TimeZoneEntry> AddEntries(IList<TimeZoneEntry> entry);
        void ClearCache();
    }
}
