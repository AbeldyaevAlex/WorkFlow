// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.SetBillingAgreementDetailsResult
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class SetBillingAgreementDetailsResult
  {
    private BillingAgreementDetails billingAgreementDetailsField;

    [XmlElement(ElementName = "BillingAgreementDetails")]
    public BillingAgreementDetails BillingAgreementDetails
    {
      get
      {
        return this.billingAgreementDetailsField;
      }
      set
      {
        this.billingAgreementDetailsField = value;
      }
    }

    public SetBillingAgreementDetailsResult WithBillingAgreementDetails(BillingAgreementDetails billingAgreementDetails)
    {
      this.billingAgreementDetailsField = billingAgreementDetails;
      return this;
    }

    public bool IsSetBillingAgreementDetails()
    {
      return this.billingAgreementDetailsField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetBillingAgreementDetails())
      {
        BillingAgreementDetails agreementDetails = this.BillingAgreementDetails;
        stringBuilder.Append("<BillingAgreementDetails>");
        stringBuilder.Append(agreementDetails.ToXMLFragment());
        stringBuilder.Append("</BillingAgreementDetails>");
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
