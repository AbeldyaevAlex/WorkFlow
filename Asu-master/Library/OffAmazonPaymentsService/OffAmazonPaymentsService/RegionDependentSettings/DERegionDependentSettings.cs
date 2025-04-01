// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings.DERegionDependentSettings
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

namespace OffAmazonPaymentsService.OffAmazonPaymentsService.RegionDependentSettings
{
  public class DERegionDependentSettings : EURegionDependentSettings
  {
    public override string getLocale()
    {
      return "de";
    }

    public override string getCurrencyCode()
    {
      return "EUR";
    }
  }
}
