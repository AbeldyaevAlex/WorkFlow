// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.CloseOrderReferenceRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class CloseOrderReferenceRequest
  {
    private string sellerIdField;
    private string amazonOrderReferenceIdField;
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

    public CloseOrderReferenceRequest WithSellerId(string sellerId)
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

    public CloseOrderReferenceRequest WithAmazonOrderReferenceId(string amazonOrderReferenceId)
    {
      this.amazonOrderReferenceIdField = amazonOrderReferenceId;
      return this;
    }

    public bool IsSetAmazonOrderReferenceId()
    {
      return this.amazonOrderReferenceIdField != null;
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

    public CloseOrderReferenceRequest WithClosureReason(string closureReason)
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

    public CloseOrderReferenceRequest WithMWSAuthToken(string mwsAuthToken)
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
