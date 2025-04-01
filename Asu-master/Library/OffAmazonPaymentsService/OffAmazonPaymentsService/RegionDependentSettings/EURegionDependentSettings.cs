// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings.EURegionDependentSettings
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

namespace OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings
{
  public abstract class EURegionDependentSettings : RegionDependentSettingsImpl
  {
    private static string MWS_URL = "https://mws-eu.amazonservices.com/";
    private static string WIDGET_URL = "https://static-eu.payments-amazon.com/";

    protected override string getMwsUrl()
    {
      return EURegionDependentSettings.MWS_URL;
    }

    protected override string getWidgetUrl()
    {
      return EURegionDependentSettings.WIDGET_URL;
    }
  }
}
