// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.OrderReferenceAttributes
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class OrderReferenceAttributes
  {
    private OrderTotal orderTotalField;
    private string platformId;
    private string sellerNoteField;
    private SellerOrderAttributes sellerOrderAttributesField;

    [XmlElement(ElementName = "OrderTotal")]
    public OrderTotal OrderTotal
    {
      get
      {
        return this.orderTotalField;
      }
      set
      {
        this.orderTotalField = value;
      }
    }

    public OrderReferenceAttributes WithOrderTotal(OrderTotal orderTotal)
    {
      this.orderTotalField = orderTotal;
      return this;
    }

    public bool IsSetOrderTotal()
    {
      return this.orderTotalField != null;
    }

    [XmlElement(ElementName = "PlatformId")]
    public string PlatformId
    {
      get
      {
        return this.platformId;
      }
      set
      {
        this.platformId = value;
      }
    }

    public OrderReferenceAttributes WithPlatformId(string platformId)
    {
      this.platformId = platformId;
      return this;
    }

    public bool IsSetPlatformId()
    {
      return this.platformId != null;
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

    public OrderReferenceAttributes WithSellerNote(string sellerNote)
    {
      this.sellerNoteField = sellerNote;
      return this;
    }

    public bool IsSetSellerNote()
    {
      return this.sellerNoteField != null;
    }

    [XmlElement(ElementName = "SellerOrderAttributes")]
    public SellerOrderAttributes SellerOrderAttributes
    {
      get
      {
        return this.sellerOrderAttributesField;
      }
      set
      {
        this.sellerOrderAttributesField = value;
      }
    }

    public OrderReferenceAttributes WithSellerOrderAttributes(SellerOrderAttributes sellerOrderAttributes)
    {
      this.sellerOrderAttributesField = sellerOrderAttributes;
      return this;
    }

    public bool IsSetSellerOrderAttributes()
    {
      return this.sellerOrderAttributesField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetOrderTotal())
      {
        OrderTotal orderTotal = this.OrderTotal;
        stringBuilder.Append("<OrderTotal>");
        stringBuilder.Append(orderTotal.ToXMLFragment());
        stringBuilder.Append("</OrderTotal>");
      }
      if (this.IsSetPlatformId())
      {
        stringBuilder.Append("<PlatformId>");
        stringBuilder.Append(this.platformId);
        stringBuilder.Append("</PlatformId>");
      }
      if (this.IsSetSellerNote())
      {
        stringBuilder.Append("<SellerNote>");
        stringBuilder.Append(this.SellerNote);
        stringBuilder.Append("</SellerNote>");
      }
      if (this.IsSetSellerOrderAttributes())
      {
        SellerOrderAttributes sellerOrderAttributes = this.SellerOrderAttributes;
        stringBuilder.Append("<SellerOrderAttributes>");
        stringBuilder.Append(sellerOrderAttributes.ToXMLFragment());
        stringBuilder.Append("</SellerOrderAttributes>");
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
