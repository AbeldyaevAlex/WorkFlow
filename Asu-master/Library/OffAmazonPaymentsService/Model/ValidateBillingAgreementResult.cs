// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ValidateBillingAgreementResult
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class ValidateBillingAgreementResult
  {
    private RequestStatus? validationResultField;
    private string failureReasonCodeField;
    private BillingAgreementStatus billingAgreementStatusField;

    [XmlElement(ElementName = "ValidationResult")]
    public RequestStatus ValidationResult
    {
      get
      {
        return this.validationResultField.GetValueOrDefault();
      }
      set
      {
        this.validationResultField = new RequestStatus?(value);
      }
    }

    public ValidateBillingAgreementResult WithValidationResult(RequestStatus validationResult)
    {
      this.validationResultField = new RequestStatus?(validationResult);
      return this;
    }

    public bool IsSetValidationResult()
    {
      return this.validationResultField.HasValue;
    }

    [XmlElement(ElementName = "FailureReasonCode")]
    public string FailureReasonCode
    {
      get
      {
        return this.failureReasonCodeField;
      }
      set
      {
        this.failureReasonCodeField = value;
      }
    }

    public ValidateBillingAgreementResult WithFailureReasonCode(string failureReasonCode)
    {
      this.failureReasonCodeField = failureReasonCode;
      return this;
    }

    public bool IsSetFailureReasonCode()
    {
      return this.failureReasonCodeField != null;
    }

    [XmlElement(ElementName = "BillingAgreementStatus")]
    public BillingAgreementStatus BillingAgreementStatus
    {
      get
      {
        return this.billingAgreementStatusField;
      }
      set
      {
        this.billingAgreementStatusField = value;
      }
    }

    public ValidateBillingAgreementResult WithBillingAgreementStatus(BillingAgreementStatus billingAgreementStatus)
    {
      this.billingAgreementStatusField = billingAgreementStatus;
      return this;
    }

    public bool IsSetBillingAgreementStatus()
    {
      return this.billingAgreementStatusField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetValidationResult())
      {
        stringBuilder.Append("<ValidationResult>");
        stringBuilder.Append((object) this.ValidationResult);
        stringBuilder.Append("</ValidationResult>");
      }
      if (this.IsSetFailureReasonCode())
      {
        stringBuilder.Append("<FailureReasonCode>");
        stringBuilder.Append(this.EscapeXML(this.FailureReasonCode));
        stringBuilder.Append("</FailureReasonCode>");
      }
      if (this.IsSetBillingAgreementStatus())
      {
        BillingAgreementStatus billingAgreementStatus = this.BillingAgreementStatus;
        stringBuilder.Append("<BillingAgreementStatus>");
        stringBuilder.Append(billingAgreementStatus.ToXMLFragment());
        stringBuilder.Append("</BillingAgreementStatus>");
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
