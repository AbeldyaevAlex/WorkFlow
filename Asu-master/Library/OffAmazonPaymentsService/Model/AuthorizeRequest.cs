// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.AuthorizeRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class AuthorizeRequest
  {
    private string sellerIdField;
    private string amazonOrderReferenceIdField;
    private string authorizationReferenceIdField;
    private Price authorizationAmountField;
    private string sellerAuthorizationNoteField;
    private OrderItemCategories orderItemCategoriesField;
    private uint? transactionTimeoutField;
    private bool? captureNowField;
    private string softDescriptorField;
    private ProviderCreditList providerCreditListField;
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

    public AuthorizeRequest WithSellerId(string sellerId)
    {
      this.sellerIdField = sellerId;
      return this;
    }

    public bool IsSetSellerId()
    {
      return this.sellerIdField != null;
    }

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

    public AuthorizeRequest WithAmazonOrderReferenceId(string amazonOrderReferenceId)
    {
      this.amazonOrderReferenceIdField = amazonOrderReferenceId;
      return this;
    }

    public bool IsSetAmazonOrderReferenceId()
    {
      return this.amazonOrderReferenceIdField != null;
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

    public AuthorizeRequest WithAuthorizationReferenceId(string authorizationReferenceId)
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

    public AuthorizeRequest WithAuthorizationAmount(Price authorizationAmount)
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

    public AuthorizeRequest WithSellerAuthorizationNote(string sellerAuthorizationNote)
    {
      this.sellerAuthorizationNoteField = sellerAuthorizationNote;
      return this;
    }

    public bool IsSetSellerAuthorizationNote()
    {
      return this.sellerAuthorizationNoteField != null;
    }

    [XmlElement(ElementName = "OrderItemCategories")]
    public OrderItemCategories OrderItemCategories
    {
      get
      {
        return this.orderItemCategoriesField;
      }
      set
      {
        this.orderItemCategoriesField = value;
      }
    }

    public AuthorizeRequest WithOrderItemCategories(OrderItemCategories orderItemCategories)
    {
      this.orderItemCategoriesField = orderItemCategories;
      return this;
    }

    public bool IsSetOrderItemCategories()
    {
      return this.orderItemCategoriesField != null;
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

    public AuthorizeRequest WithTransactionTimeout(uint? transactionTimeout)
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

    public AuthorizeRequest WithCaptureNow(bool captureNow)
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

    public AuthorizeRequest WithSoftDescriptor(string softDescriptor)
    {
      this.softDescriptorField = softDescriptor;
      return this;
    }

    public bool IsSetSoftDescriptor()
    {
      return this.softDescriptorField != null;
    }

    [XmlElement(ElementName = "ProviderCreditList")]
    public ProviderCreditList ProviderCreditList
    {
      get
      {
        return this.providerCreditListField;
      }
      set
      {
        this.providerCreditListField = value;
      }
    }

    public AuthorizeRequest WithProviderCreditList(ProviderCreditList providerCreditList)
    {
      this.providerCreditListField = providerCreditList;
      return this;
    }

    public bool IsSetProviderCreditList()
    {
      return this.providerCreditListField != null;
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

    public AuthorizeRequest WithMWSAuthToken(string mwsAuthToken)
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
