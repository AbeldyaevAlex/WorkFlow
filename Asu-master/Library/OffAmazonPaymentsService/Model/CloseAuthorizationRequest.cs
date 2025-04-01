// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.CloseAuthorizationRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class CloseAuthorizationRequest
  {
    private string sellerIdField;
    private string amazonAuthorizationIdField;
    private string closureReasonField;
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

    public CloseAuthorizationRequest WithSellerId(string sellerId)
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

    public CloseAuthorizationRequest WithAmazonAuthorizationId(string amazonAuthorizationId)
    {
      this.amazonAuthorizationIdField = amazonAuthorizationId;
      return this;
    }

    public bool IsSetAmazonAuthorizationId()
    {
      return this.amazonAuthorizationIdField != null;
    }

    [XmlElement(ElementName = "ClosureReason")]
    public string ClosureReason
    {
      get
      {
        return this.closureReasonField;
      }
      set
      {
        this.closureReasonField = value;
      }
    }

    public CloseAuthorizationRequest WithClosureReason(string closureReason)
    {
      this.closureReasonField = closureReason;
      return this;
    }

    public bool IsSetClosureReason()
    {
      return this.closureReasonField != null;
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

    public CloseAuthorizationRequest WithMWSAuthToken(string mwsAuthToken)
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
