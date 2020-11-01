using Motel.Domain.Domain.Configuration;
using Motel.Services.Caching;


namespace Motel.Services.Configuration.Caching
{
    /// <summary>
    /// Represents a setting cache event consumer
    /// </summary>
    public partial class SettingCacheEventConsumer : CacheEventConsumer<Setting>
    {
    }
}