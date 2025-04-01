// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.BillingAgreementAttributes
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class BillingAgreementAttributes
  {
    private string platformIdField;
    private string sellerNoteField;
    private SellerBillingAgreementAttributes sellerBillingAgreementAttributesField;

    [XmlElement(ElementName = "PlatformId")]
    public string PlatformId
    {
      get
      {
        return this.platformIdField;
      }
      set
      {
        this.platformIdField = value;
      }
    }

    public BillingAgreementAttributes WithPlatformId(string platformId)
    {
      this.platformIdField = platformId;
      return this;
    }

    public bool IsSetPlatformId()
    {
      return this.platformIdField != null;
    }

    [XmlElement(ElementName = "SellerNote")]
    public string SellerNote
    {
      get
      {
        return this.sellerNoteField;
      }
      set
      {
        this.sellerNoteField = value;
      }
    }

    public BillingAgreementAttributes WithSellerNote(string sellerNote)
    {
      this.sellerNoteField = sellerNote;
      return this;
    }

    public bool IsSetSellerNote()
    {
      return this.sellerNoteField != null;
    }

    [XmlElement(ElementName = "SellerBillingAgreementAttributes")]
    public SellerBillingAgreementAttributes SellerBillingAgreementAttributes
    {
      get
      {
        return this.sellerBillingAgreementAttributesField;
      }
      set
      {
        this.sellerBillingAgreementAttributesField = value;
      }
    }

    public BillingAgreementAttributes WithSellerBillingAgreementAttributes(SellerBillingAgreementAttributes sellerBillingAgreementAttributes)
    {
      this.sellerBillingAgreementAttributesField = sellerBillingAgreementAttributes;
      return this;
    }

    public bool IsSetSellerBillingAgreementAttributes()
    {
      return this.sellerBillingAgreementAttributesField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetPlatformId())
      {
        stringBuilder.Append("<PlatformId>");
        stringBuilder.Append(this.EscapeXML(this.PlatformId));
        stringBuilder.Append("</PlatformId>");
      }
      if (this.IsSetSellerNote())
      {
        stringBuilder.Append("<SellerNote>");
        stringBuilder.Append(this.EscapeXML(this.SellerNote));
        stringBuilder.Append("</SellerNote>");
      }
      if (this.IsSetSellerBillingAgreementAttributes())
      {
        SellerBillingAgreementAttributes agreementAttributes = this.SellerBillingAgreementAttributes;
        stringBuilder.Append("<SellerBillingAgreementAttributes>");
        stringBuilder.Append(agreementAttributes.ToXMLFragment());
        stringBuilder.Append("</SellerBillingAgreementAttributes>");
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
