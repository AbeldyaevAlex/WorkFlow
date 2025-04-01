// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ProviderCreditReversalDetails
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
  public class ProviderCreditReversalDetails
  {
    private string amazonProviderCreditReversalIdField;
    private string sellerIdField;
    private string providerIdField;
    private string creditReversalReferenceIdField;
    private Price creditReversalAmountField;
    private DateTime? creationTimestampField;
    private Status creditReversalStatusField;
    private string creditReversalNoteField;

    [XmlElement(ElementName = "AmazonProviderCreditReversalId")]
    public string AmazonProviderCreditReversalId
    {
      get
      {
        return this.amazonProviderCreditReversalIdField;
      }
      set
      {
        this.amazonProviderCreditReversalIdField = value;
      }
    }

    public ProviderCreditReversalDetails WithAmazonProviderCreditReversalId(string amazonProviderCreditReversalId)
    {
      this.amazonProviderCreditReversalIdField = amazonProviderCreditReversalId;
      return this;
    }

    public bool IsSetAmazonProviderCreditReversalId()
    {
      return this.amazonProviderCreditReversalIdField != null;
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

    public ProviderCreditReversalDetails WithSellerId(string sellerId)
    {
      this.sellerIdField = sellerId;
      return this;
    }

    public bool IsSetSellerId()
    {
      return this.sellerIdField != null;
    }

    [XmlElement(ElementName = "ProviderId")]
    public string ProviderId
    {
      get
      {
        return this.providerIdField;
      }
      set
      {
        this.providerIdField = value;
      }
    }

    public ProviderCreditReversalDetails WithProviderId(string providerId)
    {
      this.providerIdField = providerId;
      return this;
    }

    public bool IsSetProviderId()
    {
      return this.providerIdField != null;
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

    public ProviderCreditReversalDetails WithCreditReversalReferenceId(string creditReversalReferenceId)
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

    public ProviderCreditReversalDetails WithCreditReversalAmount(Price creditReversalAmount)
    {
      this.creditReversalAmountField = creditReversalAmount;
      return this;
    }

    public bool IsSetCreditReversalAmount()
    {
      return this.creditReversalAmountField != null;
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

    public ProviderCreditReversalDetails WithCreationTimestamp(DateTime creationTimestamp)
    {
      this.creationTimestampField = new DateTime?(creationTimestamp);
      return this;
    }

    public bool IsSetCreationTimestamp()
    {
      return this.creationTimestampField.HasValue;
    }

    [XmlElement(ElementName = "CreditReversalStatus")]
    public Status CreditReversalStatus
    {
      get
      {
        return this.creditReversalStatusField;
      }
      set
      {
        this.creditReversalStatusField = value;
      }
    }

    public ProviderCreditReversalDetails WithCreditReversalStatus(Status creditReversalStatus)
    {
      this.creditReversalStatusField = creditReversalStatus;
      return this;
    }

    public bool IsSetCreditReversalStatus()
    {
      return this.creditReversalStatusField != null;
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

    public ProviderCreditReversalDetails WithCreditReversalNote(string creditReversalNote)
    {
      this.creditReversalNoteField = creditReversalNote;
      return this;
    }

    public bool IsSetCreditReversalNote()
    {
      return this.creditReversalNoteField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetAmazonProviderCreditReversalId())
      {
        stringBuilder.Append("<AmazonProviderCreditReversalId>");
        stringBuilder.Append(this.AmazonProviderCreditReversalId);
        stringBuilder.Append("</AmazonProviderCreditReversalId>");
      }
      if (this.IsSetSellerId())
      {
        stringBuilder.Append("<SellerId>");
        stringBuilder.Append(this.SellerId);
        stringBuilder.Append("</SellerId>");
      }
      if (this.IsSetProviderId())
      {
        stringBuilder.Append("<ProviderId>");
        stringBuilder.Append(this.ProviderId);
        stringBuilder.Append("</ProviderId>");
      }
      if (this.IsSetCreditReversalReferenceId())
      {
        stringBuilder.Append("<CreditReversalReferenceId>");
        stringBuilder.Append(this.CreditReversalReferenceId);
        stringBuilder.Append("</CreditReversalReferenceId>");
      }
      if (this.IsSetCreditReversalAmount())
      {
        Price creditReversalAmount = this.CreditReversalAmount;
        stringBuilder.Append("<CreditReversalAmount>");
        stringBuilder.Append(creditReversalAmount.ToXMLFragment());
        stringBuilder.Append("</CreditReversalAmount>");
      }
      if (this.IsSetCreationTimestamp())
      {
        stringBuilder.Append("<CreationTimestamp>");
        stringBuilder.Append((object) this.CreationTimestamp);
        stringBuilder.Append("</CreationTimestamp>");
      }
      if (this.IsSetCreditReversalStatus())
      {
        Status creditReversalStatus = this.CreditReversalStatus;
        stringBuilder.Append("<CreditReversalStatus>");
        stringBuilder.Append(creditReversalStatus.ToXMLFragment());
        stringBuilder.Append("</CreditReversalStatus>");
      }
      if (this.IsSetCreditReversalNote())
      {
        stringBuilder.Append("<CreditReversalNote>");
        stringBuilder.Append(this.EscapeXML(this.CreditReversalNote));
        stringBuilder.Append("</CreditReversalNote>");
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
