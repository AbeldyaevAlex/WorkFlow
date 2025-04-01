// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ErrorResponse
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class ErrorResponse
  {
    private List<Error> errorField;
    private string requestIdField;

    [XmlElement(ElementName = "Error")]
    public List<Error> Error
    {
      get
      {
        if (this.errorField == null)
          this.errorField = new List<Error>();
        return this.errorField;
      }
      set
      {
        this.errorField = value;
      }
    }

    public ErrorResponse WithError(params Error[] list)
    {
      foreach (Error error in list)
        this.Error.Add(error);
      return this;
    }

    public bool IsSetError()
    {
      return this.Error.Count > 0;
    }

    [XmlElement(ElementName = "RequestId")]
    public string RequestId
    {
      get
      {
        return this.requestIdField;
      }
      set
      {
        this.requestIdField = value;
      }
    }

    public ErrorResponse WithRequestId(string requestId)
    {
      this.requestIdField = requestId;
      return this;
    }

    public bool IsSetRequestId()
    {
      return this.requestIdField != null;
    }

    public string ToXML()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<ErrorResponse xmlns=\"http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01\">");
      foreach (Error error in this.Error)
      {
        stringBuilder.Append("<Error>");
        stringBuilder.Append(error.ToXMLFragment());
        stringBuilder.Append("</Error>");
      }
      if (this.IsSetRequestId())
      {
        stringBuilder.Append("<RequestId>");
        stringBuilder.Append(this.EscapeXML(this.RequestId));
        stringBuilder.Append("</RequestId>");
      }
      stringBuilder.Append("</ErrorResponse>");
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
