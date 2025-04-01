using System.Collections.Generic;

namespace Asu.Services.Events
{
    /// <summary>
    /// Event subscrption service
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Get subscriptions
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Event consumers</returns>
        IList<IConsumer<T>> GetSubscriptions<T>();
    }
}
