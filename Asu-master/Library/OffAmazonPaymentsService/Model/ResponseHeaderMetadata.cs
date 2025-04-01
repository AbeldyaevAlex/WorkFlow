// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.Model.ResponseHeaderMetadata
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

namespace OffAmazonPaymentsService.Model
{
  public class ResponseHeaderMetadata
  {
    private string requestId;
    private string responseContext;
    private string timestamp;

    public ResponseHeaderMetadata()
    {
      this.requestId = this.responseContext = this.timestamp = (string) null;
    }

    public ResponseHeaderMetadata(string requestId, string responseContext, string timestamp)
    {
      this.requestId = requestId;
      this.responseContext = responseContext;
      this.timestamp = timestamp;
    }

    public string RequestId
    {
      get
      {
        return this.requestId;
      }
    }

    public string ResponseContext
    {
      get
      {
        return this.responseContext;
      }
    }

    public string Timestamp
    {
      get
      {
        return this.timestamp;
      }
    }

    public override string ToString()
    {
      return "[RequestId: " + this.requestId + ", ResponseContext: " + this.responseContext + ", Timestamp: " + this.timestamp + "]";
    }
  }
}
