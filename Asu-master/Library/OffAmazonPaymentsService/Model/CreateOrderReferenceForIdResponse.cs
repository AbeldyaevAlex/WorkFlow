// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.CreateOrderReferenceForIdResponse
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class CreateOrderReferenceForIdResponse
  {
    private CreateOrderReferenceForIdResult createOrderReferenceForIdResultField;
    private ResponseMetadata responseMetadataField;

    [XmlElement(ElementName = "CreateOrderReferenceForIdResult")]
    public CreateOrderReferenceForIdResult CreateOrderReferenceForIdResult
    {
      get
      {
        return this.createOrderReferenceForIdResultField;
      }
      set
      {
        this.createOrderReferenceForIdResultField = value;
      }
    }

    public CreateOrderReferenceForIdResponse WithCreateOrderReferenceForIdResult(CreateOrderReferenceForIdResult createOrderReferenceForIdResult)
    {
      this.createOrderReferenceForIdResultField = createOrderReferenceForIdResult;
      return this;
    }

    public bool IsSetCreateOrderReferenceForIdResult()
    {
      return this.createOrderReferenceForIdResultField != null;
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

    public CreateOrderReferenceForIdResponse WithResponseMetadata(ResponseMetadata responseMetadata)
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
      stringBuilder.Append("<CreateOrderReferenceForIdResponse xmlns=\"http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01\">");
      if (this.IsSetCreateOrderReferenceForIdResult())
      {
        CreateOrderReferenceForIdResult referenceForIdResult = this.CreateOrderReferenceForIdResult;
        stringBuilder.Append("<CreateOrderReferenceForIdResult>");
        stringBuilder.Append(referenceForIdResult.ToXMLFragment());
        stringBuilder.Append("</CreateOrderReferenceForIdResult>");
      }
      if (this.IsSetResponseMetadata())
      {
        ResponseMetadata responseMetadata = this.ResponseMetadata;
        stringBuilder.Append("<ResponseMetadata>");
        stringBuilder.Append(responseMetadata.ToXMLFragment());
        stringBuilder.Append("</ResponseMetadata>");
      }
      stringBuilder.Append("</CreateOrderReferenceForIdResponse>");
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
