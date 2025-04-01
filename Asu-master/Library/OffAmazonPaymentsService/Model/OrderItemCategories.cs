// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.OrderItemCategories
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class OrderItemCategories
  {
    private List<string> orderItemCategoryField;

    [XmlElement(ElementName = "OrderItemCategory")]
    public List<string> OrderItemCategory
    {
      get
      {
        if (this.orderItemCategoryField == null)
          this.orderItemCategoryField = new List<string>();
        return this.orderItemCategoryField;
      }
      set
      {
        this.orderItemCategoryField = value;
      }
    }

    public OrderItemCategories WithOrderItemCategory(params string[] list)
    {
      foreach (string str in list)
        this.OrderItemCategory.Add(str);
      return this;
    }

    public bool IsSetOrderItemCategory()
    {
      return this.OrderItemCategory.Count > 0;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string str in this.OrderItemCategory)
      {
        stringBuilder.Append("<OrderItemCategory>");
        stringBuilder.Append(this.EscapeXML(str));
        stringBuilder.Append("</OrderItemCategory>");
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
