// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.CreateOrderReferenceForIdRequest
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class CreateOrderReferenceForIdRequest
  {
    private string idField;
    private string sellerIdField;
    private string idTypeField;
    private bool? inheritShippingAddressField;
    private bool? confirmNowField;
    private OrderReferenceAttributes orderReferenceAttributesField;
    private string mwsAuthTokenField;

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

    public CreateOrderReferenceForIdRequest WithId(string id)
    {
      this.idField = id;
      return this;
    }

    public bool IsSetId()
    {
      return this.idField != null;
    }

    [XmlElement(ElementName = "SellerId")]
    public string SellerId
    {
      get
      {
        return this.sellerIdField;
      }
      set
      {
        this.sellerIdField = value;
      }
    }

    public CreateOrderReferenceForIdRequest WithSellerId(string sellerId)
    {
      this.sellerIdField = sellerId;
      return this;
    }

    public bool IsSetSellerId()
    {
      return this.sellerIdField != null;
    }

    [XmlElement(ElementName = "IdType")]
    public string IdType
    {
      get
      {
        return this.idTypeField;
      }
      set
      {
        this.idTypeField = value;
      }
    }

    public CreateOrderReferenceForIdRequest WithIdType(string idType)
    {
      this.idTypeField = idType;
      return this;
    }

    public bool IsSetIdType()
    {
      return this.idTypeField != null;
    }

    [XmlElement(ElementName = "InheritShippingAddress")]
    public bool InheritShippingAddress
    {
      get
      {
        return this.inheritShippingAddressField.GetValueOrDefault();
      }
      set
      {
        this.inheritShippingAddressField = new bool?(value);
      }
    }

    public CreateOrderReferenceForIdRequest WithInheritShippingAddress(bool inheritShippingAddress)
    {
      this.inheritShippingAddressField = new bool?(inheritShippingAddress);
      return this;
    }

    public bool IsSetInheritShippingAddress()
    {
      return this.inheritShippingAddressField.HasValue;
    }

    [XmlElement(ElementName = "ConfirmNow")]
    public bool ConfirmNow
    {
      get
      {
        return this.confirmNowField.GetValueOrDefault();
      }
      set
      {
        this.confirmNowField = new bool?(value);
      }
    }

    public CreateOrderReferenceForIdRequest WithConfirmNow(bool confirmNow)
    {
      this.confirmNowField = new bool?(confirmNow);
      return this;
    }

    public bool IsSetConfirmNow()
    {
      return this.confirmNowField.HasValue;
    }

    [XmlElement(ElementName = "OrderReferenceAttributes")]
    public OrderReferenceAttributes OrderReferenceAttributes
    {
      get
      {
        return this.orderReferenceAttributesField;
      }
      set
      {
        this.orderReferenceAttributesField = value;
      }
    }

    public CreateOrderReferenceForIdRequest WithOrderReferenceAttributes(OrderReferenceAttributes orderReferenceAttributes)
    {
      this.orderReferenceAttributesField = orderReferenceAttributes;
      return this;
    }

    public bool IsSetOrderReferenceAttributes()
    {
      return this.orderReferenceAttributesField != null;
    }

    [XmlElement(ElementName = "MWSAuthToken")]
    public string MWSAuthToken
    {
      get
      {
        return this.mwsAuthTokenField;
      }
      set
      {
        this.mwsAuthTokenField = value;
      }
    }

    public CreateOrderReferenceForIdRequest WithMWSAuthToken(string mwsAuthToken)
    {
      this.mwsAuthTokenField = mwsAuthToken;
      return this;
    }

    public bool IsSetMWSAuthToken()
    {
      return this.mwsAuthTokenField != null;
    }
  }
}
