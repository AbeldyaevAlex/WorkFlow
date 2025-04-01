// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.AuthorizeOnBillingAgreementResult
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class AuthorizeOnBillingAgreementResult
  {
    private AuthorizationDetails authorizationDetailsField;
    private string amazonOrderReferenceIdField;

    [XmlElement(ElementName = "AuthorizationDetails")]
    public AuthorizationDetails AuthorizationDetails
    {
      get
      {
        return this.authorizationDetailsField;
      }
      set
      {
        this.authorizationDetailsField = value;
      }
    }

    public AuthorizeOnBillingAgreementResult WithAuthorizationDetails(AuthorizationDetails authorizationDetails)
    {
      this.authorizationDetailsField = authorizationDetails;
      return this;
    }

    public bool IsSetAuthorizationDetails()
    {
      return this.authorizationDetailsField != null;
    }

    [XmlElement(ElementName = "AmazonOrderReferenceId")]
    public string AmazonOrderReferenceId
    {
      get
      {
        return this.amazonOrderReferenceIdField;
      }
      set
      {
        this.amazonOrderReferenceIdField = value;
      }
    }

    public AuthorizeOnBillingAgreementResult WithAmazonOrderReferenceId(string amazonOrderReferenceId)
    {
      this.amazonOrderReferenceIdField = amazonOrderReferenceId;
      return this;
    }

    public bool IsSetAmazonOrderReferenceId()
    {
      return this.amazonOrderReferenceIdField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetAuthorizationDetails())
      {
        AuthorizationDetails authorizationDetails = this.AuthorizationDetails;
        stringBuilder.Append("<AuthorizationDetails>");
        stringBuilder.Append(authorizationDetails.ToXMLFragment());
        stringBuilder.Append("</AuthorizationDetails>");
      }
      if (this.IsSetAmazonOrderReferenceId())
      {
        stringBuilder.Append("<AmazonOrderReferenceId>");
        stringBuilder.Append(this.EscapeXML(this.AmazonOrderReferenceId));
        stringBuilder.Append("</AmazonOrderReferenceId>");
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
