namespace ProjectDashboard.Domain
{
    using ProjectDashboard.Domain.TimeZoneIntegration;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;

    public class TimeZoneCache : ITimeZoneCache
    {
        private ObjectCache _cache;
     
        public TimeZoneCache()
        {
            _cache = MemoryCache.Default;
        }

        public IList<TimeZoneEntry> GetEntries()
        {
            return (IList<TimeZoneEntry>)_cache.Get("Entries");
        }

        public IList<TimeZoneEntry> AddEntries(IList<TimeZoneEntry> entries)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(0.1) };

            _cache.Set("Entries", entries, policy);

            return entries;
        }

        public void ClearCache()
        {
            _cache.Remove("Entries");
        }
    }
}
