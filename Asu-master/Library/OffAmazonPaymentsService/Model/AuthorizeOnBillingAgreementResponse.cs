// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.AuthorizeOnBillingAgreementResponse
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class AuthorizeOnBillingAgreementResponse
  {
    private AuthorizeOnBillingAgreementResult authorizeOnBillingAgreementResultField;
    private ResponseMetadata responseMetadataField;

    [XmlElement(ElementName = "AuthorizeOnBillingAgreementResult")]
    public AuthorizeOnBillingAgreementResult AuthorizeOnBillingAgreementResult
    {
      get
      {
        return this.authorizeOnBillingAgreementResultField;
      }
      set
      {
        this.authorizeOnBillingAgreementResultField = value;
      }
    }

    public AuthorizeOnBillingAgreementResponse WithAuthorizeOnBillingAgreementResult(AuthorizeOnBillingAgreementResult authorizeOnBillingAgreementResult)
    {
      this.authorizeOnBillingAgreementResultField = authorizeOnBillingAgreementResult;
      return this;
    }

    public bool IsSetAuthorizeOnBillingAgreementResult()
    {
      return this.authorizeOnBillingAgreementResultField != null;
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

    public AuthorizeOnBillingAgreementResponse WithResponseMetadata(ResponseMetadata responseMetadata)
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
      stringBuilder.Append("<AuthorizeOnBillingAgreementResponse xmlns=\"http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01\">");
      if (this.IsSetAuthorizeOnBillingAgreementResult())
      {
        AuthorizeOnBillingAgreementResult billingAgreementResult = this.AuthorizeOnBillingAgreementResult;
        stringBuilder.Append("<AuthorizeOnBillingAgreementResult>");
        stringBuilder.Append(billingAgreementResult.ToXMLFragment());
        stringBuilder.Append("</AuthorizeOnBillingAgreementResult>");
      }
      if (this.IsSetResponseMetadata())
      {
        ResponseMetadata responseMetadata = this.ResponseMetadata;
        stringBuilder.Append("<ResponseMetadata>");
        stringBuilder.Append(responseMetadata.ToXMLFragment());
        stringBuilder.Append("</ResponseMetadata>");
      }
      stringBuilder.Append("</AuthorizeOnBillingAgreementResponse>");
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
