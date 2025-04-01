// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.RefundRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class RefundRequest
  {
    private string sellerIdField;
    private string amazonCaptureIdField;
    private string refundReferenceIdField;
    private Price refundAmountField;
    private string sellerRefundNoteField;
    private string softDescriptorField;
    private ProviderCreditReversalList providerCreditReversalListField;
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

    public RefundRequest WithSellerId(string sellerId)
    {
      this.sellerIdField = sellerId;
      return this;
    }

    public bool IsSetSellerId()
    {
      return this.sellerIdField != null;
    }

    [XmlElement(ElementName = "AmazonCaptureId")]
    public string AmazonCaptureId
    {
      get
      {
        return this.amazonCaptureIdField;
      }
      set
      {
        this.amazonCaptureIdField = value;
      }
    }

    public RefundRequest WithAmazonCaptureId(string amazonCaptureId)
    {
      this.amazonCaptureIdField = amazonCaptureId;
      return this;
    }

    public bool IsSetAmazonCaptureId()
    {
      return this.amazonCaptureIdField != null;
    }

    [XmlElement(ElementName = "RefundReferenceId")]
    public string RefundReferenceId
    {
      get
      {
        return this.refundReferenceIdField;
      }
      set
      {
        this.refundReferenceIdField = value;
      }
    }

    public RefundRequest WithRefundReferenceId(string refundReferenceId)
    {
      this.refundReferenceIdField = refundReferenceId;
      return this;
    }

    public bool IsSetRefundReferenceId()
    {
      return this.refundReferenceIdField != null;
    }

    [XmlElement(ElementName = "RefundAmount")]
    public Price RefundAmount
    {
      get
      {
        return this.refundAmountField;
      }
      set
      {
        this.refundAmountField = value;
      }
    }

    public RefundRequest WithRefundAmount(Price refundAmount)
    {
      this.refundAmountField = refundAmount;
      return this;
    }

    public bool IsSetRefundAmount()
    {
      return this.refundAmountField != null;
    }

    [XmlElement(ElementName = "SellerRefundNote")]
    public string SellerRefundNote
    {
      get
      {
        return this.sellerRefundNoteField;
      }
      set
      {
        this.sellerRefundNoteField = value;
      }
    }

    public RefundRequest WithSellerRefundNote(string sellerRefundNote)
    {
      this.sellerRefundNoteField = sellerRefundNote;
      return this;
    }

    public bool IsSetSellerRefundNote()
    {
      return this.sellerRefundNoteField != null;
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

    public RefundRequest WithSoftDescriptor(string softDescriptor)
    {
      this.softDescriptorField = softDescriptor;
      return this;
    }

    public bool IsSetSoftDescriptor()
    {
      return this.softDescriptorField != null;
    }

    [XmlElement(ElementName = "ProviderCreditReversalList")]
    public ProviderCreditReversalList ProviderCreditReversalList
    {
      get
      {
        return this.providerCreditReversalListField;
      }
      set
      {
        this.providerCreditReversalListField = value;
      }
    }

    public RefundRequest WithProviderCreditReversalList(ProviderCreditReversalList providerCreditReversalList)
    {
      this.providerCreditReversalListField = providerCreditReversalList;
      return this;
    }

    public bool IsSetProviderCreditReversalList()
    {
      return this.providerCreditReversalListField != null;
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

    public RefundRequest WithMWSAuthToken(string mwsAuthToken)
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
