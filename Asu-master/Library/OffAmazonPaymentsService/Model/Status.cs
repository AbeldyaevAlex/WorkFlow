// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.Status
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
  public class Status
  {
    private PaymentStatus? stateField;
    private DateTime? lastUpdateTimestampField;
    private string reasonCodeField;
    private string reasonDescriptionField;

    [XmlElement(ElementName = "State")]
    public PaymentStatus State
    {
      get
      {
        return this.stateField.GetValueOrDefault();
      }
      set
      {
        this.stateField = new PaymentStatus?(value);
      }
    }

    public Status WithState(PaymentStatus state)
    {
      this.stateField = new PaymentStatus?(state);
      return this;
    }

    public bool IsSetState()
    {
      return this.stateField.HasValue;
    }

    [XmlElement(ElementName = "LastUpdateTimestamp")]
    public DateTime LastUpdateTimestamp
    {
      get
      {
        return this.lastUpdateTimestampField.GetValueOrDefault();
      }
      set
      {
        this.lastUpdateTimestampField = new DateTime?(value);
      }
    }

    public Status WithLastUpdateTimestamp(DateTime lastUpdateTimestamp)
    {
      this.lastUpdateTimestampField = new DateTime?(lastUpdateTimestamp);
      return this;
    }

    public bool IsSetLastUpdateTimestamp()
    {
      return this.lastUpdateTimestampField.HasValue;
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

    public Status WithReasonCode(string reasonCode)
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

    public Status WithReasonDescription(string reasonDescription)
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
        stringBuilder.Append((object) this.State);
        stringBuilder.Append("</State>");
      }
      if (this.IsSetLastUpdateTimestamp())
      {
        stringBuilder.Append("<LastUpdateTimestamp>");
        stringBuilder.Append((object) this.LastUpdateTimestamp);
        stringBuilder.Append("</LastUpdateTimestamp>");
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
        stringBuilder.Append(this.ReasonDescription);
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
