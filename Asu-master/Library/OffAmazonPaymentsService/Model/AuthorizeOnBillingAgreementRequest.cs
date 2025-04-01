// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.AuthorizeOnBillingAgreementRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class AuthorizeOnBillingAgreementRequest
  {
    private string sellerIdField;
    private string amazonBillingAgreementIdField;
    private string authorizationReferenceIdField;
    private Price authorizationAmountField;
    private string sellerAuthorizationNoteField;
    private uint? transactionTimeoutField;
    private bool? captureNowField;
    private string softDescriptorField;
    private string sellerNoteField;
    private string platformIdField;
    private SellerOrderAttributes sellerOrderAttributesField;
    private bool? inheritShippingAddressField;
    private string mwsAuthTokenField;

    [XmlElement(ElementName = "SellerId")]
    public string SellerId
    {
      get
      {
        return this.sellerIdField;
      }
      set
      {
        this.sellerIdField = value;
      }
    }

    public AuthorizeOnBillingAgreementRequest WithSellerId(string sellerId)
    {
      this.sellerIdField = sellerId;
      return this;
    }

    public bool IsSetSellerId()
    {
      return this.sellerIdField != null;
    }

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

    public AuthorizeOnBillingAgreementRequest WithAmazonBillingAgreementId(string amazonBillingAgreementId)
    {
      this.amazonBillingAgreementIdField = amazonBillingAgreementId;
      return this;
    }

    public bool IsSetAmazonBillingAgreementId()
    {
      return this.amazonBillingAgreementIdField != null;
    }

    [XmlElement(ElementName = "AuthorizationReferenceId")]
    public string AuthorizationReferenceId
    {
      get
      {
        return this.authorizationReferenceIdField;
      }
      set
      {
        this.authorizationReferenceIdField = value;
      }
    }

    public AuthorizeOnBillingAgreementRequest WithAuthorizationReferenceId(string authorizationReferenceId)
    {
      this.authorizationReferenceIdField = authorizationReferenceId;
      return this;
    }

    public bool IsSetAuthorizationReferenceId()
    {
      return this.authorizationReferenceIdField != null;
    }

    [XmlElement(ElementName = "AuthorizationAmount")]
    public Price AuthorizationAmount
    {
      get
      {
        return this.authorizationAmountField;
      }
      set
      {
        this.authorizationAmountField = value;
      }
    }

    public AuthorizeOnBillingAgreementRequest WithAuthorizationAmount(Price authorizationAmount)
    {
      this.authorizationAmountField = authorizationAmount;
      return this;
    }

    public bool IsSetAuthorizationAmount()
    {
      return this.authorizationAmountField != null;
    }

    [XmlElement(ElementName = "SellerAuthorizationNote")]
    public string SellerAuthorizationNote
    {
      get
      {
        return this.sellerAuthorizationNoteField;
      }
      set
      {
        this.sellerAuthorizationNoteField = value;
      }
    }

    public AuthorizeOnBillingAgreementRequest WithSellerAuthorizationNote(string sellerAuthorizationNote)
    {
      this.sellerAuthorizationNoteField = sellerAuthorizationNote;
      return this;
    }

    public bool IsSetSellerAuthorizationNote()
    {
      return this.sellerAuthorizationNoteField != null;
    }

    [XmlElement(ElementName = "TransactionTimeout")]
    public uint? TransactionTimeout
    {
      get
      {
        return this.transactionTimeoutField;
      }
      set
      {
        this.transactionTimeoutField = value;
      }
    }

    public AuthorizeOnBillingAgreementRequest WithTransactionTimeout(uint? transactionTimeout)
    {
      this.transactionTimeoutField = transactionTimeout;
      return this;
    }

    public bool IsSetTransactionTimeout()
    {
      return this.transactionTimeoutField.HasValue;
    }

    [XmlElement(ElementName = "CaptureNow")]
    public bool CaptureNow
    {
      get
      {
        return this.captureNowField.GetValueOrDefault();
      }
      set
      {
        this.captureNowField = new bool?(value);
      }
    }

    public AuthorizeOnBillingAgreementRequest WithCaptureNow(bool captureNow)
    {
      this.captureNowField = new bool?(captureNow);
      return this;
    }

    public bool IsSetCaptureNow()
    {
      return this.captureNowField.HasValue;
    }

    [XmlElement(ElementName = "SoftDescriptor")]
    public string SoftDescriptor
    {
      get
      {
        return this.softDescriptorField;
      }
      set
      {
        this.softDescriptorField = value;
      }
    }

    public AuthorizeOnBillingAgreementRequest WithSoftDescriptor(string softDescriptor)
    {
      this.softDescriptorField = softDescriptor;
      return this;
    }

    public bool IsSetSoftDescriptor()
    {
      return this.softDescriptorField != null;
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

    public AuthorizeOnBillingAgreementRequest WithSellerNote(string sellerNote)
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

    public AuthorizeOnBillingAgreementRequest WithPlatformId(string platformId)
    {
      this.platformIdField = platformId;
      return this;
    }

    public bool IsSetPlatformId()
    {
      return this.platformIdField != null;
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

    public AuthorizeOnBillingAgreementRequest WithSellerOrderAttributes(SellerOrderAttributes sellerOrderAttributes)
    {
      this.sellerOrderAttributesField = sellerOrderAttributes;
      return this;
    }

    public bool IsSetSellerOrderAttributes()
    {
      return this.sellerOrderAttributesField != null;
    }

    [XmlElement(ElementName = "InheritShippingAddress")]
    public bool InheritShippingAddress
    {
      get
      {
        return this.inheritShippingAddressField.GetValueOrDefault();
      }
      set
      {
        this.inheritShippingAddressField = new bool?(value);
      }
    }

    public AuthorizeOnBillingAgreementRequest WithInheritShippingAddress(bool inheritShippingAddress)
    {
      this.inheritShippingAddressField = new bool?(inheritShippingAddress);
      return this;
    }

    public bool IsSetInheritShippingAddress()
    {
      return this.inheritShippingAddressField.HasValue;
    }

    [XmlElement(ElementName = "MWSAuthToken")]
    public string MWSAuthToken
    {
      get
      {
        return this.mwsAuthTokenField;
      }
      set
      {
        this.mwsAuthTokenField = value;
      }
    }

    public AuthorizeOnBillingAgreementRequest WithMWSAuthToken(string mwsAuthToken)
    {
      this.mwsAuthTokenField = mwsAuthToken;
      return this;
    }

    public bool IsSetMWSAuthToken()
    {
      return this.mwsAuthTokenField != null;
    }
  }
}
