// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.OffAmazonPaymentsServicePropertyCollection
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace OffAmazonPaymentsService
{
  public class OffAmazonPaymentsServicePropertyCollection
  {
    private static OffAmazonPaymentsServicePropertyCollection instance = (OffAmazonPaymentsServicePropertyCollection)null;
    private string merchantID;
    private string accessKey;
    private string secretKey;
    private string applicationName;
    private string applicationVersion;
    private string environment;
    private string region;
    private string widgetUrl;
    private string clientId;
    private string certCN;
    private string serviceUrl;
    private IDictionary<string, RegionDependentSettings> regionList;
    private OffAmazonPaymentsServiceConfig URLConfig;

    public static OffAmazonPaymentsServicePropertyCollection getInstance()
    {
      if (OffAmazonPaymentsServicePropertyCollection.instance == null)
        OffAmazonPaymentsServicePropertyCollection.instance = new OffAmazonPaymentsServicePropertyCollection();
      return OffAmazonPaymentsServicePropertyCollection.instance;
    }

    public string MerchantID
    {
        get
        {
            return this.merchantID;
        }
    }

    public string AccessKey
    {
        get
        {
            return this.accessKey;
        }
    }

    public string SecretKey
    {
        get
        {
            return this.secretKey;
        }
    }

    public string CurrencyCode
    {
        get
        {
            if (!this.regionList.ContainsKey(this.Region.ToLower()))
            {
                return (string)null;
            }
          
            return this.regionList[this.Region.ToLower()].getCurrencyCode();
        }
    }

    public string ApplicationName
    {
        get
        {
            return this.applicationName;
        }
    }

    public string ApplicationVersion
    {
        get
        {
            return this.applicationVersion;
        }
    }

    public string Region
    {
        get
        {
            return this.region;
        }
    }

    public string Environment
    {
        get
        {
            return this.environment;
        }
    }

    public string ServiceURL
    {
        get
        {
            string str;
            if (!string.IsNullOrEmpty(this.serviceUrl))
            {
                str = UrlBuilder.buildMwsUrlWithBaseForEnvironment(this.serviceUrl, this.environment);
            }
            else
            {
                if (!this.regionList.ContainsKey(this.Region.ToLower()))
                {
                    return (string)null;
                }
                
                str = this.regionList[this.Region.ToLower()].getMwsUrlForEnvironment(this.environment);
            }

            return str;
        }
    }

    public string WidgetUrl
    {
        get
        {
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.widgetUrl))
            {
                if (!this.regionList.ContainsKey(this.Region.ToLower()))
                {
                    return (string)null;
                }
                
                var locale = this.regionList[this.Region.ToLower()].getLocale();
                stringBuilder.Append(UrlBuilder.buildWidgetUrlWithBaseAndLocaleForEnvironment(this.widgetUrl, locale, this.environment));
            }
            else
            {
                if (!this.regionList.ContainsKey(this.Region.ToLower()))
                {
                    return (string)null;
                }
                
                stringBuilder.Append(this.regionList[this.region.ToLower()].getWidgetUrlForEnvironment(this.environment));
            }

            return stringBuilder.ToString();
        }
    }

    public OffAmazonPaymentsServiceConfig MPSConfig
    {
      get
      {
        return this.URLConfig.WithServiceURL(this.ServiceURL);
      }
    }

    public OffAmazonPaymentsServicePropertyCollection(string applicationName = null,
        string applicationVersion = null,
        string region = null,
        string merchantId = null, 
        string accessKey = null, 
        string secretKey = null, 
        string environment = null, 
        string clientId = null, 
        string widgetUrl = null,
        string certCn = null,
        string serviceUrl = null)
    {
        this.serviceUrl = serviceUrl;
        this.region = region;
        this.applicationName = applicationName;
        this.applicationVersion = applicationVersion;
        this.merchantID = merchantId;
        this.accessKey = accessKey;
        this.secretKey = secretKey;
        this.environment = environment;
        this.clientId = clientId;
        this.certCN = certCn;
        this.widgetUrl = widgetUrl;
        this.regionList = (IDictionary<string, RegionDependentSettings>)new Dictionary<string, RegionDependentSettings>();
        this.ConstructRegionList();
        this.URLConfig = new OffAmazonPaymentsServiceConfig();
        this.URLConfig.WithServiceURL(this.ServiceURL);
        if (!this.environment.Equals("sandbox") && !this.environment.Equals("live"))
        {
            throw new SystemException("The value of environment is not correct!");
        }
        
        this.rejectConfigurationIfEURegionIsSelected();
    }

    private void rejectConfigurationIfEURegionIsSelected()
    {
        if (this.region.Equals("eu"))
        {
            throw new OffAmazonPaymentsServiceException("The eu region is deprecated, please enter either de or uk to select the correct region.");
        }
    }

    private void ConstructRegionList()
    {
      this.regionList.Add("de", (RegionDependentSettings) new DERegionDependentSettings());
      this.regionList.Add("uk", (RegionDependentSettings) new UKRegionDependentSettings());
      this.regionList.Add("us", (RegionDependentSettings) new USRegionDependentSettings());
      this.regionList.Add("na", (RegionDependentSettings) new USRegionDependentSettings());
    }

    public string ClientId
    {
      get
      {
        if (this.clientId == null)
          throw new SystemException("client id not defined, check app/web configuration and add a key for clientId");
        return this.clientId;
      }
    }

    public string CertCN
    {
      get
      {
        if (this.certCN == null)
          throw new SystemException("certCN is not defined, check app/web configuration and add a key for certCN");
        return this.certCN;
      }
    }
  }
}
