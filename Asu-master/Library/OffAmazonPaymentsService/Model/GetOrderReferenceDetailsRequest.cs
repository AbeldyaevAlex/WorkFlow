// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.GetOrderReferenceDetailsRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class GetOrderReferenceDetailsRequest
  {
    private string amazonOrderReferenceIdField;
    private string sellerIdField;
    private string addressConsentTokenField;
    private string mwsAuthTokenField;

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

    public GetOrderReferenceDetailsRequest WithAmazonOrderReferenceId(string amazonOrderReferenceId)
    {
      this.amazonOrderReferenceIdField = amazonOrderReferenceId;
      return this;
    }

    public bool IsSetAmazonOrderReferenceId()
    {
      return this.amazonOrderReferenceIdField != null;
    }

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

    public GetOrderReferenceDetailsRequest WithSellerId(string sellerId)
    {
      this.sellerIdField = sellerId;
      return this;
    }

    public bool IsSetSellerId()
    {
      return this.sellerIdField != null;
    }

    [XmlElement(ElementName = "AddressConsentToken")]
    public string AddressConsentToken
    {
      get
      {
        return this.addressConsentTokenField;
      }
      set
      {
        this.addressConsentTokenField = value;
      }
    }

    public GetOrderReferenceDetailsRequest WithAddressConsentToken(string addressConsentToken)
    {
      this.addressConsentTokenField = addressConsentToken;
      return this;
    }

    public bool IsSetAddressConsentToken()
    {
      return this.addressConsentTokenField != null;
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

    public GetOrderReferenceDetailsRequest WithMWSAuthToken(string mwsAuthToken)
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
