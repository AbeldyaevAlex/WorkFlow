// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ProviderCreditReversalSummary
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class ProviderCreditReversalSummary
  {
    private string providerIdField;
    private string providerCreditReversalIdField;

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

    public ProviderCreditReversalSummary WithProviderId(string providerId)
    {
      this.providerIdField = providerId;
      return this;
    }

    public bool IsSetProviderId()
    {
      return this.providerIdField != null;
    }

    [XmlElement(ElementName = "ProviderCreditReversalId")]
    public string ProviderCreditReversalId
    {
      get
      {
        return this.providerCreditReversalIdField;
      }
      set
      {
        this.providerCreditReversalIdField = value;
      }
    }

    public ProviderCreditReversalSummary WithProviderCreditReversalId(string providerCreditReversalId)
    {
      this.providerCreditReversalIdField = providerCreditReversalId;
      return this;
    }

    public bool IsSetProviderCreditReversalId()
    {
      return this.providerCreditReversalIdField != null;
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
      if (this.IsSetProviderCreditReversalId())
      {
        stringBuilder.Append("<ProviderCreditReversalId>");
        stringBuilder.Append(this.ProviderCreditReversalId);
        stringBuilder.Append("</ProviderCreditReversalId>");
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
