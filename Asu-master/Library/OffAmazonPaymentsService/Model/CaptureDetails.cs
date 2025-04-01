// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.CaptureDetails
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System;
using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class CaptureDetails
  {
    private string amazonCaptureIdField;
    private string captureReferenceIdField;
    private string sellerCaptureNoteField;
    private Price captureAmountField;
    private Price refundedAmountField;
    private Price captureFeeField;
    private IdList idListField;
    private DateTime? creationTimestampField;
    private Status captureStatusField;
    private string softDescriptorField;
    private ProviderCreditSummaryList providerCreditSummaryListField;

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

    public CaptureDetails WithAmazonCaptureId(string amazonCaptureId)
    {
      this.amazonCaptureIdField = amazonCaptureId;
      return this;
    }

    public bool IsSetAmazonCaptureId()
    {
      return this.amazonCaptureIdField != null;
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

    public CaptureDetails WithCaptureReferenceId(string captureReferenceId)
    {
      this.captureReferenceIdField = captureReferenceId;
      return this;
    }

    public bool IsSetCaptureReferenceId()
    {
      return this.captureReferenceIdField != null;
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

    public CaptureDetails WithSellerCaptureNote(string sellerCaptureNote)
    {
      this.sellerCaptureNoteField = sellerCaptureNote;
      return this;
    }

    public bool IsSetSellerCaptureNote()
    {
      return this.sellerCaptureNoteField != null;
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

    public CaptureDetails WithCaptureAmount(Price captureAmount)
    {
      this.captureAmountField = captureAmount;
      return this;
    }

    public bool IsSetCaptureAmount()
    {
      return this.captureAmountField != null;
    }

    [XmlElement(ElementName = "RefundedAmount")]
    public Price RefundedAmount
    {
      get
      {
        return this.refundedAmountField;
      }
      set
      {
        this.refundedAmountField = value;
      }
    }

    public CaptureDetails WithRefundedAmount(Price refundedAmount)
    {
      this.refundedAmountField = refundedAmount;
      return this;
    }

    public bool IsSetRefundedAmount()
    {
      return this.refundedAmountField != null;
    }

    [XmlElement(ElementName = "CaptureFee")]
    public Price CaptureFee
    {
      get
      {
        return this.captureFeeField;
      }
      set
      {
        this.captureFeeField = value;
      }
    }

    public CaptureDetails WithCaptureFee(Price captureFee)
    {
      this.captureFeeField = captureFee;
      return this;
    }

    public bool IsSetCaptureFee()
    {
      return this.captureFeeField != null;
    }

    [XmlElement(ElementName = "IdList")]
    public IdList IdList
    {
      get
      {
        return this.idListField;
      }
      set
      {
        this.idListField = value;
      }
    }

    public CaptureDetails WithIdList(IdList idList)
    {
      this.idListField = idList;
      return this;
    }

    public bool IsSetIdList()
    {
      return this.idListField != null;
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

    public CaptureDetails WithCreationTimestamp(DateTime creationTimestamp)
    {
      this.creationTimestampField = new DateTime?(creationTimestamp);
      return this;
    }

    public bool IsSetCreationTimestamp()
    {
      return this.creationTimestampField.HasValue;
    }

    [XmlElement(ElementName = "CaptureStatus")]
    public Status CaptureStatus
    {
      get
      {
        return this.captureStatusField;
      }
      set
      {
        this.captureStatusField = value;
      }
    }

    public CaptureDetails WithCaptureStatus(Status captureStatus)
    {
      this.captureStatusField = captureStatus;
      return this;
    }

    public bool IsSetCaptureStatus()
    {
      return this.captureStatusField != null;
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

    public CaptureDetails WithSoftDescriptor(string softDescriptor)
    {
      this.softDescriptorField = softDescriptor;
      return this;
    }

    public bool IsSetSoftDescriptor()
    {
      return this.softDescriptorField != null;
    }

    [XmlElement(ElementName = "ProviderCreditSummaryList")]
    public ProviderCreditSummaryList ProviderCreditSummaryList
    {
      get
      {
        return this.providerCreditSummaryListField;
      }
      set
      {
        this.providerCreditSummaryListField = value;
      }
    }

    public CaptureDetails WithProviderCreditSummaryList(ProviderCreditSummaryList providerCreditSummaryList)
    {
      this.providerCreditSummaryListField = providerCreditSummaryList;
      return this;
    }

    public bool IsSetProviderCreditSummaryList()
    {
      return this.providerCreditSummaryListField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetAmazonCaptureId())
      {
        stringBuilder.Append("<AmazonCaptureId>");
        stringBuilder.Append(this.AmazonCaptureId);
        stringBuilder.Append("</AmazonCaptureId>");
      }
      if (this.IsSetCaptureReferenceId())
      {
        stringBuilder.Append("<CaptureReferenceId>");
        stringBuilder.Append(this.CaptureReferenceId);
        stringBuilder.Append("</CaptureReferenceId>");
      }
      if (this.IsSetSellerCaptureNote())
      {
        stringBuilder.Append("<SellerCaptureNote>");
        stringBuilder.Append(this.EscapeXML(this.SellerCaptureNote));
        stringBuilder.Append("</SellerCaptureNote>");
      }
      if (this.IsSetCaptureAmount())
      {
        Price captureAmount = this.CaptureAmount;
        stringBuilder.Append("<CaptureAmount>");
        stringBuilder.Append(captureAmount.ToXMLFragment());
        stringBuilder.Append("</CaptureAmount>");
      }
      if (this.IsSetRefundedAmount())
      {
        Price refundedAmount = this.RefundedAmount;
        stringBuilder.Append("<RefundedAmount>");
        stringBuilder.Append(refundedAmount.ToXMLFragment());
        stringBuilder.Append("</RefundedAmount>");
      }
      if (this.IsSetCaptureFee())
      {
        Price captureFee = this.CaptureFee;
        stringBuilder.Append("<CaptureFee>");
        stringBuilder.Append(captureFee.ToXMLFragment());
        stringBuilder.Append("</CaptureFee>");
      }
      if (this.IsSetIdList())
      {
        IdList idList = this.IdList;
        stringBuilder.Append("<IdList>");
        stringBuilder.Append(idList.ToXMLFragment());
        stringBuilder.Append("</IdList>");
      }
      if (this.IsSetCreationTimestamp())
      {
        stringBuilder.Append("<CreationTimestamp>");
        stringBuilder.Append((object) this.CreationTimestamp);
        stringBuilder.Append("</CreationTimestamp>");
      }
      if (this.IsSetCaptureStatus())
      {
        Status captureStatus = this.CaptureStatus;
        stringBuilder.Append("<CaptureStatus>");
        stringBuilder.Append(captureStatus.ToXMLFragment());
        stringBuilder.Append("</CaptureStatus>");
      }
      if (this.IsSetSoftDescriptor())
      {
        stringBuilder.Append("<SoftDescriptor>");
        stringBuilder.Append(this.EscapeXML(this.SoftDescriptor));
        stringBuilder.Append("</SoftDescriptor>");
      }
      if (this.IsSetProviderCreditSummaryList())
      {
        ProviderCreditSummaryList creditSummaryList = this.ProviderCreditSummaryList;
        stringBuilder.Append("<ProviderCreditSummaryList>");
        stringBuilder.Append(creditSummaryList.ToXMLFragment());
        stringBuilder.Append("</ProviderCreditSummaryList>");
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
