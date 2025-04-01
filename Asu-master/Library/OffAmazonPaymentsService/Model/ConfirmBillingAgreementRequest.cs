// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ConfirmBillingAgreementRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class ConfirmBillingAgreementRequest
  {
    private string sellerIdField;
    private string amazonBillingAgreementIdField;
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

    public ConfirmBillingAgreementRequest WithSellerId(string sellerId)
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

    public ConfirmBillingAgreementRequest WithAmazonBillingAgreementId(string amazonBillingAgreementId)
    {
      this.amazonBillingAgreementIdField = amazonBillingAgreementId;
      return this;
    }

    public bool IsSetAmazonBillingAgreementId()
    {
      return this.amazonBillingAgreementIdField != null;
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

    public ConfirmBillingAgreementRequest WithMWSAuthToken(string mwsAuthToken)
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
