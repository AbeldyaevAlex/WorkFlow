// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.BillingAgreementStatus
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System;
using System.Text;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService.Model
{
  [XmlRoot(IsNullable = false, Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  [XmlType(Namespace = "http://mws.amazonservices.com/schema/OffAmazonPayments/2013-01-01")]
  public class BillingAgreementStatus
  {
    private string stateField;
    private DateTime? lastUpdatedTimestampField;
    private string reasonCodeField;
    private string reasonDescriptionField;

    [XmlElement(ElementName = "State")]
    public string State
    {
      get
      {
        return this.stateField;
      }
      set
      {
        this.stateField = value;
      }
    }

    public BillingAgreementStatus WithState(string state)
    {
      this.stateField = state;
      return this;
    }

    public bool IsSetState()
    {
      return this.stateField != null;
    }

    [XmlElement(ElementName = "LastUpdatedTimestamp")]
    public DateTime LastUpdatedTimestamp
    {
      get
      {
        return this.lastUpdatedTimestampField.GetValueOrDefault();
      }
      set
      {
        this.lastUpdatedTimestampField = new DateTime?(value);
      }
    }

    public BillingAgreementStatus WithLastUpdatedTimestamp(DateTime lastUpdatedTimestamp)
    {
      this.lastUpdatedTimestampField = new DateTime?(lastUpdatedTimestamp);
      return this;
    }

    public bool IsSetLastUpdatedTimestamp()
    {
      return this.lastUpdatedTimestampField.HasValue;
    }

    [XmlElement(ElementName = "ReasonCode")]
    public string ReasonCode
    {
      get
      {
        return this.reasonCodeField;
      }
      set
      {
        this.reasonCodeField = value;
      }
    }

    public BillingAgreementStatus WithReasonCode(string reasonCode)
    {
      this.reasonCodeField = reasonCode;
      return this;
    }

    public bool IsSetReasonCode()
    {
      return this.reasonCodeField != null;
    }

    [XmlElement(ElementName = "ReasonDescription")]
    public string ReasonDescription
    {
      get
      {
        return this.reasonDescriptionField;
      }
      set
      {
        this.reasonDescriptionField = value;
      }
    }

    public BillingAgreementStatus WithReasonDescription(string reasonDescription)
    {
      this.reasonDescriptionField = reasonDescription;
      return this;
    }

    public bool IsSetReasonDescription()
    {
      return this.reasonDescriptionField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetState())
      {
        stringBuilder.Append("<State>");
        stringBuilder.Append(this.EscapeXML(this.State));
        stringBuilder.Append("</State>");
      }
      if (this.IsSetLastUpdatedTimestamp())
      {
        stringBuilder.Append("<LastUpdatedTimestamp>");
        stringBuilder.Append((object) this.LastUpdatedTimestamp);
        stringBuilder.Append("</LastUpdatedTimestamp>");
      }
      if (this.IsSetReasonCode())
      {
        stringBuilder.Append("<ReasonCode>");
        stringBuilder.Append(this.EscapeXML(this.ReasonCode));
        stringBuilder.Append("</ReasonCode>");
      }
      if (this.IsSetReasonDescription())
      {
        stringBuilder.Append("<ReasonDescription>");
        stringBuilder.Append(this.EscapeXML(this.ReasonDescription));
        stringBuilder.Append("</ReasonDescription>");
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
