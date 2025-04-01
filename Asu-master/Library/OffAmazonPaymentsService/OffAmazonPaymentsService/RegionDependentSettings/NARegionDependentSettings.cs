// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings.NARegionDependentSettings
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

namespace OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings
{
  public abstract class NARegionDependentSettings : RegionDependentSettingsImpl
  {
    private static string MWS_URL = "https://mws.amazonservices.com/";
    private static string WIDGET_URL = "https://static-na.payments-amazon.com/";

    protected override string getMwsUrl()
    {
      return NARegionDependentSettings.MWS_URL;
    }

    protected override string getWidgetUrl()
    {
      return NARegionDependentSettings.WIDGET_URL;
    }
  }
}
