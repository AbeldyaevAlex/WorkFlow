// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.CaptureResult
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class CaptureResult
  {
    private CaptureDetails captureDetailsField;

    [XmlElement(ElementName = "CaptureDetails")]
    public CaptureDetails CaptureDetails
    {
      get
      {
        return this.captureDetailsField;
      }
      set
      {
        this.captureDetailsField = value;
      }
    }

    public CaptureResult WithCaptureDetails(CaptureDetails captureDetails)
    {
      this.captureDetailsField = captureDetails;
      return this;
    }

    public bool IsSetCaptureDetails()
    {
      return this.captureDetailsField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetCaptureDetails())
      {
        CaptureDetails captureDetails = this.CaptureDetails;
        stringBuilder.Append("<CaptureDetails>");
        stringBuilder.Append(captureDetails.ToXMLFragment());
        stringBuilder.Append("</CaptureDetails>");
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
