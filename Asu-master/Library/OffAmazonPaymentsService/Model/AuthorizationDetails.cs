// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.AuthorizationDetails
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System;
using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class AuthorizationDetails
  {
    private string amazonAuthorizationIdField;
    private string authorizationReferenceIdField;
    private Address authorizationBillingAddress;
    private string sellerAuthorizationNoteField;
    private Price authorizationAmountField;
    private Price capturedAmountField;
    private Price authorizationFeeField;
    private IdList idListField;
    private DateTime? creationTimestampField;
    private DateTime? expirationTimestampField;
    private Status authorizationStatusField;
    private OrderItemCategories orderItemCategoriesField;
    private bool? captureNowField;
    private string softDescriptorField;
    private string addressVerificationCodeField;

    [XmlElement(ElementName = "AmazonAuthorizationId")]
    public string AmazonAuthorizationId
    {
      get
      {
        return this.amazonAuthorizationIdField;
      }
      set
      {
        this.amazonAuthorizationIdField = value;
      }
    }

    public AuthorizationDetails WithAmazonAuthorizationId(string amazonAuthorizationId)
    {
      this.amazonAuthorizationIdField = amazonAuthorizationId;
      return this;
    }

    public bool IsSetAmazonAuthorizationId()
    {
      return this.amazonAuthorizationIdField != null;
    }

    [XmlElement(ElementName = "AuthorizationReferenceId")]
    public string AuthorizationReferenceId
    {
      get
      {
        return this.authorizationReferenceIdField;
      }
      set
      {
        this.authorizationReferenceIdField = value;
      }
    }

    public AuthorizationDetails WithAuthorizationReferenceId(string authorizationReferenceId)
    {
      this.authorizationReferenceIdField = authorizationReferenceId;
      return this;
    }

    public bool IsSetAuthorizationReferenceId()
    {
      return this.authorizationReferenceIdField != null;
    }

    [XmlElement(ElementName = "AuthorizationBillingAddress")]
    public Address AuthorizationBillingAddress
    {
      get
      {
        return this.authorizationBillingAddress;
      }
      set
      {
        this.authorizationBillingAddress = value;
      }
    }

    public AuthorizationDetails WithAuthorizationBillingAddress(Address authorizationBillingAddress)
    {
      this.authorizationBillingAddress = authorizationBillingAddress;
      return this;
    }

    public bool IsSetAuthorizationBillingAddress()
    {
      return this.authorizationBillingAddress != null;
    }

    [XmlElement(ElementName = "SellerAuthorizationNote")]
    public string SellerAuthorizationNote
    {
      get
      {
        return this.sellerAuthorizationNoteField;
      }
      set
      {
        this.sellerAuthorizationNoteField = value;
      }
    }

    public AuthorizationDetails WithSellerAuthorizationNote(string sellerAuthorizationNote)
    {
      this.sellerAuthorizationNoteField = sellerAuthorizationNote;
      return this;
    }

    public bool IsSetSellerAuthorizationNote()
    {
      return this.sellerAuthorizationNoteField != null;
    }

    [XmlElement(ElementName = "AuthorizationAmount")]
    public Price AuthorizationAmount
    {
      get
      {
        return this.authorizationAmountField;
      }
      set
      {
        this.authorizationAmountField = value;
      }
    }

    public AuthorizationDetails WithAuthorizationAmount(Price authorizationAmount)
    {
      this.authorizationAmountField = authorizationAmount;
      return this;
    }

    public bool IsSetAuthorizationAmount()
    {
      return this.authorizationAmountField != null;
    }

    [XmlElement(ElementName = "CapturedAmount")]
    public Price CapturedAmount
    {
      get
      {
        return this.capturedAmountField;
      }
      set
      {
        this.capturedAmountField = value;
      }
    }

    public AuthorizationDetails WithCapturedAmount(Price capturedAmount)
    {
      this.capturedAmountField = capturedAmount;
      return this;
    }

    public bool IsSetCapturedAmount()
    {
      return this.capturedAmountField != null;
    }

    [XmlElement(ElementName = "AuthorizationFee")]
    public Price AuthorizationFee
    {
      get
      {
        return this.authorizationFeeField;
      }
      set
      {
        this.authorizationFeeField = value;
      }
    }

    public AuthorizationDetails WithAuthorizationFee(Price authorizationFee)
    {
      this.authorizationFeeField = authorizationFee;
      return this;
    }

    public bool IsSetAuthorizationFee()
    {
      return this.authorizationFeeField != null;
    }

    [XmlElement(ElementName = "IdList")]
    public IdList IdList
    {
      get
      {
        return this.idListField;
      }
      set
      {
        this.idListField = value;
      }
    }

    public AuthorizationDetails WithIdList(IdList idList)
    {
      this.idListField = idList;
      return this;
    }

    public bool IsSetIdList()
    {
      return this.idListField != null;
    }

    [XmlElement(ElementName = "CreationTimestamp")]
    public DateTime CreationTimestamp
    {
      get
      {
        return this.creationTimestampField.GetValueOrDefault();
      }
      set
      {
        this.creationTimestampField = new DateTime?(value);
      }
    }

    public AuthorizationDetails WithCreationTimestamp(DateTime creationTimestamp)
    {
      this.creationTimestampField = new DateTime?(creationTimestamp);
      return this;
    }

    public bool IsSetCreationTimestamp()
    {
      return this.creationTimestampField.HasValue;
    }

    [XmlElement(ElementName = "ExpirationTimestamp")]
    public DateTime ExpirationTimestamp
    {
      get
      {
        return this.expirationTimestampField.GetValueOrDefault();
      }
      set
      {
        this.expirationTimestampField = new DateTime?(value);
      }
    }

    public AuthorizationDetails WithExpirationTimestamp(DateTime expirationTimestamp)
    {
      this.expirationTimestampField = new DateTime?(expirationTimestamp);
      return this;
    }

    public bool IsSetExpirationTimestamp()
    {
      return this.expirationTimestampField.HasValue;
    }

    [XmlElement(ElementName = "AuthorizationStatus")]
    public Status AuthorizationStatus
    {
      get
      {
        return this.authorizationStatusField;
      }
      set
      {
        this.authorizationStatusField = value;
      }
    }

    public AuthorizationDetails WithAuthorizationStatus(Status authorizationStatus)
    {
      this.authorizationStatusField = authorizationStatus;
      return this;
    }

    public bool IsSetAuthorizationStatus()
    {
      return this.authorizationStatusField != null;
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

    public AuthorizationDetails WithOrderItemCategories(OrderItemCategories orderItemCategories)
    {
      this.orderItemCategoriesField = orderItemCategories;
      return this;
    }

    public bool IsSetOrderItemCategories()
    {
      return this.orderItemCategoriesField != null;
    }

    [XmlElement(ElementName = "CaptureNow")]
    public bool CaptureNow
    {
      get
      {
        return this.captureNowField.GetValueOrDefault();
      }
      set
      {
        this.captureNowField = new bool?(value);
      }
    }

    public AuthorizationDetails WithCaptureNow(bool captureNow)
    {
      this.captureNowField = new bool?(captureNow);
      return this;
    }

    public bool IsSetCaptureNow()
    {
      return this.captureNowField.HasValue;
    }

    [XmlElement(ElementName = "SoftDescriptor")]
    public string SoftDescriptor
    {
      get
      {
        return this.softDescriptorField;
      }
      set
      {
        this.softDescriptorField = value;
      }
    }

    public AuthorizationDetails WithSoftDescriptor(string softDescriptor)
    {
      this.softDescriptorField = softDescriptor;
      return this;
    }

    public bool IsSetSoftDescriptor()
    {
      return this.softDescriptorField != null;
    }

    [XmlElement(ElementName = "AddressVerificationCode")]
    public string AddressVerificationCode
    {
      get
      {
        return this.addressVerificationCodeField;
      }
      set
      {
        this.addressVerificationCodeField = value;
      }
    }

    public AuthorizationDetails WithAddressVerificationCode(string addressVerificationCode)
    {
      this.addressVerificationCodeField = addressVerificationCode;
      return this;
    }

    public bool IsSetAddressVerificationCode()
    {
      return this.addressVerificationCodeField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetAmazonAuthorizationId())
      {
        stringBuilder.Append("<AmazonAuthorizationId>");
        stringBuilder.Append(this.AmazonAuthorizationId);
        stringBuilder.Append("</AmazonAuthorizationId>");
      }
      if (this.IsSetAuthorizationReferenceId())
      {
        stringBuilder.Append("<AuthorizationReferenceId>");
        stringBuilder.Append(this.AuthorizationReferenceId);
        stringBuilder.Append("</AuthorizationReferenceId>");
      }
      if (this.IsSetAuthorizationBillingAddress())
      {
        stringBuilder.Append("<AuthorizationBillingAddress>");
        stringBuilder.Append((object) this.AuthorizationBillingAddress);
        stringBuilder.Append("</AuthorizationBillingAddress>");
      }
      if (this.IsSetSellerAuthorizationNote())
      {
        stringBuilder.Append("<SellerAuthorizationNote>");
        stringBuilder.Append(this.EscapeXML(this.SellerAuthorizationNote));
        stringBuilder.Append("</SellerAuthorizationNote>");
      }
      if (this.IsSetAuthorizationAmount())
      {
        Price authorizationAmount = this.AuthorizationAmount;
        stringBuilder.Append("<AuthorizationAmount>");
        stringBuilder.Append(authorizationAmount.ToXMLFragment());
        stringBuilder.Append("</AuthorizationAmount>");
      }
      if (this.IsSetCapturedAmount())
      {
        Price capturedAmount = this.CapturedAmount;
        stringBuilder.Append("<CapturedAmount>");
        stringBuilder.Append(capturedAmount.ToXMLFragment());
        stringBuilder.Append("</CapturedAmount>");
      }
      if (this.IsSetAuthorizationFee())
      {
        Price authorizationFee = this.AuthorizationFee;
        stringBuilder.Append("<AuthorizationFee>");
        stringBuilder.Append(authorizationFee.ToXMLFragment());
        stringBuilder.Append("</AuthorizationFee>");
      }
      if (this.IsSetIdList())
      {
        IdList idList = this.IdList;
        stringBuilder.Append("<IdList>");
        stringBuilder.Append(idList.ToXMLFragment());
        stringBuilder.Append("</IdList>");
      }
      if (this.IsSetCreationTimestamp())
      {
        stringBuilder.Append("<CreationTimestamp>");
        stringBuilder.Append((object) this.CreationTimestamp);
        stringBuilder.Append("</CreationTimestamp>");
      }
      if (this.IsSetExpirationTimestamp())
      {
        stringBuilder.Append("<ExpirationTimestamp>");
        stringBuilder.Append((object) this.ExpirationTimestamp);
        stringBuilder.Append("</ExpirationTimestamp>");
      }
      if (this.IsSetAuthorizationStatus())
      {
        Status authorizationStatus = this.AuthorizationStatus;
        stringBuilder.Append("<AuthorizationStatus>");
        stringBuilder.Append(authorizationStatus.ToXMLFragment());
        stringBuilder.Append("</AuthorizationStatus>");
      }
      if (this.IsSetOrderItemCategories())
      {
        OrderItemCategories orderItemCategories = this.OrderItemCategories;
        stringBuilder.Append("<OrderItemCategories>");
        stringBuilder.Append(orderItemCategories.ToXMLFragment());
        stringBuilder.Append("</OrderItemCategories>");
      }
      if (this.IsSetCaptureNow())
      {
        stringBuilder.Append("<CaptureNow>");
        stringBuilder.Append(this.CaptureNow);
        stringBuilder.Append("</CaptureNow>");
      }
      if (this.IsSetSoftDescriptor())
      {
        stringBuilder.Append("<SoftDescriptor>");
        stringBuilder.Append(this.EscapeXML(this.SoftDescriptor));
        stringBuilder.Append("</SoftDescriptor>");
      }
      if (this.IsSetAddressVerificationCode())
      {
        stringBuilder.Append("<AddressVerificationCode>");
        stringBuilder.Append(this.EscapeXML(this.AddressVerificationCode));
        stringBuilder.Append("</AddressVerificationCode>");
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
