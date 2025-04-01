using System;
using System.Collections.Generic;
using Asu.Core.Domain.Common;
using Asu.Core.Domain.Orders;
using Asu.Core.Domain.Customization;
using Asu.Core.Domain.Vehicles;
using Asu.Core.Domain.Tasks;
using Asu.Core.Domain.DirectoryOfMaterialCodifiers;
using Asu.Core.Domain.Msi;

namespace Asu.Core.Domain.Customers
{
    /// <summary>
    /// Represents a customer
    /// </summary>
    public partial class Customer : BaseEntity
    {
        private ICollection<ExternalAuthenticationRecord> _externalAuthenticationRecords;
        private ICollection<CustomerRole> _customerRoles;
        private ICollection<ShoppingCartItem> _shoppingCartItems;
        private ICollection<RewardPointsHistory> _rewardPointsHistory;
        private ICollection<Address> _addresses;
        private ICollection<UsersTasks> _usersTasks;
        private ICollection<Spr_cex> _usersWorkshop;
        private ICollection<MemoAddingMaterialCode> _MemoAddingMaterialCodeItems;

        #region WC

        private ICollection<AmazonPaymentAdvanced> _amazonPaymentsAdvanced;

        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        public Customer()
        {
            this.CustomerGuid = Guid.NewGuid();
            this.PasswordFormat = PasswordFormat.Clear;
            this.VehicleGarage = new List<CustomerVehicleGarage>();
        }

        /// <summary>
        /// Gets or sets the customer Guid
        /// </summary>
        public Guid CustomerGuid { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password format
        /// </summary>
        public int PasswordFormatId { get; set; }
        /// <summary>
        /// Gets or sets the password format
        /// </summary>
        public PasswordFormat PasswordFormat
        {
            get { return (PasswordFormat)PasswordFormatId; }
            set { this.PasswordFormatId = (int)value; }
        }
        /// <summary>
        /// Gets or sets the password salt
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is tax exempt
        /// </summary>
        public bool IsTaxExempt { get; set; }

        /// <summary>
        /// Gets or sets the affiliate identifier
        /// </summary>
        public int AffiliateId { get; set; }

        /// <summary>
        /// Gets or sets the vendor identifier with which this customer is associated (maganer)
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this customer has some products in the shopping cart
        /// <remarks>The same as if we run this.ShoppingCartItems.Count > 0
        /// We use this property for performance optimization:
        /// if this property is set to false, then we do not need to load "ShoppingCartItems" navifation property for each page load
        /// It's used only in a couple of places in the presenation layer
        /// </remarks>
        /// </summary>
        public bool HasShoppingCartItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer account is system
        /// </summary>
        public bool IsSystemAccount { get; set; }

        /// <summary>
        /// Gets or sets the customer system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the last IP address
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// Gets or sets the date and time of entity creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last login
        /// </summary>
        public DateTime? LastLoginDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of last activity
        /// </summary>
        public DateTime LastActivityDateUtc { get; set; }

        #region Navigation properties

        /// <summary>
        /// Gets or sets the customer tasks
        /// </summary>
        public virtual ICollection<UsersTasks> UsersTask
        {
            get { return _usersTasks ?? (_usersTasks = new List<UsersTasks>()); }
            protected set { _usersTasks = value; }
        }
        /// <summary>
        /// Gets or sets the customer workshop
        /// </summary>
        public virtual ICollection<Spr_cex> UsersWorkshop
        {
            get { return _usersWorkshop ?? (_usersWorkshop = new List<Spr_cex>()); }
            protected set { _usersWorkshop = value; }
        }      
        /// <summary>
        /// Gets or sets customer generated content
        /// </summary>
        public virtual ICollection<ExternalAuthenticationRecord> ExternalAuthenticationRecords
        {
            get { return _externalAuthenticationRecords ?? (_externalAuthenticationRecords = new List<ExternalAuthenticationRecord>()); }
            protected set { _externalAuthenticationRecords = value; }
        }

        /// <summary>
        /// Gets or sets the customer roles
        /// </summary>
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }

        /// <summary>
        /// Gets or sets shopping cart items
        /// </summary>
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get { return _shoppingCartItems ?? (_shoppingCartItems = new List<ShoppingCartItem>()); }
            protected set { _shoppingCartItems = value; }            
        }
        /// <summary>
        /// Gets or sets MemoAddingMaterialCode items
        /// </summary>
        public virtual ICollection<MemoAddingMaterialCode> MemoAddingMaterialCode
        {
            get { return _MemoAddingMaterialCodeItems ?? (_MemoAddingMaterialCodeItems = new List<MemoAddingMaterialCode>()); }
            protected set { _MemoAddingMaterialCodeItems = value; }
        }

        /// <summary>
        /// Gets or sets reward points history
        /// </summary>
        public virtual ICollection<RewardPointsHistory> RewardPointsHistory
        {
            get { return _rewardPointsHistory ?? (_rewardPointsHistory = new List<RewardPointsHistory>()); }
            protected set { _rewardPointsHistory = value; }            
        }
        
        /// <summary>
        /// Default billing address
        /// </summary>
        public virtual Address BillingAddress { get; set; }

        /// <summary>
        /// Default shipping address
        /// </summary>
        public virtual Address ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets customer addresses
        /// </summary>
        public virtual ICollection<Address> Addresses
        {
            get { return _addresses ?? (_addresses = new List<Address>()); }
            protected set { _addresses = value; }            
        }
        



        #endregion

        #region WC

        public virtual ICollection<AmazonPaymentAdvanced> AmazonPaymentsAdvanced
        {
            get { return _amazonPaymentsAdvanced ?? (_amazonPaymentsAdvanced = new List<AmazonPaymentAdvanced>()); }
            protected set { _amazonPaymentsAdvanced = value; }
        }
        public virtual List<CustomerVehicleGarage> VehicleGarage { get; set; }

        #endregion
    }
}