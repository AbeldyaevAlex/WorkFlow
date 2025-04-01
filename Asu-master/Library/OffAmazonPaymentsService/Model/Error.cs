// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.Error
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class Error
  {
    private string typeField;
    private string codeField;
    private string messageField;
    private object detailField;

    [XmlElement(ElementName = "Type")]
    public string Type
    {
      get
      {
        return this.typeField;
      }
      set
      {
        this.typeField = value;
      }
    }

    public Error WithType(string type)
    {
      this.typeField = type;
      return this;
    }

    public bool IsSetType()
    {
      return this.typeField != null;
    }

    [XmlElement(ElementName = "Code")]
    public string Code
    {
      get
      {
        return this.codeField;
      }
      set
      {
        this.codeField = value;
      }
    }

    public Error WithCode(string code)
    {
      this.codeField = code;
      return this;
    }

    public bool IsSetCode()
    {
      return this.codeField != null;
    }

    [XmlElement(ElementName = "Message")]
    public string Message
    {
      get
      {
        return this.messageField;
      }
      set
      {
        this.messageField = value;
      }
    }

    public Error WithMessage(string message)
    {
      this.messageField = message;
      return this;
    }

    public bool IsSetMessage()
    {
      return this.messageField != null;
    }

    [XmlElement(ElementName = "Detail")]
    public object Detail
    {
      get
      {
        return this.detailField;
      }
      set
      {
        this.detailField = value;
      }
    }

    public Error WithDetail(object detail)
    {
      this.detailField = detail;
      return this;
    }

    public bool IsSetDetail()
    {
      return this.detailField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetType())
      {
        stringBuilder.Append("<Type>");
        stringBuilder.Append(this.Type);
        stringBuilder.Append("</Type>");
      }
      if (this.IsSetCode())
      {
        stringBuilder.Append("<Code>");
        stringBuilder.Append(this.EscapeXML(this.Code));
        stringBuilder.Append("</Code>");
      }
      if (this.IsSetMessage())
      {
        stringBuilder.Append("<Message>");
        stringBuilder.Append(this.EscapeXML(this.Message));
        stringBuilder.Append("</Message>");
      }
      if (this.IsSetDetail())
      {
        object detail = this.Detail;
        stringBuilder.Append("<Detail>");
        stringBuilder.Append(detail.ToString());
        stringBuilder.Append("</Detail>");
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
