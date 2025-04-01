// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.Destination
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class Destination
  {
    private string destinationTypeField;
    private Address physicalDestinationField;

    [XmlElement(ElementName = "DestinationType")]
    public string DestinationType
    {
      get
      {
        return this.destinationTypeField;
      }
      set
      {
        this.destinationTypeField = value;
      }
    }

    public Destination WithDestinationType(string destinationType)
    {
      this.destinationTypeField = destinationType;
      return this;
    }

    public bool IsSetDestinationType()
    {
      return this.destinationTypeField != null;
    }

    [XmlElement(ElementName = "PhysicalDestination")]
    public Address PhysicalDestination
    {
      get
      {
        return this.physicalDestinationField;
      }
      set
      {
        this.physicalDestinationField = value;
      }
    }

    public Destination WithPhysicalDestination(Address physicalDestination)
    {
      this.physicalDestinationField = physicalDestination;
      return this;
    }

    public bool IsSetPhysicalDestination()
    {
      return this.physicalDestinationField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetDestinationType())
      {
        stringBuilder.Append("<DestinationType>");
        stringBuilder.Append(this.EscapeXML(this.DestinationType));
        stringBuilder.Append("</DestinationType>");
      }
      if (this.IsSetPhysicalDestination())
      {
        Address physicalDestination = this.PhysicalDestination;
        stringBuilder.Append("<PhysicalDestination>");
        stringBuilder.Append(physicalDestination.ToXMLFragment());
        stringBuilder.Append("</PhysicalDestination>");
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
