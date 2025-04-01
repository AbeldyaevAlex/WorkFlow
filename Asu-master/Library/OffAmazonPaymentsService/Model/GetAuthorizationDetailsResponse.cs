// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.GetAuthorizationDetailsResponse
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class GetAuthorizationDetailsResponse
  {
    private GetAuthorizationDetailsResult getAuthorizationDetailsResultField;
    private ResponseMetadata responseMetadataField;

    [XmlElement(ElementName = "GetAuthorizationDetailsResult")]
    public GetAuthorizationDetailsResult GetAuthorizationDetailsResult
    {
      get
      {
        return this.getAuthorizationDetailsResultField;
      }
      set
      {
        this.getAuthorizationDetailsResultField = value;
      }
    }

    public GetAuthorizationDetailsResponse WithGetAuthorizationDetailsResult(GetAuthorizationDetailsResult getAuthorizationDetailsResult)
    {
      this.getAuthorizationDetailsResultField = getAuthorizationDetailsResult;
      return this;
    }

    public bool IsSetGetAuthorizationDetailsResult()
    {
      return this.getAuthorizationDetailsResultField != null;
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

    public GetAuthorizationDetailsResponse WithResponseMetadata(ResponseMetadata responseMetadata)
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
      stringBuilder.Append("<GetAuthorizationDetailsResponse xmlns=\"http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01\">");
      if (this.IsSetGetAuthorizationDetailsResult())
      {
        GetAuthorizationDetailsResult authorizationDetailsResult = this.GetAuthorizationDetailsResult;
        stringBuilder.Append("<GetAuthorizationDetailsResult>");
        stringBuilder.Append(authorizationDetailsResult.ToXMLFragment());
        stringBuilder.Append("</GetAuthorizationDetailsResult>");
      }
      if (this.IsSetResponseMetadata())
      {
        ResponseMetadata responseMetadata = this.ResponseMetadata;
        stringBuilder.Append("<ResponseMetadata>");
        stringBuilder.Append(responseMetadata.ToXMLFragment());
        stringBuilder.Append("</ResponseMetadata>");
      }
      stringBuilder.Append("</GetAuthorizationDetailsResponse>");
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
