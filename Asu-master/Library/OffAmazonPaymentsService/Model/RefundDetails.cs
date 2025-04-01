// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.RefundDetails
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
  public class RefundDetails
  {
    private string amazonRefundIdField;
    private string refundReferenceIdField;
    private string sellerRefundNoteField;
    private RefundType? refundTypeField;
    private Price refundAmountField;
    private Price feeRefundedField;
    private DateTime? creationTimestampField;
    private Status refundStatusField;
    private string softDescriptorField;
    private ProviderCreditReversalSummaryList providerCreditReversalSummaryListField;

    [XmlElement(ElementName = "AmazonRefundId")]
    public string AmazonRefundId
    {
      get
      {
        return this.amazonRefundIdField;
      }
      set
      {
        this.amazonRefundIdField = value;
      }
    }

    public RefundDetails WithAmazonRefundId(string amazonRefundId)
    {
      this.amazonRefundIdField = amazonRefundId;
      return this;
    }

    public bool IsSetAmazonRefundId()
    {
      return this.amazonRefundIdField != null;
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

    public RefundDetails WithRefundReferenceId(string refundReferenceId)
    {
      this.refundReferenceIdField = refundReferenceId;
      return this;
    }

    public bool IsSetRefundReferenceId()
    {
      return this.refundReferenceIdField != null;
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

    public RefundDetails WithSellerRefundNote(string sellerRefundNote)
    {
      this.sellerRefundNoteField = sellerRefundNote;
      return this;
    }

    public bool IsSetSellerRefundNote()
    {
      return this.sellerRefundNoteField != null;
    }

    [XmlElement(ElementName = "RefundType")]
    public RefundType RefundType
    {
      get
      {
        return this.refundTypeField.GetValueOrDefault();
      }
      set
      {
        this.refundTypeField = new RefundType?(value);
      }
    }

    public RefundDetails WithRefundType(RefundType refundType)
    {
      this.refundTypeField = new RefundType?(refundType);
      return this;
    }

    public bool IsSetRefundType()
    {
      return this.refundTypeField.HasValue;
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

    public RefundDetails WithRefundAmount(Price refundAmount)
    {
      this.refundAmountField = refundAmount;
      return this;
    }

    public bool IsSetRefundAmount()
    {
      return this.refundAmountField != null;
    }

    [XmlElement(ElementName = "FeeRefunded")]
    public Price FeeRefunded
    {
      get
      {
        return this.feeRefundedField;
      }
      set
      {
        this.feeRefundedField = value;
      }
    }

    public RefundDetails WithFeeRefunded(Price feeRefunded)
    {
      this.feeRefundedField = feeRefunded;
      return this;
    }

    public bool IsSetFeeRefunded()
    {
      return this.feeRefundedField != null;
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

    public RefundDetails WithCreationTimestamp(DateTime creationTimestamp)
    {
      this.creationTimestampField = new DateTime?(creationTimestamp);
      return this;
    }

    public bool IsSetCreationTimestamp()
    {
      return this.creationTimestampField.HasValue;
    }

    [XmlElement(ElementName = "RefundStatus")]
    public Status RefundStatus
    {
      get
      {
        return this.refundStatusField;
      }
      set
      {
        this.refundStatusField = value;
      }
    }

    public RefundDetails WithRefundStatus(Status refundStatus)
    {
      this.refundStatusField = refundStatus;
      return this;
    }

    public bool IsSetRefundStatus()
    {
      return this.refundStatusField != null;
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

    public RefundDetails WithSoftDescriptor(string softDescriptor)
    {
      this.softDescriptorField = softDescriptor;
      return this;
    }

    public bool IsSetSoftDescriptor()
    {
      return this.softDescriptorField != null;
    }

    [XmlElement(ElementName = "ProviderCreditReversalSummaryList")]
    public ProviderCreditReversalSummaryList ProviderCreditReversalSummaryList
    {
      get
      {
        return this.providerCreditReversalSummaryListField;
      }
      set
      {
        this.providerCreditReversalSummaryListField = value;
      }
    }

    public RefundDetails WithProviderCreditReversalSummaryList(ProviderCreditReversalSummaryList providerCreditReversalSummaryList)
    {
      this.providerCreditReversalSummaryListField = providerCreditReversalSummaryList;
      return this;
    }

    public bool IsSetProviderCreditReversalSummaryList()
    {
      return this.providerCreditReversalSummaryListField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetAmazonRefundId())
      {
        stringBuilder.Append("<AmazonRefundId>");
        stringBuilder.Append(this.AmazonRefundId);
        stringBuilder.Append("</AmazonRefundId>");
      }
      if (this.IsSetRefundReferenceId())
      {
        stringBuilder.Append("<RefundReferenceId>");
        stringBuilder.Append(this.RefundReferenceId);
        stringBuilder.Append("</RefundReferenceId>");
      }
      if (this.IsSetSellerRefundNote())
      {
        stringBuilder.Append("<SellerRefundNote>");
        stringBuilder.Append(this.EscapeXML(this.SellerRefundNote));
        stringBuilder.Append("</SellerRefundNote>");
      }
      if (this.IsSetRefundType())
      {
        stringBuilder.Append("<RefundType>");
        stringBuilder.Append((object) this.RefundType);
        stringBuilder.Append("</RefundType>");
      }
      if (this.IsSetRefundAmount())
      {
        Price refundAmount = this.RefundAmount;
        stringBuilder.Append("<RefundAmount>");
        stringBuilder.Append(refundAmount.ToXMLFragment());
        stringBuilder.Append("</RefundAmount>");
      }
      if (this.IsSetFeeRefunded())
      {
        Price feeRefunded = this.FeeRefunded;
        stringBuilder.Append("<FeeRefunded>");
        stringBuilder.Append(feeRefunded.ToXMLFragment());
        stringBuilder.Append("</FeeRefunded>");
      }
      if (this.IsSetCreationTimestamp())
      {
        stringBuilder.Append("<CreationTimestamp>");
        stringBuilder.Append((object) this.CreationTimestamp);
        stringBuilder.Append("</CreationTimestamp>");
      }
      if (this.IsSetRefundStatus())
      {
        Status refundStatus = this.RefundStatus;
        stringBuilder.Append("<RefundStatus>");
        stringBuilder.Append(refundStatus.ToXMLFragment());
        stringBuilder.Append("</RefundStatus>");
      }
      if (this.IsSetSoftDescriptor())
      {
        stringBuilder.Append("<SoftDescriptor>");
        stringBuilder.Append(this.EscapeXML(this.SoftDescriptor));
        stringBuilder.Append("</SoftDescriptor>");
      }
      if (this.IsSetProviderCreditReversalSummaryList())
      {
        ProviderCreditReversalSummaryList reversalSummaryList = this.ProviderCreditReversalSummaryList;
        stringBuilder.Append("<ProviderCreditReversalSummaryList>");
        stringBuilder.Append(reversalSummaryList.ToXMLFragment());
        stringBuilder.Append("</ProviderCreditReversalSummaryList>");
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
