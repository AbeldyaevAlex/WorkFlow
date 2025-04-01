// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.OffAmazonPaymentsServiceException
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using OffAmazonPaymentsService.Model;
using System;
using System.Net;

namespace OffAmazonPaymentsService
{
  public class OffAmazonPaymentsServiceException : Exception
  {
    private string message = (string) null;
    private HttpStatusCode statusCode = (HttpStatusCode) 0;
    private string errorCode = (string) null;
    private string errorType = (string) null;
    private string requestId = (string) null;
    private string xml = (string) null;
    private ResponseHeaderMetadata responseHeaderMetadata = (ResponseHeaderMetadata) null;

    public OffAmazonPaymentsServiceException(string message)
    {
      this.message = message;
    }

    public OffAmazonPaymentsServiceException(string message, HttpStatusCode statusCode, ResponseHeaderMetadata rhm)
      : this(message)
    {
      this.statusCode = statusCode;
      this.responseHeaderMetadata = rhm;
    }

    public OffAmazonPaymentsServiceException(Exception t)
      : this(t.Message, t)
    {
    }

    public OffAmazonPaymentsServiceException(string message, Exception t)
      : base(message, t)
    {
      this.message = message;
      if (!(t is OffAmazonPaymentsServiceException))
        return;
      OffAmazonPaymentsServiceException serviceException = (OffAmazonPaymentsServiceException) t;
      this.statusCode = serviceException.StatusCode;
      this.errorCode = serviceException.ErrorCode;
      this.errorType = serviceException.ErrorType;
      this.requestId = serviceException.RequestId;
      this.xml = serviceException.XML;
      this.responseHeaderMetadata = serviceException.ResponseHeaderMetadata;
    }

    public OffAmazonPaymentsServiceException(string message, HttpStatusCode statusCode, string errorCode, string errorType, string requestId, string xml, ResponseHeaderMetadata rhm)
      : this(message, statusCode, rhm)
    {
      this.errorCode = errorCode;
      this.errorType = errorType;
      this.requestId = requestId;
      this.xml = xml;
    }

    public string ErrorCode
    {
      get
      {
        return this.errorCode;
      }
    }

    public string ErrorType
    {
      get
      {
        return this.errorType;
      }
    }

    public override string Message
    {
      get
      {
        return this.message;
      }
    }

    public HttpStatusCode StatusCode
    {
      get
      {
        return this.statusCode;
      }
    }

    public string XML
    {
      get
      {
        return this.xml;
      }
    }

    public string RequestId
    {
      get
      {
        return this.requestId;
      }
    }

    public ResponseHeaderMetadata ResponseHeaderMetadata
    {
      get
      {
        return this.responseHeaderMetadata;
      }
    }
  }
}
