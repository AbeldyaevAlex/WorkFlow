// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ProviderCreditReversalSummaryList
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
  public class ProviderCreditReversalSummaryList
  {
    private List<ProviderCreditReversalSummary> memberField;

    [XmlElement(ElementName = "member")]
    public List<ProviderCreditReversalSummary> member
    {
      get
      {
        if (this.memberField == null)
          this.memberField = new List<ProviderCreditReversalSummary>();
        return this.memberField;
      }
      set
      {
        this.memberField = value;
      }
    }

    public ProviderCreditReversalSummaryList Withmember(params ProviderCreditReversalSummary[] list)
    {
      foreach (ProviderCreditReversalSummary creditReversalSummary in list)
        this.member.Add(creditReversalSummary);
      return this;
    }

    public bool IsSetmember()
    {
      return this.member.Count > 0;
    }

    protected internal string ToXMLFragment()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (ProviderCreditReversalSummary creditReversalSummary in this.member)
      {
        stringBuilder.Append("<member>");
        stringBuilder.Append(creditReversalSummary.ToXMLFragment());
        stringBuilder.Append("</member>");
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
