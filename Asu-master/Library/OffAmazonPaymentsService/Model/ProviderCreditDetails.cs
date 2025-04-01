// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ProviderCreditDetails
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
  public class ProviderCreditDetails
  {
    private string amazonProviderCreditIdField;
    private string sellerIdField;
    private string providerIdField;
    private string creditReferenceIdField;
    private Price creditAmountField;
    private Price creditReversalAmountField;
    private IdList creditReversalIdListField;
    private DateTime? creationTimestampField;
    private Status creditStatusField;

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

    public ProviderCreditDetails WithAmazonProviderCreditId(string amazonProviderCreditId)
    {
      this.amazonProviderCreditIdField = amazonProviderCreditId;
      return this;
    }

    public bool IsSetAmazonProviderCreditId()
    {
      return this.amazonProviderCreditIdField != null;
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

    public ProviderCreditDetails WithSellerId(string sellerId)
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

    public ProviderCreditDetails WithProviderId(string providerId)
    {
      this.providerIdField = providerId;
      return this;
    }

    public bool IsSetProviderId()
    {
      return this.providerIdField != null;
    }

    [XmlElement(ElementName = "CreditReferenceId")]
    public string CreditReferenceId
    {
      get
      {
        return this.creditReferenceIdField;
      }
      set
      {
        this.creditReferenceIdField = value;
      }
    }

    public ProviderCreditDetails WithCreditReferenceId(string creditReferenceId)
    {
      this.creditReferenceIdField = creditReferenceId;
      return this;
    }

    public bool IsSetCreditReferenceId()
    {
      return this.creditReferenceIdField != null;
    }

    [XmlElement(ElementName = "CreditAmount")]
    public Price CreditAmount
    {
      get
      {
        return this.creditAmountField;
      }
      set
      {
        this.creditAmountField = value;
      }
    }

    public ProviderCreditDetails WithCreditAmount(Price creditAmount)
    {
      this.creditAmountField = creditAmount;
      return this;
    }

    public bool IsSetCreditAmount()
    {
      return this.creditAmountField != null;
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

    public ProviderCreditDetails WithCreditReversalAmount(Price creditReversalAmount)
    {
      this.creditReversalAmountField = creditReversalAmount;
      return this;
    }

    public bool IsSetCreditReversalAmount()
    {
      return this.creditReversalAmountField != null;
    }

    [XmlElement(ElementName = "CreditReversalIdList")]
    public IdList CreditReversalIdList
    {
      get
      {
        return this.creditReversalIdListField;
      }
      set
      {
        this.creditReversalIdListField = value;
      }
    }

    public ProviderCreditDetails WithCreditReversalIdList(IdList creditReversalIdList)
    {
      this.creditReversalIdListField = creditReversalIdList;
      return this;
    }

    public bool IsSetCreditReversalIdList()
    {
      return this.creditReversalIdListField != null;
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

    public ProviderCreditDetails WithCreationTimestamp(DateTime creationTimestamp)
    {
      this.creationTimestampField = new DateTime?(creationTimestamp);
      return this;
    }

    public bool IsSetCreationTimestamp()
    {
      return this.creationTimestampField.HasValue;
    }

    [XmlElement(ElementName = "CreditStatus")]
    public Status CreditStatus
    {
      get
      {
        return this.creditStatusField;
      }
      set
      {
        this.creditStatusField = value;
      }
    }

    public ProviderCreditDetails WithCreditStatus(Status creditStatus)
    {
      this.creditStatusField = creditStatus;
      return this;
    }

    public bool IsSetCreditStatus()
    {
      return this.creditStatusField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetAmazonProviderCreditId())
      {
        stringBuilder.Append("<AmazonProviderCreditId>");
        stringBuilder.Append(this.AmazonProviderCreditId);
        stringBuilder.Append("</AmazonProviderCreditId>");
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
      if (this.IsSetCreditReferenceId())
      {
        stringBuilder.Append("<CreditReferenceId>");
        stringBuilder.Append(this.CreditReferenceId);
        stringBuilder.Append("</CreditReferenceId>");
      }
      if (this.IsSetCreditAmount())
      {
        Price creditAmount = this.CreditAmount;
        stringBuilder.Append("<CreditAmount>");
        stringBuilder.Append(creditAmount.ToXMLFragment());
        stringBuilder.Append("</CreditAmount>");
      }
      if (this.IsSetCreditReversalAmount())
      {
        Price creditReversalAmount = this.CreditReversalAmount;
        stringBuilder.Append("<CreditReversalAmount>");
        stringBuilder.Append(creditReversalAmount.ToXMLFragment());
        stringBuilder.Append("</CreditReversalAmount>");
      }
      if (this.IsSetCreditReversalIdList())
      {
        IdList creditReversalIdList = this.CreditReversalIdList;
        stringBuilder.Append("<CreditReversalIdList>");
        stringBuilder.Append(creditReversalIdList.ToXMLFragment());
        stringBuilder.Append("</CreditReversalIdList>");
      }
      if (this.IsSetCreationTimestamp())
      {
        stringBuilder.Append("<CreationTimestamp>");
        stringBuilder.Append((object) this.CreationTimestamp);
        stringBuilder.Append("</CreationTimestamp>");
      }
      if (this.IsSetCreditStatus())
      {
        Status creditStatus = this.CreditStatus;
        stringBuilder.Append("<CreditStatus>");
        stringBuilder.Append(creditStatus.ToXMLFragment());
        stringBuilder.Append("</CreditStatus>");
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
