// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.BillingAgreementDetails
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
  public class BillingAgreementDetails
  {
    private string amazonBillingAgreementIdField;
    private string orderLanguage;
    private BillingAgreementLimits billingAgreementLimitsField;
    private Buyer buyerField;
    private string sellerNoteField;
    private string platformIdField;
    private Destination destinationField;
    private BillingAddress billingAddressField;
    private ReleaseEnvironment? releaseEnvironmentField;
    private SellerBillingAgreementAttributes sellerBillingAgreementAttributesField;
    private BillingAgreementStatus billingAgreementStatusField;
    private Constraints constraintsField;
    private DateTime? creationTimestampField;
    private DateTime? expirationTimestampField;
    private bool? billingAgreementConsentField;

    [XmlElement(ElementName = "AmazonBillingAgreementId")]
    public string AmazonBillingAgreementId
    {
      get
      {
        return this.amazonBillingAgreementIdField;
      }
      set
      {
        this.amazonBillingAgreementIdField = value;
      }
    }

    public BillingAgreementDetails WithAmazonBillingAgreementId(string amazonBillingAgreementId)
    {
      this.amazonBillingAgreementIdField = amazonBillingAgreementId;
      return this;
    }

    public bool IsSetAmazonBillingAgreementId()
    {
      return this.amazonBillingAgreementIdField != null;
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

    public BillingAgreementDetails WithOrderLanguage(string orderLanguage)
    {
      this.orderLanguage = orderLanguage;
      return this;
    }

    public bool IsSetOrderLanguage()
    {
      return this.orderLanguage != null;
    }

    [XmlElement(ElementName = "BillingAgreementLimits")]
    public BillingAgreementLimits BillingAgreementLimits
    {
      get
      {
        return this.billingAgreementLimitsField;
      }
      set
      {
        this.billingAgreementLimitsField = value;
      }
    }

    public BillingAgreementDetails WithBillingAgreementLimits(BillingAgreementLimits billingAgreementLimits)
    {
      this.billingAgreementLimitsField = billingAgreementLimits;
      return this;
    }

    public bool IsSetBillingAgreementLimits()
    {
      return this.billingAgreementLimitsField != null;
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

    public BillingAgreementDetails WithBuyer(Buyer buyer)
    {
      this.buyerField = buyer;
      return this;
    }

    public bool IsSetBuyer()
    {
      return this.buyerField != null;
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

    public BillingAgreementDetails WithSellerNote(string sellerNote)
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

    public BillingAgreementDetails WithPlatformId(string platformId)
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

    public BillingAgreementDetails WithDestination(Destination destination)
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

    public BillingAgreementDetails WithBillingAddress(BillingAddress billingAddress)
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

    public BillingAgreementDetails WithReleaseEnvironment(ReleaseEnvironment releaseEnvironment)
    {
      this.releaseEnvironmentField = new ReleaseEnvironment?(releaseEnvironment);
      return this;
    }

    public bool IsSetReleaseEnvironment()
    {
      return this.releaseEnvironmentField.HasValue;
    }

    [XmlElement(ElementName = "SellerBillingAgreementAttributes")]
    public SellerBillingAgreementAttributes SellerBillingAgreementAttributes
    {
      get
      {
        return this.sellerBillingAgreementAttributesField;
      }
      set
      {
        this.sellerBillingAgreementAttributesField = value;
      }
    }

    public BillingAgreementDetails WithSellerBillingAgreementAttributes(SellerBillingAgreementAttributes sellerBillingAgreementAttributes)
    {
      this.sellerBillingAgreementAttributesField = sellerBillingAgreementAttributes;
      return this;
    }

    public bool IsSetSellerBillingAgreementAttributes()
    {
      return this.sellerBillingAgreementAttributesField != null;
    }

    [XmlElement(ElementName = "BillingAgreementStatus")]
    public BillingAgreementStatus BillingAgreementStatus
    {
      get
      {
        return this.billingAgreementStatusField;
      }
      set
      {
        this.billingAgreementStatusField = value;
      }
    }

    public BillingAgreementDetails WithBillingAgreementStatus(BillingAgreementStatus billingAgreementStatus)
    {
      this.billingAgreementStatusField = billingAgreementStatus;
      return this;
    }

    public bool IsSetBillingAgreementStatus()
    {
      return this.billingAgreementStatusField != null;
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

    public BillingAgreementDetails WithConstraints(Constraints constraints)
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

    public BillingAgreementDetails WithCreationTimestamp(DateTime creationTimestamp)
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

    public BillingAgreementDetails WithExpirationTimestamp(DateTime expirationTimestamp)
    {
      this.expirationTimestampField = new DateTime?(expirationTimestamp);
      return this;
    }

    public bool IsSetExpirationTimestamp()
    {
      return this.expirationTimestampField.HasValue;
    }

    [XmlElement(ElementName = "BillingAgreementConsent")]
    public bool BillingAgreementConsent
    {
      get
      {
        return this.billingAgreementConsentField.GetValueOrDefault();
      }
      set
      {
        this.billingAgreementConsentField = new bool?(value);
      }
    }

    public BillingAgreementDetails WithBillingAgreementConsent(bool billingAgreementConsent)
    {
      this.billingAgreementConsentField = new bool?(billingAgreementConsent);
      return this;
    }

    public bool IsSetBillingAgreementConsent()
    {
      return this.billingAgreementConsentField.HasValue;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetAmazonBillingAgreementId())
      {
        stringBuilder.Append("<AmazonBillingAgreementId>");
        stringBuilder.Append(this.EscapeXML(this.AmazonBillingAgreementId));
        stringBuilder.Append("</AmazonBillingAgreementId>");
      }
      if (this.IsSetOrderLanguage())
      {
        stringBuilder.Append("<OrderLanguage>");
        stringBuilder.Append(this.EscapeXML(this.OrderLanguage));
        stringBuilder.Append("</OrderLanguage>");
      }
      if (this.IsSetBillingAgreementLimits())
      {
        BillingAgreementLimits billingAgreementLimits = this.BillingAgreementLimits;
        stringBuilder.Append("<BillingAgreementLimits>");
        stringBuilder.Append(billingAgreementLimits.ToXMLFragment());
        stringBuilder.Append("</BillingAgreementLimits>");
      }
      if (this.IsSetBuyer())
      {
        Buyer buyer = this.Buyer;
        stringBuilder.Append("<Buyer>");
        stringBuilder.Append(buyer.ToXMLFragment());
        stringBuilder.Append("</Buyer>");
      }
      if (this.IsSetSellerNote())
      {
        stringBuilder.Append("<SellerNote>");
        stringBuilder.Append(this.EscapeXML(this.SellerNote));
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
      if (this.IsSetReleaseEnvironment())
      {
        stringBuilder.Append("<ReleaseEnvironment>");
        stringBuilder.Append((object) this.ReleaseEnvironment);
        stringBuilder.Append("</ReleaseEnvironment>");
      }
      if (this.IsSetSellerBillingAgreementAttributes())
      {
        SellerBillingAgreementAttributes agreementAttributes = this.SellerBillingAgreementAttributes;
        stringBuilder.Append("<SellerBillingAgreementAttributes>");
        stringBuilder.Append(agreementAttributes.ToXMLFragment());
        stringBuilder.Append("</SellerBillingAgreementAttributes>");
      }
      if (this.IsSetBillingAgreementStatus())
      {
        BillingAgreementStatus billingAgreementStatus = this.BillingAgreementStatus;
        stringBuilder.Append("<BillingAgreementStatus>");
        stringBuilder.Append(billingAgreementStatus.ToXMLFragment());
        stringBuilder.Append("</BillingAgreementStatus>");
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
      if (this.IsSetBillingAgreementConsent())
      {
        stringBuilder.Append("<BillingAgreementConsent>");
        stringBuilder.Append(this.BillingAgreementConsent);
        stringBuilder.Append("</BillingAgreementConsent>");
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
