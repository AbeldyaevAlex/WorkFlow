// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ReverseProviderCreditRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class ReverseProviderCreditRequest
  {
    private string sellerIdField;
    private string amazonProviderCreditIdField;
    private string creditReversalReferenceIdField;
    private Price creditReversalAmountField;
    private string creditReversalNoteField;
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

    public ReverseProviderCreditRequest WithSellerId(string sellerId)
    {
      this.sellerIdField = sellerId;
      return this;
    }

    public bool IsSetSellerId()
    {
      return this.sellerIdField != null;
    }

    [XmlElement(ElementName = "AmazonProviderCreditId")]
    public string AmazonProviderCreditId
    {
      get
      {
        return this.amazonProviderCreditIdField;
      }
      set
      {
        this.amazonProviderCreditIdField = value;
      }
    }

    public ReverseProviderCreditRequest WithAmazonProviderCreditId(string amazonProviderCreditId)
    {
      this.amazonProviderCreditIdField = amazonProviderCreditId;
      return this;
    }

    public bool IsSetAmazonProviderCreditId()
    {
      return this.amazonProviderCreditIdField != null;
    }

    [XmlElement(ElementName = "CreditReversalReferenceId")]
    public string CreditReversalReferenceId
    {
      get
      {
        return this.creditReversalReferenceIdField;
      }
      set
      {
        this.creditReversalReferenceIdField = value;
      }
    }

    public ReverseProviderCreditRequest WithCreditReversalReferenceId(string creditReversalReferenceId)
    {
      this.creditReversalReferenceIdField = creditReversalReferenceId;
      return this;
    }

    public bool IsSetCreditReversalReferenceId()
    {
      return this.creditReversalReferenceIdField != null;
    }

    [XmlElement(ElementName = "CreditReversalAmount")]
    public Price CreditReversalAmount
    {
      get
      {
        return this.creditReversalAmountField;
      }
      set
      {
        this.creditReversalAmountField = value;
      }
    }

    public ReverseProviderCreditRequest WithCreditReversalAmount(Price creditReversalAmount)
    {
      this.creditReversalAmountField = creditReversalAmount;
      return this;
    }

    public bool IsSetCreditReversalAmount()
    {
      return this.creditReversalAmountField != null;
    }

    [XmlElement(ElementName = "CreditReversalNote")]
    public string CreditReversalNote
    {
      get
      {
        return this.creditReversalNoteField;
      }
      set
      {
        this.creditReversalNoteField = value;
      }
    }

    public ReverseProviderCreditRequest WithCreditReversalNote(string creditReversalNote)
    {
      this.creditReversalNoteField = creditReversalNote;
      return this;
    }

    public bool IsSetCreditReversalNote()
    {
      return this.creditReversalNoteField != null;
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

    public ReverseProviderCreditRequest WithMWSAuthToken(string mwsAuthToken)
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
