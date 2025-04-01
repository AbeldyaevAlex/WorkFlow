// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ParentDetails
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class ParentDetails
  {
    private string idField;
    private Type? typeField;

    [XmlElement(ElementName = "Id")]
    public string Id
    {
      get
      {
        return this.idField;
      }
      set
      {
        this.idField = value;
      }
    }

    public ParentDetails WithId(string id)
    {
      this.idField = id;
      return this;
    }

    public bool IsSetId()
    {
      return this.idField != null;
    }

    [XmlElement(ElementName = "Type")]
    public Type Type
    {
      get
      {
        return this.typeField.GetValueOrDefault();
      }
      set
      {
        this.typeField = new Type?(value);
      }
    }

    public ParentDetails WithType(Type type)
    {
      this.typeField = new Type?(type);
      return this;
    }

    public bool IsSetType()
    {
      return this.typeField.HasValue;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetId())
      {
        stringBuilder.Append("<Id>");
        stringBuilder.Append(this.EscapeXML(this.Id));
        stringBuilder.Append("</Id>");
      }
      if (this.IsSetType())
      {
        stringBuilder.Append("<Type>");
        stringBuilder.Append((object) this.Type);
        stringBuilder.Append("</Type>");
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
