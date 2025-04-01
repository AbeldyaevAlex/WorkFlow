// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings.RegionDependentSettingsImpl
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

namespace OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings
{
  public abstract class RegionDependentSettingsImpl : RegionDependentSettings
  {
    public string getMwsUrlForEnvironment(string environment)
    {
      return UrlBuilder.buildMwsUrlWithBaseForEnvironment(this.getMwsUrl(), environment);
    }

    public string getWidgetUrlForEnvironment(string environment)
    {
      return UrlBuilder.buildWidgetUrlWithBaseAndLocaleForEnvironment(this.getWidgetUrl(), this.getLocale(), environment);
    }

    public abstract string getCurrencyCode();

    public abstract string getLocale();

    protected abstract string getMwsUrl();

    protected abstract string getWidgetUrl();
  }
}
