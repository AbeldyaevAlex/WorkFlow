using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asu.Core.Domain.Catalog
{
    using System;

    /// <summary>
    /// Represents a recurring product cycle period
    /// </summary>
    [Serializable]
    public enum RecurringProductCyclePeriod
    {
        /// <summary>
        /// Days
        /// </summary>
        Days = 0,
        /// <summary>
        /// Weeks
        /// </summary>
        Weeks = 10,
        /// <summary>
        /// Months
        /// </summary>
        Months = 20,
        /// <summary>
        /// Years
        /// </summary>
        Years = 30,
    }
}
