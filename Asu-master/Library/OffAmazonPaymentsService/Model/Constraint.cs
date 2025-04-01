// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.Constraint
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class Constraint
  {
    private string constraintIDField;
    private string descriptionField;

    [XmlElement(ElementName = "ConstraintID")]
    public string ConstraintID
    {
      get
      {
        return this.constraintIDField;
      }
      set
      {
        this.constraintIDField = value;
      }
    }

    public Constraint WithConstraintID(string constraintID)
    {
      this.constraintIDField = constraintID;
      return this;
    }

    public bool IsSetConstraintID()
    {
      return this.constraintIDField != null;
    }

    [XmlElement(ElementName = "Description")]
    public string Description
    {
      get
      {
        return this.descriptionField;
      }
      set
      {
        this.descriptionField = value;
      }
    }

    public Constraint WithDescription(string description)
    {
      this.descriptionField = description;
      return this;
    }

    public bool IsSetDescription()
    {
      return this.descriptionField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetConstraintID())
      {
        stringBuilder.Append("<ConstraintID>");
        stringBuilder.Append(this.EscapeXML(this.ConstraintID));
        stringBuilder.Append("</ConstraintID>");
      }
      if (this.IsSetDescription())
      {
        stringBuilder.Append("<Description>");
        stringBuilder.Append(this.EscapeXML(this.Description));
        stringBuilder.Append("</Description>");
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
