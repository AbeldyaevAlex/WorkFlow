// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.Type
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public enum Type
  {
    [XmlEnum(Name = "OrderReference")] OrderReference,
    [XmlEnum(Name = "BillingAgreement")] BillingAgreement,
    [XmlEnum(Name = "ChildOrderReference")] ChildOrderReference,
  }
}
