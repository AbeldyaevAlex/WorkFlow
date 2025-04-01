// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.GetProviderCreditReversalDetailsResponse
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class GetProviderCreditReversalDetailsResponse
  {
    private GetProviderCreditReversalDetailsResult getProviderCreditReversalDetailsResultField;
    private ResponseMetadata responseMetadataField;

    [XmlElement(ElementName = "GetProviderCreditReversalDetailsResult")]
    public GetProviderCreditReversalDetailsResult GetProviderCreditReversalDetailsResult
    {
      get
      {
        return this.getProviderCreditReversalDetailsResultField;
      }
      set
      {
        this.getProviderCreditReversalDetailsResultField = value;
      }
    }

    public GetProviderCreditReversalDetailsResponse WithGetProviderCreditReversalDetailsResult(GetProviderCreditReversalDetailsResult getProviderCreditReversalDetailsResult)
    {
      this.getProviderCreditReversalDetailsResultField = getProviderCreditReversalDetailsResult;
      return this;
    }

    public bool IsSetGetProviderCreditReversalDetailsResult()
    {
      return this.getProviderCreditReversalDetailsResultField != null;
    }

    [XmlElement(ElementName = "ResponseMetadata")]
    public ResponseMetadata ResponseMetadata
    {
      get
      {
        return this.responseMetadataField;
      }
      set
      {
        this.responseMetadataField = value;
      }
    }

    public GetProviderCreditReversalDetailsResponse WithResponseMetadata(ResponseMetadata responseMetadata)
    {
      this.responseMetadataField = responseMetadata;
      return this;
    }

    public bool IsSetResponseMetadata()
    {
      return this.responseMetadataField != null;
    }

    public string ToXML()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<GetProviderCreditReversalDetailsResponse xmlns=\"http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01\">");
      if (this.IsSetGetProviderCreditReversalDetailsResult())
      {
        GetProviderCreditReversalDetailsResult reversalDetailsResult = this.GetProviderCreditReversalDetailsResult;
        stringBuilder.Append("<GetProviderCreditReversalDetailsResult>");
        stringBuilder.Append(reversalDetailsResult.ToXMLFragment());
        stringBuilder.Append("</GetProviderCreditReversalDetailsResult>");
      }
      if (this.IsSetResponseMetadata())
      {
        ResponseMetadata responseMetadata = this.ResponseMetadata;
        stringBuilder.Append("<ResponseMetadata>");
        stringBuilder.Append(responseMetadata.ToXMLFragment());
        stringBuilder.Append("</ResponseMetadata>");
      }
      stringBuilder.Append("</GetProviderCreditReversalDetailsResponse>");
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
