// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.OrderReferenceDetails
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System;
using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class OrderReferenceDetails
  {
    private string amazonOrderReferenceIdField;
    private string orderLanguage;
    private Buyer buyerField;
    private OrderTotal orderTotalField;
    private string sellerNoteField;
    private string platformIdField;
    private Destination destinationField;
    private BillingAddress billingAddressField;
    private ReleaseEnvironment? releaseEnvironmentField;
    private SellerOrderAttributes sellerOrderAttributesField;
    private OrderReferenceStatus orderReferenceStatusField;
    private Constraints constraintsField;
    private DateTime? creationTimestampField;
    private DateTime? expirationTimestampField;
    private ParentDetails parentDetailsField;
    private IdList idListField;

    [XmlElement(ElementName = "AmazonOrderReferenceId")]
    public string AmazonOrderReferenceId
    {
      get
      {
        return this.amazonOrderReferenceIdField;
      }
      set
      {
        this.amazonOrderReferenceIdField = value;
      }
    }

    public OrderReferenceDetails WithAmazonOrderReferenceId(string amazonOrderReferenceId)
    {
      this.amazonOrderReferenceIdField = amazonOrderReferenceId;
      return this;
    }

    public bool IsSetAmazonOrderReferenceId()
    {
      return this.amazonOrderReferenceIdField != null;
    }

    [XmlElement(ElementName = "OrderLanguage")]
    public string OrderLanguage
    {
      get
      {
        return this.orderLanguage;
      }
      set
      {
        this.orderLanguage = value;
      }
    }

    public OrderReferenceDetails WithOrderLanguage(string orderLanguage)
    {
      this.orderLanguage = orderLanguage;
      return this;
    }

    public bool IsSetOrderLanguage()
    {
      return this.orderLanguage != null;
    }

    [XmlElement(ElementName = "Buyer")]
    public Buyer Buyer
    {
      get
      {
        return this.buyerField;
      }
      set
      {
        this.buyerField = value;
      }
    }

    public OrderReferenceDetails WithBuyer(Buyer buyer)
    {
      this.buyerField = buyer;
      return this;
    }

    public bool IsSetBuyer()
    {
      return this.buyerField != null;
    }

    [XmlElement(ElementName = "OrderTotal")]
    public OrderTotal OrderTotal
    {
      get
      {
        return this.orderTotalField;
      }
      set
      {
        this.orderTotalField = value;
      }
    }

    public OrderReferenceDetails WithOrderTotal(OrderTotal orderTotal)
    {
      this.orderTotalField = orderTotal;
      return this;
    }

    public bool IsSetOrderTotal()
    {
      return this.orderTotalField != null;
    }

    [XmlElement(ElementName = "SellerNote")]
    public string SellerNote
    {
      get
      {
        return this.sellerNoteField;
      }
      set
      {
        this.sellerNoteField = value;
      }
    }

    public OrderReferenceDetails WithSellerNote(string sellerNote)
    {
      this.sellerNoteField = sellerNote;
      return this;
    }

    public bool IsSetSellerNote()
    {
      return this.sellerNoteField != null;
    }

    [XmlElement(ElementName = "PlatformId")]
    public string PlatformId
    {
      get
      {
        return this.platformIdField;
      }
      set
      {
        this.platformIdField = value;
      }
    }

    public OrderReferenceDetails WithPlatformId(string platformId)
    {
      this.platformIdField = platformId;
      return this;
    }

    public bool IsSetPlatformId()
    {
      return this.platformIdField != null;
    }

    [XmlElement(ElementName = "Destination")]
    public Destination Destination
    {
      get
      {
        return this.destinationField;
      }
      set
      {
        this.destinationField = value;
      }
    }

    public OrderReferenceDetails WithDestination(Destination destination)
    {
      this.destinationField = destination;
      return this;
    }

    public bool IsSetDestination()
    {
      return this.destinationField != null;
    }

    [XmlElement(ElementName = "BillingAddress")]
    public BillingAddress BillingAddress
    {
      get
      {
        return this.billingAddressField;
      }
      set
      {
        this.billingAddressField = value;
      }
    }

    public OrderReferenceDetails WithBillingAddress(BillingAddress billingAddress)
    {
      this.billingAddressField = billingAddress;
      return this;
    }

    public bool IsSetBillingAddress()
    {
      return this.billingAddressField != null;
    }

    [XmlElement(ElementName = "ReleaseEnvironment")]
    public ReleaseEnvironment ReleaseEnvironment
    {
      get
      {
        return this.releaseEnvironmentField.GetValueOrDefault();
      }
      set
      {
        this.releaseEnvironmentField = new ReleaseEnvironment?(value);
      }
    }

    public OrderReferenceDetails WithReleaseEnvironment(ReleaseEnvironment releaseEnvironment)
    {
      this.releaseEnvironmentField = new ReleaseEnvironment?(releaseEnvironment);
      return this;
    }

    public bool IsSetReleaseEnvironment()
    {
      return this.releaseEnvironmentField.HasValue;
    }

    [XmlElement(ElementName = "SellerOrderAttributes")]
    public SellerOrderAttributes SellerOrderAttributes
    {
      get
      {
        return this.sellerOrderAttributesField;
      }
      set
      {
        this.sellerOrderAttributesField = value;
      }
    }

    public OrderReferenceDetails WithSellerOrderAttributes(SellerOrderAttributes sellerOrderAttributes)
    {
      this.sellerOrderAttributesField = sellerOrderAttributes;
      return this;
    }

    public bool IsSetSellerOrderAttributes()
    {
      return this.sellerOrderAttributesField != null;
    }

    [XmlElement(ElementName = "OrderReferenceStatus")]
    public OrderReferenceStatus OrderReferenceStatus
    {
      get
      {
        return this.orderReferenceStatusField;
      }
      set
      {
        this.orderReferenceStatusField = value;
      }
    }

    public OrderReferenceDetails WithOrderReferenceStatus(OrderReferenceStatus orderReferenceStatus)
    {
      this.orderReferenceStatusField = orderReferenceStatus;
      return this;
    }

    public bool IsSetOrderReferenceStatus()
    {
      return this.orderReferenceStatusField != null;
    }

    [XmlElement(ElementName = "Constraints")]
    public Constraints Constraints
    {
      get
      {
        return this.constraintsField;
      }
      set
      {
        this.constraintsField = value;
      }
    }

    public OrderReferenceDetails WithConstraints(Constraints constraints)
    {
      this.constraintsField = constraints;
      return this;
    }

    public bool IsSetConstraints()
    {
      return this.constraintsField != null;
    }

    [XmlElement(ElementName = "CreationTimestamp")]
    public DateTime CreationTimestamp
    {
      get
      {
        return this.creationTimestampField.GetValueOrDefault();
      }
      set
      {
        this.creationTimestampField = new DateTime?(value);
      }
    }

    public OrderReferenceDetails WithCreationTimestamp(DateTime creationTimestamp)
    {
      this.creationTimestampField = new DateTime?(creationTimestamp);
      return this;
    }

    public bool IsSetCreationTimestamp()
    {
      return this.creationTimestampField.HasValue;
    }

    [XmlElement(ElementName = "ExpirationTimestamp")]
    public DateTime ExpirationTimestamp
    {
      get
      {
        return this.expirationTimestampField.GetValueOrDefault();
      }
      set
      {
        this.expirationTimestampField = new DateTime?(value);
      }
    }

    public OrderReferenceDetails WithExpirationTimestamp(DateTime expirationTimestamp)
    {
      this.expirationTimestampField = new DateTime?(expirationTimestamp);
      return this;
    }

    public bool IsSetExpirationTimestamp()
    {
      return this.expirationTimestampField.HasValue;
    }

    [XmlElement(ElementName = "ParentDetails")]
    public ParentDetails ParentDetails
    {
      get
      {
        return this.parentDetailsField;
      }
      set
      {
        this.parentDetailsField = value;
      }
    }

    public OrderReferenceDetails WithParentDetails(ParentDetails parentDetails)
    {
      this.parentDetailsField = parentDetails;
      return this;
    }

    public bool IsSetParentDetails()
    {
      return this.parentDetailsField != null;
    }

    [XmlElement(ElementName = "IdList")]
    public IdList IdList
    {
      get
      {
        return this.idListField;
      }
      set
      {
        this.idListField = value;
      }
    }

    public OrderReferenceDetails WithIdList(IdList idList)
    {
      this.idListField = idList;
      return this;
    }

    public bool IsSetIdList()
    {
      return this.idListField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetAmazonOrderReferenceId())
      {
        stringBuilder.Append("<AmazonOrderReferenceId>");
        stringBuilder.Append(this.EscapeXML(this.AmazonOrderReferenceId));
        stringBuilder.Append("</AmazonOrderReferenceId>");
      }
      if (this.IsSetOrderLanguage())
      {
        stringBuilder.Append("<OrderLanguage>");
        stringBuilder.Append(this.EscapeXML(this.OrderLanguage));
        stringBuilder.Append("</OrderLanguage>");
      }
      if (this.IsSetBuyer())
      {
        Buyer buyer = this.Buyer;
        stringBuilder.Append("<Buyer>");
        stringBuilder.Append(buyer.ToXMLFragment());
        stringBuilder.Append("</Buyer>");
      }
      if (this.IsSetOrderTotal())
      {
        OrderTotal orderTotal = this.OrderTotal;
        stringBuilder.Append("<OrderTotal>");
        stringBuilder.Append(orderTotal.ToXMLFragment());
        stringBuilder.Append("</OrderTotal>");
      }
      if (this.IsSetSellerNote())
      {
        stringBuilder.Append("<SellerNote>");
        stringBuilder.Append(this.SellerNote);
        stringBuilder.Append("</SellerNote>");
      }
      if (this.IsSetPlatformId())
      {
        stringBuilder.Append("<PlatformId>");
        stringBuilder.Append(this.EscapeXML(this.PlatformId));
        stringBuilder.Append("</PlatformId>");
      }
      if (this.IsSetDestination())
      {
        Destination destination = this.Destination;
        stringBuilder.Append("<Destination>");
        stringBuilder.Append(destination.ToXMLFragment());
        stringBuilder.Append("</Destination>");
      }
      if (this.IsSetBillingAddress())
      {
        BillingAddress billingAddress = this.BillingAddress;
        stringBuilder.Append("<BillingAddress>");
        stringBuilder.Append(billingAddress.ToXMLFragment());
        stringBuilder.Append("</BillingAddress>");
      }
      if (this.IsSetReleaseEnvironment())
      {
        stringBuilder.Append("<ReleaseEnvironment>");
        stringBuilder.Append((object) this.ReleaseEnvironment);
        stringBuilder.Append("</ReleaseEnvironment>");
      }
      if (this.IsSetSellerOrderAttributes())
      {
        SellerOrderAttributes sellerOrderAttributes = this.SellerOrderAttributes;
        stringBuilder.Append("<SellerOrderAttributes>");
        stringBuilder.Append(sellerOrderAttributes.ToXMLFragment());
        stringBuilder.Append("</SellerOrderAttributes>");
      }
      if (this.IsSetOrderReferenceStatus())
      {
        OrderReferenceStatus orderReferenceStatus = this.OrderReferenceStatus;
        stringBuilder.Append("<OrderReferenceStatus>");
        stringBuilder.Append(orderReferenceStatus.ToXMLFragment());
        stringBuilder.Append("</OrderReferenceStatus>");
      }
      if (this.IsSetConstraints())
      {
        Constraints constraints = this.Constraints;
        stringBuilder.Append("<Constraints>");
        stringBuilder.Append(constraints.ToXMLFragment());
        stringBuilder.Append("</Constraints>");
      }
      if (this.IsSetCreationTimestamp())
      {
        stringBuilder.Append("<CreationTimestamp>");
        stringBuilder.Append((object) this.CreationTimestamp);
        stringBuilder.Append("</CreationTimestamp>");
      }
      if (this.IsSetExpirationTimestamp())
      {
        stringBuilder.Append("<ExpirationTimestamp>");
        stringBuilder.Append((object) this.ExpirationTimestamp);
        stringBuilder.Append("</ExpirationTimestamp>");
      }
      if (this.IsSetParentDetails())
      {
        ParentDetails parentDetails = this.ParentDetails;
        stringBuilder.Append("<ParentDetails>");
        stringBuilder.Append(parentDetails.ToXMLFragment());
        stringBuilder.Append("</ParentDetails>");
      }
      if (this.IsSetIdList())
      {
        IdList idList = this.IdList;
        stringBuilder.Append("<IdList>");
        stringBuilder.Append(idList.ToXMLFragment());
        stringBuilder.Append("</IdList>");
      }
      return stringBuilder.ToString();
    }

    private string EscapeXML(string str)
    {
      if (str == null)
        return "null";
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char ch in str)
      {
        switch (ch)
        {
          case '"':
            stringBuilder.Append("&quot;");
            break;
          case '&':
            stringBuilder.Append("&amp;");
            break;
          case '\'':
            stringBuilder.Append("&#039;");
            break;
          case '<':
            stringBuilder.Append("&lt;");
            break;
          case '>':
            stringBuilder.Append("&gt;");
            break;
          default:
            stringBuilder.Append(ch);
            break;
        }
      }
      return stringBuilder.ToString();
    }
  }
}
