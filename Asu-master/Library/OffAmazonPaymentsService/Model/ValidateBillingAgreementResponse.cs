// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ValidateBillingAgreementResponse
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class ValidateBillingAgreementResponse
  {
    private ValidateBillingAgreementResult validateBillingAgreementResultField;
    private ResponseMetadata responseMetadataField;

    [XmlElement(ElementName = "ValidateBillingAgreementResult")]
    public ValidateBillingAgreementResult ValidateBillingAgreementResult
    {
      get
      {
        return this.validateBillingAgreementResultField;
      }
      set
      {
        this.validateBillingAgreementResultField = value;
      }
    }

    public ValidateBillingAgreementResponse WithValidateBillingAgreementResult(ValidateBillingAgreementResult validateBillingAgreementResult)
    {
      this.validateBillingAgreementResultField = validateBillingAgreementResult;
      return this;
    }

    public bool IsSetValidateBillingAgreementResult()
    {
      return this.validateBillingAgreementResultField != null;
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

    public ValidateBillingAgreementResponse WithResponseMetadata(ResponseMetadata responseMetadata)
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
      stringBuilder.Append("<ValidateBillingAgreementResponse xmlns=\"http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01\">");
      if (this.IsSetValidateBillingAgreementResult())
      {
        ValidateBillingAgreementResult billingAgreementResult = this.ValidateBillingAgreementResult;
        stringBuilder.Append("<ValidateBillingAgreementResult>");
        stringBuilder.Append(billingAgreementResult.ToXMLFragment());
        stringBuilder.Append("</ValidateBillingAgreementResult>");
      }
      if (this.IsSetResponseMetadata())
      {
        ResponseMetadata responseMetadata = this.ResponseMetadata;
        stringBuilder.Append("<ResponseMetadata>");
        stringBuilder.Append(responseMetadata.ToXMLFragment());
        stringBuilder.Append("</ResponseMetadata>");
      }
      stringBuilder.Append("</ValidateBillingAgreementResponse>");
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
