// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.GetProviderCreditDetailsResult
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class GetProviderCreditDetailsResult
  {
    private ProviderCreditDetails providerCreditDetailsField;

    [XmlElement(ElementName = "ProviderCreditDetails")]
    public ProviderCreditDetails ProviderCreditDetails
    {
      get
      {
        return this.providerCreditDetailsField;
      }
      set
      {
        this.providerCreditDetailsField = value;
      }
    }

    public GetProviderCreditDetailsResult WithProviderCreditDetails(ProviderCreditDetails providerCreditDetails)
    {
      this.providerCreditDetailsField = providerCreditDetails;
      return this;
    }

    public bool IsSetProviderCreditDetails()
    {
      return this.providerCreditDetailsField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetProviderCreditDetails())
      {
        ProviderCreditDetails providerCreditDetails = this.ProviderCreditDetails;
        stringBuilder.Append("<ProviderCreditDetails>");
        stringBuilder.Append(providerCreditDetails.ToXMLFragment());
        stringBuilder.Append("</ProviderCreditDetails>");
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
