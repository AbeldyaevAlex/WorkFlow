// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.Address
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class Address
  {
    private string nameField;
    private string addressLine1Field;
    private string addressLine2Field;
    private string addressLine3Field;
    private string cityField;
    private string countyField;
    private string districtField;
    private string stateOrRegionField;
    private string postalCodeField;
    private string countryCodeField;
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

    public Address WithName(string name)
    {
      this.nameField = name;
      return this;
    }

    public bool IsSetName()
    {
      return this.nameField != null;
    }

    [XmlElement(ElementName = "AddressLine1")]
    public string AddressLine1
    {
      get
      {
        return this.addressLine1Field;
      }
      set
      {
        this.addressLine1Field = value;
      }
    }

    public Address WithAddressLine1(string addressLine1)
    {
      this.addressLine1Field = addressLine1;
      return this;
    }

    public bool IsSetAddressLine1()
    {
      return this.addressLine1Field != null;
    }

    [XmlElement(ElementName = "AddressLine2")]
    public string AddressLine2
    {
      get
      {
        return this.addressLine2Field;
      }
      set
      {
        this.addressLine2Field = value;
      }
    }

    public Address WithAddressLine2(string addressLine2)
    {
      this.addressLine2Field = addressLine2;
      return this;
    }

    public bool IsSetAddressLine2()
    {
      return this.addressLine2Field != null;
    }

    [XmlElement(ElementName = "AddressLine3")]
    public string AddressLine3
    {
      get
      {
        return this.addressLine3Field;
      }
      set
      {
        this.addressLine3Field = value;
      }
    }

    public Address WithAddressLine3(string addressLine3)
    {
      this.addressLine3Field = addressLine3;
      return this;
    }

    public bool IsSetAddressLine3()
    {
      return this.addressLine3Field != null;
    }

    [XmlElement(ElementName = "City")]
    public string City
    {
      get
      {
        return this.cityField;
      }
      set
      {
        this.cityField = value;
      }
    }

    public Address WithCity(string city)
    {
      this.cityField = city;
      return this;
    }

    public bool IsSetCity()
    {
      return this.cityField != null;
    }

    [XmlElement(ElementName = "County")]
    public string County
    {
      get
      {
        return this.countyField;
      }
      set
      {
        this.countyField = value;
      }
    }

    public Address WithCounty(string county)
    {
      this.countyField = county;
      return this;
    }

    public bool IsSetCounty()
    {
      return this.countyField != null;
    }

    [XmlElement(ElementName = "District")]
    public string District
    {
      get
      {
        return this.districtField;
      }
      set
      {
        this.districtField = value;
      }
    }

    public Address WithDistrict(string district)
    {
      this.districtField = district;
      return this;
    }

    public bool IsSetDistrict()
    {
      return this.districtField != null;
    }

    [XmlElement(ElementName = "StateOrRegion")]
    public string StateOrRegion
    {
      get
      {
        return this.stateOrRegionField;
      }
      set
      {
        this.stateOrRegionField = value;
      }
    }

    public Address WithStateOrRegion(string stateOrRegion)
    {
      this.stateOrRegionField = stateOrRegion;
      return this;
    }

    public bool IsSetStateOrRegion()
    {
      return this.stateOrRegionField != null;
    }

    [XmlElement(ElementName = "PostalCode")]
    public string PostalCode
    {
      get
      {
        return this.postalCodeField;
      }
      set
      {
        this.postalCodeField = value;
      }
    }

    public Address WithPostalCode(string postalCode)
    {
      this.postalCodeField = postalCode;
      return this;
    }

    public bool IsSetPostalCode()
    {
      return this.postalCodeField != null;
    }

    [XmlElement(ElementName = "CountryCode")]
    public string CountryCode
    {
      get
      {
        return this.countryCodeField;
      }
      set
      {
        this.countryCodeField = value;
      }
    }

    public Address WithCountryCode(string countryCode)
    {
      this.countryCodeField = countryCode;
      return this;
    }

    public bool IsSetCountryCode()
    {
      return this.countryCodeField != null;
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

    public Address WithPhone(string phone)
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
      if (this.IsSetAddressLine1())
      {
        stringBuilder.Append("<AddressLine1>");
        stringBuilder.Append(this.EscapeXML(this.AddressLine1));
        stringBuilder.Append("</AddressLine1>");
      }
      if (this.IsSetAddressLine2())
      {
        stringBuilder.Append("<AddressLine2>");
        stringBuilder.Append(this.EscapeXML(this.AddressLine2));
        stringBuilder.Append("</AddressLine2>");
      }
      if (this.IsSetAddressLine3())
      {
        stringBuilder.Append("<AddressLine3>");
        stringBuilder.Append(this.EscapeXML(this.AddressLine3));
        stringBuilder.Append("</AddressLine3>");
      }
      if (this.IsSetCity())
      {
        stringBuilder.Append("<City>");
        stringBuilder.Append(this.EscapeXML(this.City));
        stringBuilder.Append("</City>");
      }
      if (this.IsSetCounty())
      {
        stringBuilder.Append("<County>");
        stringBuilder.Append(this.EscapeXML(this.County));
        stringBuilder.Append("</County>");
      }
      if (this.IsSetDistrict())
      {
        stringBuilder.Append("<District>");
        stringBuilder.Append(this.EscapeXML(this.District));
        stringBuilder.Append("</District>");
      }
      if (this.IsSetStateOrRegion())
      {
        stringBuilder.Append("<StateOrRegion>");
        stringBuilder.Append(this.EscapeXML(this.StateOrRegion));
        stringBuilder.Append("</StateOrRegion>");
      }
      if (this.IsSetPostalCode())
      {
        stringBuilder.Append("<PostalCode>");
        stringBuilder.Append(this.EscapeXML(this.PostalCode));
        stringBuilder.Append("</PostalCode>");
      }
      if (this.IsSetCountryCode())
      {
        stringBuilder.Append("<CountryCode>");
        stringBuilder.Append(this.EscapeXML(this.CountryCode));
        stringBuilder.Append("</CountryCode>");
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
