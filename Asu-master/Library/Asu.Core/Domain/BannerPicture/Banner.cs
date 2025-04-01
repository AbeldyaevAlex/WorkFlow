using Asu.Core.Domain.Stores;
using System;

namespace Asu.Core.Domain.BannerPicture
{
    public partial class Banner : BaseEntity, IStoreMappingSupported
    {
        /// <summary>
        /// Gets or Sets entity identifire
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Gets or Sets entity name
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or Sets banner picture identifire
        /// </summary>
        public string BannerPicturePath { get; set; }

        /// <summary>
        /// Gets or Sets mobile banner picture identifire
        /// </summary>
        public string MobileBannerPicturePath { get; set; }

        /// <summary>
        /// Gets or Sets alter text
        /// </summary>
        public string AlterText { get; set; }

        /// <summary>
        /// Gets or Sets display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or Sets active
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or Sets start date
        /// </summary>
        public DateTime? StartDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or Sets end date
        /// </summary>
        public DateTime? EndDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }


        /// <summary>
        /// Gets or Sets banner picture identifire
        /// </summary>
        public int BannerPictureId { get; set; }

        /// <summary>
        /// Gets or Sets mobile banner picture identifire
        /// </summary>
        public int MobileBannerPictureId { get; set; }
    }
}
