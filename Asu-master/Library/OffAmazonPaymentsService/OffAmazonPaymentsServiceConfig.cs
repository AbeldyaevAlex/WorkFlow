// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.OffAmazonPaymentsServiceConfig
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace OffAmazonPaymentsService
{
  public class OffAmazonPaymentsServiceConfig
  {
    private string serviceVersion = "2013-01-01";
    private string serviceURL = (string) null;
    private string userAgent = (string) null;
    private string signatureVersion = "2";
    private string signatureMethod = "HmacSHA256";
    private string proxyHost = (string) null;
    private int proxyPort = -1;
    private int maxErrorRetry = 3;
    private string mwsClientVersion = "2013-01-01";
    private string applicationLibraryVersion = "1.0.0.9";

    public string ServiceVersion
    {
      get
      {
        return this.serviceVersion;
      }
    }

    public string SignatureMethod
    {
      get
      {
        return this.signatureMethod;
      }
      set
      {
        this.signatureMethod = value;
      }
    }

    public OffAmazonPaymentsServiceConfig WithSignatureMethod(string signatureMethod)
    {
      this.signatureMethod = signatureMethod;
      return this;
    }

    public bool IsSetSignatureMethod()
    {
      return this.signatureMethod != null;
    }

    public string SignatureVersion
    {
      get
      {
        return this.signatureVersion;
      }
      set
      {
        this.signatureVersion = value;
      }
    }

    public OffAmazonPaymentsServiceConfig WithSignatureVersion(string signatureVersion)
    {
      this.signatureVersion = signatureVersion;
      return this;
    }

    public bool IsSetSignatureVersion()
    {
      return this.signatureVersion != null;
    }

    public string UserAgent
    {
      get
      {
        return this.userAgent;
      }
    }

    public OffAmazonPaymentsServiceConfig WithUserAgent(string applicationName, string applicationVersion)
    {
      this.ConfigureUserAgentHeader(applicationName, applicationVersion);
      return this;
    }

    public void SetUserAgent(string applicationName, string applicationVersion)
    {
      this.ConfigureUserAgentHeader(applicationName, applicationVersion);
    }

    public bool IsSetUserAgent()
    {
      return this.userAgent != null;
    }

    public string ServiceURL
    {
      get
      {
        return this.serviceURL;
      }
      set
      {
        this.serviceURL = value;
      }
    }

    public OffAmazonPaymentsServiceConfig WithServiceURL(string serviceURL)
    {
      this.serviceURL = serviceURL;
      return this;
    }

    public bool IsSetServiceURL()
    {
      return this.serviceURL != null;
    }

    public string ProxyHost
    {
      get
      {
        return this.proxyHost;
      }
      set
      {
        this.proxyHost = value;
      }
    }

    public OffAmazonPaymentsServiceConfig WithProxyHost(string proxyHost)
    {
      this.proxyHost = proxyHost;
      return this;
    }

    public bool IsSetProxyHost()
    {
      return this.proxyHost != null;
    }

    public int ProxyPort
    {
      get
      {
        return this.proxyPort;
      }
      set
      {
        this.proxyPort = value;
      }
    }

    public OffAmazonPaymentsServiceConfig WithProxyPort(int proxyPort)
    {
      this.proxyPort = proxyPort;
      return this;
    }

    public bool IsSetProxyPort()
    {
      return this.proxyPort != -1;
    }

    public int MaxErrorRetry
    {
      get
      {
        return this.maxErrorRetry;
      }
      set
      {
        this.maxErrorRetry = value;
      }
    }

    public OffAmazonPaymentsServiceConfig WithMaxErrorRetry(int maxErrorRetry)
    {
      this.maxErrorRetry = maxErrorRetry;
      return this;
    }

    public bool IsSetMaxErrorRetry()
    {
      return this.maxErrorRetry != -1;
    }

    private void ConfigureUserAgentHeader(string applicationName, string applicationVersion)
    {
      this.SetUserAgentHeader(applicationName, applicationVersion, "C#", "CLI", Environment.Version.ToString(), "Platform", ((int) Environment.OSVersion.Platform).ToString() + "/" + (object) Environment.OSVersion.Version, "MWSClientVersion", this.mwsClientVersion, "ApplicationLibraryVersion", this.applicationLibraryVersion);
    }

    private void SetUserAgentHeader(string applicationName, string applicationVersion, string programmingLanguage, params string[] additionalNameValuePairs)
    {
      if (applicationName == null)
        throw new ArgumentNullException(nameof (applicationName), "Value cannot be null.");
      if (applicationVersion == null)
        throw new ArgumentNullException(nameof (applicationVersion), "Value cannot be null.");
      if (programmingLanguage == null)
        throw new ArgumentNullException(nameof (programmingLanguage), "Value cannot be null.");
      if (additionalNameValuePairs.Length % 2 != 0)
        throw new ArgumentException(nameof (additionalNameValuePairs), "Every name must have a corresponding value.");
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(OffAmazonPaymentsServiceConfig.QuoteApplicationName(applicationName));
      stringBuilder.Append("/");
      stringBuilder.Append(OffAmazonPaymentsServiceConfig.QuoteApplicationVersion(applicationVersion));
      stringBuilder.Append(" (");
      stringBuilder.Append("Language=");
      stringBuilder.Append(OffAmazonPaymentsServiceConfig.QuoteAttributeValue(programmingLanguage));
      int num;
      for (int index = 0; index < additionalNameValuePairs.Length; index = num + 1)
      {
        string additionalNameValuePair1 = additionalNameValuePairs[index];
        string additionalNameValuePair2 = additionalNameValuePairs[num = index + 1];
        stringBuilder.Append("; ");
        stringBuilder.Append(OffAmazonPaymentsServiceConfig.QuoteAttributeName(additionalNameValuePair1));
        stringBuilder.Append("=");
        stringBuilder.Append(OffAmazonPaymentsServiceConfig.QuoteAttributeValue(additionalNameValuePair2));
      }
      stringBuilder.Append(")");
      this.userAgent = stringBuilder.ToString();
    }

    private static string Clean(string s)
    {
      return Regex.Replace(s, " {2,}|\\s", (MatchEvaluator) (m => " "));
    }

    private static string QuoteApplicationName(string s)
    {
      return OffAmazonPaymentsServiceConfig.Clean(s).Replace("\\", "\\\\").Replace("@/", "\\/");
    }

    private static string QuoteApplicationVersion(string s)
    {
      return OffAmazonPaymentsServiceConfig.Clean(s).Replace("\\", "\\\\").Replace("(", "\\(");
    }

    private static string QuoteAttributeName(string s)
    {
      return OffAmazonPaymentsServiceConfig.Clean(s).Replace("\\", "\\\\").Replace("=", "\\=");
    }

    private static string QuoteAttributeValue(string s)
    {
      return OffAmazonPaymentsServiceConfig.Clean(s).Replace("\\", "\\\\").Replace(";", "\\;").Replace(")", "\\)");
    }
  }
}
