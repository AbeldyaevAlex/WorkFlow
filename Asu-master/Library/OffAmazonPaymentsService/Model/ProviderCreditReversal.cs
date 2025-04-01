// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ProviderCreditReversal
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class ProviderCreditReversal
  {
    private string providerIdField;
    private Price creditReversalAmountField;

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

    public ProviderCreditReversal WithProviderId(string providerId)
    {
      this.providerIdField = providerId;
      return this;
    }

    public bool IsSetProviderId()
    {
      return this.providerIdField != null;
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

    public ProviderCreditReversal WithCreditReversalAmount(Price creditReversalAmount)
    {
      this.creditReversalAmountField = creditReversalAmount;
      return this;
    }

    public bool IsSetCreditReversalAmount()
    {
      return this.creditReversalAmountField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetProviderId())
      {
        stringBuilder.Append("<ProviderId>");
        stringBuilder.Append(this.ProviderId);
        stringBuilder.Append("</ProviderId>");
      }
      if (this.IsSetCreditReversalAmount())
      {
        Price creditReversalAmount = this.CreditReversalAmount;
        stringBuilder.Append("<CreditReversalAmount>");
        stringBuilder.Append(creditReversalAmount.ToXMLFragment());
        stringBuilder.Append("</CreditReversalAmount>");
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
