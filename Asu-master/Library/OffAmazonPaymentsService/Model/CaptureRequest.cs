// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.CaptureRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class CaptureRequest
  {
    private string sellerIdField;
    private string amazonAuthorizationIdField;
    private string captureReferenceIdField;
    private Price captureAmountField;
    private string sellerCaptureNoteField;
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

    public CaptureRequest WithSellerId(string sellerId)
    {
      this.sellerIdField = sellerId;
      return this;
    }

    public bool IsSetSellerId()
    {
      return this.sellerIdField != null;
    }

    [XmlElement(ElementName = "AmazonAuthorizationId")]
    public string AmazonAuthorizationId
    {
      get
      {
        return this.amazonAuthorizationIdField;
      }
      set
      {
        this.amazonAuthorizationIdField = value;
      }
    }

    public CaptureRequest WithAmazonAuthorizationId(string amazonAuthorizationId)
    {
      this.amazonAuthorizationIdField = amazonAuthorizationId;
      return this;
    }

    public bool IsSetAmazonAuthorizationId()
    {
      return this.amazonAuthorizationIdField != null;
    }

    [XmlElement(ElementName = "CaptureReferenceId")]
    public string CaptureReferenceId
    {
      get
      {
        return this.captureReferenceIdField;
      }
      set
      {
        this.captureReferenceIdField = value;
      }
    }

    public CaptureRequest WithCaptureReferenceId(string captureReferenceId)
    {
      this.captureReferenceIdField = captureReferenceId;
      return this;
    }

    public bool IsSetCaptureReferenceId()
    {
      return this.captureReferenceIdField != null;
    }

    [XmlElement(ElementName = "CaptureAmount")]
    public Price CaptureAmount
    {
      get
      {
        return this.captureAmountField;
      }
      set
      {
        this.captureAmountField = value;
      }
    }

    public CaptureRequest WithCaptureAmount(Price captureAmount)
    {
      this.captureAmountField = captureAmount;
      return this;
    }

    public bool IsSetCaptureAmount()
    {
      return this.captureAmountField != null;
    }

    [XmlElement(ElementName = "SellerCaptureNote")]
    public string SellerCaptureNote
    {
      get
      {
        return this.sellerCaptureNoteField;
      }
      set
      {
        this.sellerCaptureNoteField = value;
      }
    }

    public CaptureRequest WithSellerCaptureNote(string sellerCaptureNote)
    {
      this.sellerCaptureNoteField = sellerCaptureNote;
      return this;
    }

    public bool IsSetSellerCaptureNote()
    {
      return this.sellerCaptureNoteField != null;
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

    public CaptureRequest WithSoftDescriptor(string softDescriptor)
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

    public CaptureRequest WithProviderCreditList(ProviderCreditList providerCreditList)
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

    public CaptureRequest WithMWSAuthToken(string mwsAuthToken)
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
