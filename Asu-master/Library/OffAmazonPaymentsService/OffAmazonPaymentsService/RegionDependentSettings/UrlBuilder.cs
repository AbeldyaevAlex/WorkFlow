// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings.UrlBuilder
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using System.Text;

namespace OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings
{
  public static class UrlBuilder
  {
    public static string buildMwsUrlWithBaseForEnvironment(string urlBase, string environment)
    {
      StringBuilder builderWithUrlBase = UrlBuilder.createStringBuilderWithUrlBase(urlBase);
      builderWithUrlBase.Append("OffAmazonPayments");
      if (UrlBuilder.isSandbox(environment))
        builderWithUrlBase.Append("_Sandbox");
      builderWithUrlBase.Append("/2013-01-01");
      return builderWithUrlBase.ToString();
    }

    public static string buildWidgetUrlWithBaseAndLocaleForEnvironment(string urlBase, string locale, string environment)
    {
      StringBuilder builderWithUrlBase = UrlBuilder.createStringBuilderWithUrlBase(urlBase);
      builderWithUrlBase.Append("OffAmazonPayments/");
      builderWithUrlBase.Append(locale);
      if (UrlBuilder.isSandbox(environment))
        builderWithUrlBase.Append("/sandbox");
      if (locale.Equals("us") || locale.Equals("na"))
        builderWithUrlBase.Append("/js/Widgets.js");
      else
        builderWithUrlBase.Append("/lpa/js/Widgets.js");
      return builderWithUrlBase.ToString();
    }

    private static StringBuilder createStringBuilderWithUrlBase(string urlBase)
    {
      StringBuilder stringBuilder = new StringBuilder(urlBase);
      if (!urlBase.EndsWith("/"))
        stringBuilder.Append("/");
      return stringBuilder;
    }

    private static bool isSandbox(string environment)
    {
      return environment.Equals("sandbox");
    }
  }
}
