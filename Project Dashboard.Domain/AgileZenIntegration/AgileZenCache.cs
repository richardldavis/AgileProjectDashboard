namespace ProjectDashboard.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;
    using Model.Stories;

    public class StoryCache : IStoryCache
    {
        #region Fields

        private readonly ObjectCache _cache;

        #endregion

        #region Constructor

        public StoryCache()
        {
            _cache = MemoryCache.Default;
        }

        #endregion

        #region Methods

        public IList<Story> GetStories()
        {
            return (IList<Story>)_cache.Get(CacheKeys.Stories);
        }

        public IList<Story> AddStories(IList<Story> stories)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(0.01) };
            _cache.Set(CacheKeys.Stories, stories, policy);
            return stories;
        }

        public void ClearCache()
        {
            _cache.Remove(CacheKeys.Stories);
        }

        #endregion

        #region Classes

        private static class CacheKeys
        {
            public const string Stories = "Stories";
        }

        #endregion
    }
}
