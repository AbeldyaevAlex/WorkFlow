namespace Asu.Services.Customization
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Transactions;

    using Core.Data;
    using Core.Domain.Shipping;
    using Logging;
    using Asu.Core.Domain.Customization;
    using Tasks;

    public class ShipmentsTrackingStatusTask : ITask
    {
        internal enum ShippingService
        {
            FedEx = 1,
            UPS = 2,
            USPS = 6,
            Unknown = 100
        }

        private const string LockerName = "ShipmentsTrackingLocker";
        private readonly Guid crmUserProfileGuid = new Guid("D66D063F-2EAF-4CA2-BD62-3C487198B200");
        private readonly int crmUserProfileId;
        private readonly string crmConnectionString = ConfigurationManager.ConnectionStrings["CrmConnectionString"].ConnectionString;
        private readonly IRepository<Shipment> shipmentRepository;
        private readonly IRepository<CrmUserProfile> userProfileRepository;
        private readonly ICustomService customService;
        private readonly ILogger logger;
        private readonly Dictionary<string, ShippingService> carrierRegex = new Dictionary<string, ShippingService>
        {
            {@"\b(1Z ?[0-9A-Z]{3} ?[0-9A-Z]{3} ?[0-9A-Z]{2} ?[0-9A-Z]{4} ?[0-9A-Z]{3} ?[0-9A-Z]|[\dT]\d\d\d ?\d\d\d\d ?\d\d\d)\b", ShippingService.UPS},
            {@"(\b96\d{20}\b)|(\b\d{15}\b)|(\b\d{12}\b)", ShippingService.FedEx},
            {@"\b((98\d\d\d\d\d?\d\d\d\d|98\d\d) ?\d\d\d\d ?\d\d\d\d( ?\d\d\d)?)\b", ShippingService.FedEx},
            {@"^[0-9]{12}$", ShippingService.FedEx},
            {@"\b(91\d\d ?\d\d\d\d ?\d\d\d\d ?\d\d\d\d ?\d\d\d\d ?\d\d|91\d\d ?\d\d\d\d ?\d\d\d\d ?\d\d\d\d ?\d\d\d\d)\b", ShippingService.USPS},
            {@"(\b\d{30}\b)|(\b91\d+\b)|(\b\d{20}\b)", ShippingService.USPS},
            {@"^E\D{1}\d{9}\D{2}$|^9\d{15,21}$", ShippingService.USPS},
            {@"^91[0-9]+$", ShippingService.USPS},
            {@"^[A-Za-z]{2}[0-9]+US$", ShippingService.USPS},
            {@"^(7\d{19})|(\d{1}3\d{18})|(23\d{18})|((EA|EC|CP|RA)\d{9}US)|(82\d{8})$", ShippingService.USPS},
        };

        public ShipmentsTrackingStatusTask(IRepository<Shipment> shipmentRepository, ICustomService customService, ILogger logger, IRepository<CrmUserProfile> userProfileRepository)
        {
            this.shipmentRepository = shipmentRepository;
            this.userProfileRepository = userProfileRepository;
            this.customService = customService;
            this.logger = logger;
            this.crmUserProfileId = this.userProfileRepository.TableNoTracking.SingleOrDefault(m => m.UserId.Equals(this.crmUserProfileGuid)).Id;
        }

        public void Execute()
        {
            return;
            try
            {
                if (this.customService.IsLocked(LockerName, 60 * 60))
                {
                    return;
                }

                this.customService.SetLocked(LockerName);
            }
            catch (Exception exc)
            {
                this.logger.Error(string.Format("Error with OrderShipped queue locker checking. {0}", exc.Message), exc);
                return;
            }

            var orderDateLimit = DateTime.UtcNow.AddDays(-60);
            var dateLimit = DateTime.UtcNow.AddDays(-3);
            var shipments = this.shipmentRepository.Table.Where(s => s.IsTracked == null && s.Order.CreatedOnUtc >= orderDateLimit && s.CreatedOnUtc > dateLimit).OrderBy(i => i.CreatedOnUtc).Take(100).ToList();

            foreach (var shipment in shipments)
            {
                try
                {
                    if (!this.IsMatch(shipment.TrackingNumber))
                    {
                        shipment.IsTracked = false;
                        this.shipmentRepository.Update(shipment);
                        continue;
                    }

                    var shippingService = this.GetCarrier(shipment.TrackingNumber);
                    var isTracked = false;
                    try
                    {
                        using (var scope = new TransactionScope())
                        {
                            if (!this.IsShipmentExist(shipment.Id, shippingService, shipment.TrackingNumber))
                            {
                                this.TrackShipment(shippingService, shipment.TrackingNumber);
                                isTracked = true;
                            }
                            else
                            {
                                isTracked = true;
                            }

                            scope.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.logger.Error(string.Format("ShipmentsTrackingStatusTask TransactionScope. {0}", ex.Message), ex);
                    }

                    if (isTracked)
                    {
                        shipment.IsTracked = true;
                        this.shipmentRepository.Update(shipment);
                    }
                }
                catch (Exception ex)
                {
                    this.logger.Error(string.Format("ShipmentsTrackingStatusTask. {0}", ex.Message), ex);
                }
            }

            this.customService.SetUnlocked(LockerName);
        }

        private string NormalizeTrackingNumber(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
            {
                return null;
            }

            var rgx = new Regex("[^a-zA-Z0-9]");
            trackingNumber = rgx.Replace(trackingNumber, "");

            return trackingNumber.Replace(" ", string.Empty).Trim(new[] { ',', '|', '\t', '\r', '\n', '.', ' ' });
        }

        private bool IsMatch(string trackingNumber)
        {
            trackingNumber = this.NormalizeTrackingNumber(trackingNumber);

            if (string.IsNullOrWhiteSpace(trackingNumber) || trackingNumber.StartsWith("000"))
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(trackingNumber) && this.carrierRegex.Any(regex => new Regex(regex.Key).IsMatch(trackingNumber));
        }

        private ShippingService GetCarrier(string trackingNumber)
        {
            trackingNumber = this.NormalizeTrackingNumber(trackingNumber);
            if (string.IsNullOrWhiteSpace(trackingNumber))
            {
                return ShippingService.Unknown;
            }

            foreach (var regex in this.carrierRegex)
            {
                if (new Regex(regex.Key).IsMatch(trackingNumber))
                {
                    return regex.Value;
                }
            }

            return ShippingService.Unknown;
        }

        private bool IsShipmentExist(int shipmentId, ShippingService shippingService, string trackingNumber)
        {
            using (var connection = new SqlConnection(this.crmConnectionString))
            {
                using (var cmd = new SqlCommand("i_GetShipment", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var shipmentsTable = new DataTable();
                    shipmentsTable.Columns.Add("Id");
                    shipmentsTable.Columns.Add("ShippingServiceId");
                    shipmentsTable.Columns.Add("TrackingNumber");
                    shipmentsTable.Columns.Add("CreatedBy");

                    var dr = shipmentsTable.NewRow();
                    dr["Id"] = shipmentId;
                    dr["ShippingServiceId"] = (int)shippingService;
                    dr["TrackingNumber"] = trackingNumber;
                    dr["CreatedBy"] = this.crmUserProfileId;
                    shipmentsTable.Rows.Add(dr);

                    cmd.Parameters.Add(new SqlParameter("Shipments", SqlDbType.Structured)
                    {
                        Value = shipmentsTable,
                        TypeName = "i_Shipment"
                    });

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        connection.Close();
                        return true;
                    }

                    connection.Close();
                }
            }

            return false;
        }

        private void TrackShipment(ShippingService shippingService, string trackingNumber)
        {
            using (var connection = new SqlConnection(this.crmConnectionString))
            {
                using (var cmd = new SqlCommand("i_TrackShipment", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var shipmentsTable = new DataTable();
                    shipmentsTable.Columns.Add("ShippingServiceId");
                    shipmentsTable.Columns.Add("TrackingNumber");
                    shipmentsTable.Columns.Add("CreatedBy");

                    var dr = shipmentsTable.NewRow();
                    dr["ShippingServiceId"] = (int)shippingService;
                    dr["TrackingNumber"] = trackingNumber;
                    dr["CreatedBy"] = this.crmUserProfileId;
                    shipmentsTable.Rows.Add(dr);

                    cmd.Parameters.Add(new SqlParameter("Shipments", SqlDbType.Structured)
                    {
                        Value = shipmentsTable,
                        TypeName = "i_Shipment"
                    });

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
