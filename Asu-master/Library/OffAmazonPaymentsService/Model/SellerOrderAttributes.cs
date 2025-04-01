// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.SellerOrderAttributes
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class SellerOrderAttributes
  {
    private string sellerOrderIdField;
    private string storeNameField;
    private OrderItemCategories orderItemCategoriesField;
    private string customInformationField;

    [XmlElement(ElementName = "SellerOrderId")]
    public string SellerOrderId
    {
      get
      {
        return this.sellerOrderIdField;
      }
      set
      {
        this.sellerOrderIdField = value;
      }
    }

    public SellerOrderAttributes WithSellerOrderId(string sellerOrderId)
    {
      this.sellerOrderIdField = sellerOrderId;
      return this;
    }

    public bool IsSetSellerOrderId()
    {
      return this.sellerOrderIdField != null;
    }

    [XmlElement(ElementName = "StoreName")]
    public string StoreName
    {
      get
      {
        return this.storeNameField;
      }
      set
      {
        this.storeNameField = value;
      }
    }

    public SellerOrderAttributes WithStoreName(string storeName)
    {
      this.storeNameField = storeName;
      return this;
    }

    public bool IsSetStoreName()
    {
      return this.storeNameField != null;
    }

    [XmlElement(ElementName = "OrderItemCategories")]
    public OrderItemCategories OrderItemCategories
    {
      get
      {
        return this.orderItemCategoriesField;
      }
      set
      {
        this.orderItemCategoriesField = value;
      }
    }

    public SellerOrderAttributes WithOrderItemCategories(OrderItemCategories orderItemCategories)
    {
      this.orderItemCategoriesField = orderItemCategories;
      return this;
    }

    public bool IsSetOrderItemCategories()
    {
      return this.orderItemCategoriesField != null;
    }

    [XmlElement(ElementName = "CustomInformation")]
    public string CustomInformation
    {
      get
      {
        return this.customInformationField;
      }
      set
      {
        this.customInformationField = value;
      }
    }

    public SellerOrderAttributes WithCustomInformation(string customInformation)
    {
      this.customInformationField = customInformation;
      return this;
    }

    public bool IsSetCustomInformation()
    {
      return this.customInformationField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetSellerOrderId())
      {
        stringBuilder.Append("<SellerOrderId>");
        stringBuilder.Append(this.EscapeXML(this.SellerOrderId));
        stringBuilder.Append("</SellerOrderId>");
      }
      if (this.IsSetStoreName())
      {
        stringBuilder.Append("<StoreName>");
        stringBuilder.Append(this.EscapeXML(this.StoreName));
        stringBuilder.Append("</StoreName>");
      }
      if (this.IsSetOrderItemCategories())
      {
        OrderItemCategories orderItemCategories = this.OrderItemCategories;
        stringBuilder.Append("<OrderItemCategories>");
        stringBuilder.Append(orderItemCategories.ToXMLFragment());
        stringBuilder.Append("</OrderItemCategories>");
      }
      if (this.IsSetCustomInformation())
      {
        stringBuilder.Append("<CustomInformation>");
        stringBuilder.Append(this.EscapeXML(this.CustomInformation));
        stringBuilder.Append("</CustomInformation>");
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
