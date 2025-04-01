// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.OrderTotal
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class OrderTotal
  {
    private string currencyCodeField;
    private string amountField;

    [XmlElement(ElementName = "CurrencyCode")]
    public string CurrencyCode
    {
      get
      {
        return this.currencyCodeField;
      }
      set
      {
        this.currencyCodeField = value;
      }
    }

    public OrderTotal WithCurrencyCode(string currencyCode)
    {
      this.currencyCodeField = currencyCode;
      return this;
    }

    public bool IsSetCurrencyCode()
    {
      return this.currencyCodeField != null;
    }

    [XmlElement(ElementName = "Amount")]
    public string Amount
    {
      get
      {
        return this.amountField;
      }
      set
      {
        this.amountField = value;
      }
    }

    public OrderTotal WithAmount(string amount)
    {
      this.amountField = amount;
      return this;
    }

    public bool IsSetAmount()
    {
      return this.amountField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetCurrencyCode())
      {
        stringBuilder.Append("<CurrencyCode>");
        stringBuilder.Append(this.EscapeXML(this.CurrencyCode));
        stringBuilder.Append("</CurrencyCode>");
      }
      if (this.IsSetAmount())
      {
        stringBuilder.Append("<Amount>");
        stringBuilder.Append(this.EscapeXML(this.Amount));
        stringBuilder.Append("</Amount>");
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
