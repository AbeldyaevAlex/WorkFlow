// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.Buyer
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class Buyer
  {
    private string nameField;
    private string emailField;
    private string phoneField;

    [XmlElement(ElementName = "Name")]
    public string Name
    {
      get
      {
        return this.nameField;
      }
      set
      {
        this.nameField = value;
      }
    }

    public Buyer WithName(string name)
    {
      this.nameField = name;
      return this;
    }

    public bool IsSetName()
    {
      return this.nameField != null;
    }

    [XmlElement(ElementName = "Email")]
    public string Email
    {
      get
      {
        return this.emailField;
      }
      set
      {
        this.emailField = value;
      }
    }

    public Buyer WithEmail(string email)
    {
      this.emailField = email;
      return this;
    }

    public bool IsSetEmail()
    {
      return this.emailField != null;
    }

    [XmlElement(ElementName = "Phone")]
    public string Phone
    {
      get
      {
        return this.phoneField;
      }
      set
      {
        this.phoneField = value;
      }
    }

    public Buyer WithPhone(string phone)
    {
      this.phoneField = phone;
      return this;
    }

    public bool IsSetPhone()
    {
      return this.phoneField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetName())
      {
        stringBuilder.Append("<Name>");
        stringBuilder.Append(this.EscapeXML(this.Name));
        stringBuilder.Append("</Name>");
      }
      if (this.IsSetEmail())
      {
        stringBuilder.Append("<Email>");
        stringBuilder.Append(this.EscapeXML(this.Email));
        stringBuilder.Append("</Email>");
      }
      if (this.IsSetPhone())
      {
        stringBuilder.Append("<Phone>");
        stringBuilder.Append(this.EscapeXML(this.Phone));
        stringBuilder.Append("</Phone>");
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
