// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.BillingAgreementLimits
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
  public class BillingAgreementLimits
  {
    private Price amountLimitPerTimePeriodField;
    private DateTime? timePeriodStartDateField;
    private DateTime? timePeriodEndDateField;
    private Price currentRemainingBalanceField;

    [XmlElement(ElementName = "AmountLimitPerTimePeriod")]
    public Price AmountLimitPerTimePeriod
    {
      get
      {
        return this.amountLimitPerTimePeriodField;
      }
      set
      {
        this.amountLimitPerTimePeriodField = value;
      }
    }

    public BillingAgreementLimits WithAmountLimitPerTimePeriod(Price amountLimitPerTimePeriod)
    {
      this.amountLimitPerTimePeriodField = amountLimitPerTimePeriod;
      return this;
    }

    public bool IsSetAmountLimitPerTimePeriod()
    {
      return this.amountLimitPerTimePeriodField != null;
    }

    [XmlElement(ElementName = "TimePeriodStartDate")]
    public DateTime TimePeriodStartDate
    {
      get
      {
        return this.timePeriodStartDateField.GetValueOrDefault();
      }
      set
      {
        this.timePeriodStartDateField = new DateTime?(value);
      }
    }

    public BillingAgreementLimits WithTimePeriodStartDate(DateTime timePeriodStartDate)
    {
      this.timePeriodStartDateField = new DateTime?(timePeriodStartDate);
      return this;
    }

    public bool IsSetTimePeriodStartDate()
    {
      return this.timePeriodStartDateField.HasValue;
    }

    [XmlElement(ElementName = "TimePeriodEndDate")]
    public DateTime TimePeriodEndDate
    {
      get
      {
        return this.timePeriodEndDateField.GetValueOrDefault();
      }
      set
      {
        this.timePeriodEndDateField = new DateTime?(value);
      }
    }

    public BillingAgreementLimits WithTimePeriodEndDate(DateTime timePeriodEndDate)
    {
      this.timePeriodEndDateField = new DateTime?(timePeriodEndDate);
      return this;
    }

    public bool IsSetTimePeriodEndDate()
    {
      return this.timePeriodEndDateField.HasValue;
    }

    [XmlElement(ElementName = "CurrentRemainingBalance")]
    public Price CurrentRemainingBalance
    {
      get
      {
        return this.currentRemainingBalanceField;
      }
      set
      {
        this.currentRemainingBalanceField = value;
      }
    }

    public BillingAgreementLimits WithCurrentRemainingBalance(Price currentRemainingBalance)
    {
      this.currentRemainingBalanceField = currentRemainingBalance;
      return this;
    }

    public bool IsSetCurrentRemainingBalance()
    {
      return this.currentRemainingBalanceField != null;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsSetAmountLimitPerTimePeriod())
      {
        Price limitPerTimePeriod = this.AmountLimitPerTimePeriod;
        stringBuilder.Append("<AmountLimitPerTimePeriod>");
        stringBuilder.Append(limitPerTimePeriod.ToXMLFragment());
        stringBuilder.Append("</AmountLimitPerTimePeriod>");
      }
      if (this.IsSetTimePeriodStartDate())
      {
        stringBuilder.Append("<TimePeriodStartDate>");
        stringBuilder.Append((object) this.TimePeriodStartDate);
        stringBuilder.Append("</TimePeriodStartDate>");
      }
      if (this.IsSetTimePeriodEndDate())
      {
        stringBuilder.Append("<TimePeriodEndDate>");
        stringBuilder.Append((object) this.TimePeriodEndDate);
        stringBuilder.Append("</TimePeriodEndDate>");
      }
      if (this.IsSetCurrentRemainingBalance())
      {
        Price remainingBalance = this.CurrentRemainingBalance;
        stringBuilder.Append("<CurrentRemainingBalance>");
        stringBuilder.Append(remainingBalance.ToXMLFragment());
        stringBuilder.Append("</CurrentRemainingBalance>");
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
