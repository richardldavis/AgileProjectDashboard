namespace ProjectDashboard.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;

    public class StoryCache : IStoryCache
    {
        private ObjectCache _cache;
     
        public StoryCache()
        {
            _cache = MemoryCache.Default;
        }

        public IList<Story> GetStories()
        {
            return (IList<Story>)_cache.Get("Stories");
        }

        public IList<Story> AddStories(IList<Story> stories)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(0.01) };

            _cache.Set("Stories", stories, policy);

            return stories;
        }

        public void ClearCache()
        {
            _cache.Remove("Stories");
        }
    }
}
