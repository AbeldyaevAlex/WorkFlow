// Decompiled with JetBrains decompiler
// Type: OffAmazonPaymentsService.OffAmazonPaymentsServiceClient
// Assembly: OffAmazonPaymentsService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 64D9CE78-CFCD-41C5-A9A4-3D730BFC8C93
// Assembly location: C:\Users\Administrator\Desktop\AmazonPaymentsAdvancedSDK-dotnet-1.0.14_US\bin\OffAmazonPaymentsService.dll

using OffAmazonPaymentsService.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace OffAmazonPaymentsService
{
  public class OffAmazonPaymentsServiceClient : IOffAmazonPaymentsService
  {
    private string awsAccessKeyId = (string) null;
    private string awsSecretAccessKey = (string) null;
    private OffAmazonPaymentsServiceConfig config = (OffAmazonPaymentsServiceConfig) null;
    private const string REQUEST_THROTTLED_ERROR_CODE = "RequestThrottled";

    public OffAmazonPaymentsServiceClient(string applicationName, string applicationVersion, string awsAccessKeyId, string awsSecretAccessKey, OffAmazonPaymentsServiceConfig config)
    {
      this.awsAccessKeyId = awsAccessKeyId;
      this.awsSecretAccessKey = awsSecretAccessKey;
      this.config = config;
      ServicePointManager.Expect100Continue = false;
      ServicePointManager.UseNagleAlgorithm = false;
      config.SetUserAgent(applicationName, applicationVersion);
    }

    public OffAmazonPaymentsServiceClient(OffAmazonPaymentsServicePropertyCollection property)
      : this(property.ApplicationName, property.ApplicationVersion, property.AccessKey, property.SecretKey, property.MPSConfig)
    {
    }

    public CaptureResponse Capture(CaptureRequest request)
    {
      return this.Invoke<CaptureResponse>(this.ConvertCapture(request));
    }

    public RefundResponse Refund(RefundRequest request)
    {
      return this.Invoke<RefundResponse>(this.ConvertRefund(request));
    }

    public CloseAuthorizationResponse CloseAuthorization(CloseAuthorizationRequest request)
    {
      return this.Invoke<CloseAuthorizationResponse>(this.ConvertCloseAuthorization(request));
    }

    public GetRefundDetailsResponse GetRefundDetails(GetRefundDetailsRequest request)
    {
      return this.Invoke<GetRefundDetailsResponse>(this.ConvertGetRefundDetails(request));
    }

    public GetCaptureDetailsResponse GetCaptureDetails(GetCaptureDetailsRequest request)
    {
      return this.Invoke<GetCaptureDetailsResponse>(this.ConvertGetCaptureDetails(request));
    }

    public CloseOrderReferenceResponse CloseOrderReference(CloseOrderReferenceRequest request)
    {
      return this.Invoke<CloseOrderReferenceResponse>(this.ConvertCloseOrderReference(request));
    }

    public ConfirmOrderReferenceResponse ConfirmOrderReference(ConfirmOrderReferenceRequest request)
    {
      return this.Invoke<ConfirmOrderReferenceResponse>(this.ConvertConfirmOrderReference(request));
    }

    public GetOrderReferenceDetailsResponse GetOrderReferenceDetails(GetOrderReferenceDetailsRequest request)
    {
      return this.Invoke<GetOrderReferenceDetailsResponse>(this.ConvertGetOrderReferenceDetails(request));
    }

    public AuthorizeResponse Authorize(AuthorizeRequest request)
    {
      return this.Invoke<AuthorizeResponse>(this.ConvertAuthorize(request));
    }

    public SetOrderReferenceDetailsResponse SetOrderReferenceDetails(SetOrderReferenceDetailsRequest request)
    {
      return this.Invoke<SetOrderReferenceDetailsResponse>(this.ConvertSetOrderReferenceDetails(request));
    }

    public GetAuthorizationDetailsResponse GetAuthorizationDetails(GetAuthorizationDetailsRequest request)
    {
      return this.Invoke<GetAuthorizationDetailsResponse>(this.ConvertGetAuthorizationDetails(request));
    }

    public CancelOrderReferenceResponse CancelOrderReference(CancelOrderReferenceRequest request)
    {
      return this.Invoke<CancelOrderReferenceResponse>(this.ConvertCancelOrderReference(request));
    }

    public CreateOrderReferenceForIdResponse CreateOrderReferenceForId(CreateOrderReferenceForIdRequest request)
    {
      return this.Invoke<CreateOrderReferenceForIdResponse>(this.ConvertCreateOrderReferenceForId(request));
    }

    public GetBillingAgreementDetailsResponse GetBillingAgreementDetails(GetBillingAgreementDetailsRequest request)
    {
      return this.Invoke<GetBillingAgreementDetailsResponse>(this.ConvertGetBillingAgreementDetails(request));
    }

    public SetBillingAgreementDetailsResponse SetBillingAgreementDetails(SetBillingAgreementDetailsRequest request)
    {
      return this.Invoke<SetBillingAgreementDetailsResponse>(this.ConvertSetBillingAgreementDetails(request));
    }

    public ConfirmBillingAgreementResponse ConfirmBillingAgreement(ConfirmBillingAgreementRequest request)
    {
      return this.Invoke<ConfirmBillingAgreementResponse>(this.ConvertConfirmBillingAgreement(request));
    }

    public ValidateBillingAgreementResponse ValidateBillingAgreement(ValidateBillingAgreementRequest request)
    {
      return this.Invoke<ValidateBillingAgreementResponse>(this.ConvertValidateBillingAgreement(request));
    }

    public AuthorizeOnBillingAgreementResponse AuthorizeOnBillingAgreement(AuthorizeOnBillingAgreementRequest request)
    {
      return this.Invoke<AuthorizeOnBillingAgreementResponse>(this.ConvertAuthorizeOnBillingAgreement(request));
    }

    public CloseBillingAgreementResponse CloseBillingAgreement(CloseBillingAgreementRequest request)
    {
      return this.Invoke<CloseBillingAgreementResponse>(this.ConvertCloseBillingAgreement(request));
    }

    public GetProviderCreditDetailsResponse GetProviderCreditDetails(GetProviderCreditDetailsRequest request)
    {
      return this.Invoke<GetProviderCreditDetailsResponse>(this.ConvertGetProviderCreditDetails(request));
    }

    public GetProviderCreditReversalDetailsResponse GetProviderCreditReversalDetails(GetProviderCreditReversalDetailsRequest request)
    {
      return this.Invoke<GetProviderCreditReversalDetailsResponse>(this.ConvertGetProviderCreditReversalDetails(request));
    }

    public ReverseProviderCreditResponse ReverseProviderCredit(ReverseProviderCreditRequest request)
    {
      return this.Invoke<ReverseProviderCreditResponse>(this.ConvertReverseProviderCredit(request));
    }

    private HttpWebRequest ConfigureWebRequest(int contentLength)
    {
      HttpWebRequest httpWebRequest = WebRequest.Create(this.config.ServiceURL) as HttpWebRequest;
      if (this.config.IsSetProxyHost())
        httpWebRequest.Proxy = (IWebProxy) new WebProxy(this.config.ProxyHost, this.config.ProxyPort);
      httpWebRequest.UserAgent = this.config.UserAgent;
      httpWebRequest.Method = "POST";
      httpWebRequest.Timeout = 50000;
      httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
      httpWebRequest.ContentLength = (long) contentLength;
      return httpWebRequest;
    }

    private T Invoke<T>(IDictionary<string, string> parameters)
    {
      string parameter = parameters["Action"];
      T obj = default (T);
      string str = (string) null;
      HttpStatusCode httpStatusCode = (HttpStatusCode) 0;
      ResponseHeaderMetadata rhm = (ResponseHeaderMetadata) null;
      if (string.IsNullOrEmpty(this.config.ServiceURL))
        throw new OffAmazonPaymentsServiceException((Exception) new ArgumentException("Missing serviceURL configuration value. You may obtain a list of valid MWS URLs by consulting the MWS Developer's Guide, or reviewing the sample code published along side this library."));
      this.AddRequiredParameters(parameters);
      byte[] bytes = new UTF8Encoding().GetBytes(this.GetParametersAsString(parameters));
      int num = 0;
      bool flag;
      do
      {
        HttpWebRequest httpWebRequest = this.ConfigureWebRequest(bytes.Length);
        try
        {
          using (Stream requestStream = httpWebRequest.GetRequestStream())
            requestStream.Write(bytes, 0, bytes.Length);
          using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
          {
            httpStatusCode = response.StatusCode;
            rhm = new ResponseHeaderMetadata(response.GetResponseHeader("x-mws-request-id"), response.GetResponseHeader("x-mws-response-context"), response.GetResponseHeader("x-mws-timestamp"));
            str = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
          }
          using (StringReader stringReader = new StringReader(str))
            obj = (T) new XmlSerializer(typeof (T)).Deserialize((TextReader) stringReader);
          flag = false;
        }
        catch (WebException ex1)
        {
          flag = false;
          using (HttpWebResponse response = (HttpWebResponse) ex1.Response)
          {
            if (response == null)
              throw new OffAmazonPaymentsServiceException((Exception) ex1);
            httpStatusCode = response.StatusCode;
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
              str = streamReader.ReadToEnd();
          }
          using (StringReader stringReader = new StringReader(str))
          {
            try
            {
              ErrorResponse errorResponse = (ErrorResponse) new XmlSerializer(typeof (ErrorResponse)).Deserialize((TextReader) stringReader);
              Error error = errorResponse.Error[0];
              if ((httpStatusCode == HttpStatusCode.InternalServerError || httpStatusCode == HttpStatusCode.ServiceUnavailable) && !"RequestThrottled".Equals(error.Code) && num < this.config.MaxErrorRetry)
              {
                flag = true;
                this.PauseOnRetry(++num, httpStatusCode, rhm);
              }
              else
              {
                flag = false;
                throw new OffAmazonPaymentsServiceException(error.Message, httpStatusCode, error.Code, error.Type, errorResponse.RequestId, errorResponse.ToXML(), rhm);
              }
            }
            catch (OffAmazonPaymentsServiceException ex2)
            {
              throw ex2;
            }
            catch (Exception ex2)
            {
              throw this.ReportAnyErrors(str, httpStatusCode, rhm, ex2);
            }
          }
        }
        catch (Exception ex)
        {
          throw new OffAmazonPaymentsServiceException(ex);
        }
      }
      while (flag);
      return obj;
    }

    private OffAmazonPaymentsServiceException ReportAnyErrors(string responseBody, HttpStatusCode status, ResponseHeaderMetadata rhm, Exception e)
    {
      OffAmazonPaymentsServiceException serviceException;
      if (responseBody != null && responseBody.StartsWith("<"))
      {
        Match match1 = Regex.Match(responseBody, "<RequestId>(.*)</RequestId>.*<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?", RegexOptions.Multiline);
        Match match2 = Regex.Match(responseBody, "<Error><Code>(.*)</Code><Message>(.*)</Message></Error>.*(<Error>)?.*<RequestID>(.*)</RequestID>", RegexOptions.Multiline);
        if (match1.Success)
        {
          string requestId = match1.Groups[1].Value;
          string errorCode = match1.Groups[2].Value;
          serviceException = new OffAmazonPaymentsServiceException(match1.Groups[3].Value, status, errorCode, "Unknown", requestId, responseBody, rhm);
        }
        else if (match2.Success)
        {
          string errorCode = match2.Groups[1].Value;
          string message = match2.Groups[2].Value;
          string requestId = match2.Groups[4].Value;
          serviceException = new OffAmazonPaymentsServiceException(message, status, errorCode, "Unknown", requestId, responseBody, rhm);
        }
        else
          serviceException = new OffAmazonPaymentsServiceException("Internal Error", status, rhm);
      }
      else
        serviceException = new OffAmazonPaymentsServiceException("Internal Error", status, rhm);
      return serviceException;
    }

    private void PauseOnRetry(int retries, HttpStatusCode status, ResponseHeaderMetadata rhm)
    {
      if (retries > this.config.MaxErrorRetry)
        throw new OffAmazonPaymentsServiceException("Maximum number of retry attempts reached : " + (object) (retries - 1), status, rhm);
      Thread.Sleep((int) Math.Pow(4.0, (double) retries) * 100);
    }

    private void AddRequiredParameters(IDictionary<string, string> parameters)
    {
      parameters.Add("AWSAccessKeyId", this.awsAccessKeyId);
      parameters.Add("Timestamp", this.GetFormattedTimestamp());
      parameters.Add("Version", this.config.ServiceVersion);
      parameters.Add("SignatureVersion", this.config.SignatureVersion);
      parameters.Add("Signature", this.SignParameters(parameters, this.awsSecretAccessKey));
    }

    private string GetParametersAsString(IDictionary<string, string> parameters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string key in (IEnumerable<string>) parameters.Keys)
      {
        string parameter = parameters[key];
        if (parameter != null)
        {
          stringBuilder.Append(key);
          stringBuilder.Append('=');
          stringBuilder.Append(this.UrlEncode(parameter, false));
          stringBuilder.Append('&');
        }
      }
      string str = stringBuilder.ToString();
      return str.Remove(str.Length - 1);
    }

    private string SignParameters(IDictionary<string, string> parameters, string key)
    {
      string parameter = parameters["SignatureVersion"];
      KeyedHashAlgorithm algorithm = (KeyedHashAlgorithm) new HMACSHA1();
      string data;
      if ("0".Equals(parameter))
        data = this.CalculateStringToSignV0(parameters);
      else if ("1".Equals(parameter))
      {
        data = this.CalculateStringToSignV1(parameters);
      }
      else
      {
        if (!"2".Equals(parameter))
          throw new Exception("Invalid Signature Version specified");
        string signatureMethod = this.config.SignatureMethod;
        algorithm = KeyedHashAlgorithm.Create(signatureMethod.ToUpper());
        parameters.Add("SignatureMethod", signatureMethod);
        data = this.CalculateStringToSignV2(parameters);
      }
      return this.Sign(data, key, algorithm);
    }

    private string CalculateStringToSignV0(IDictionary<string, string> parameters)
    {
      return new StringBuilder().Append(parameters["Action"]).Append(parameters["Timestamp"]).ToString();
    }

    private string CalculateStringToSignV1(IDictionary<string, string> parameters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) new SortedDictionary<string, string>(parameters, (IComparer<string>) StringComparer.OrdinalIgnoreCase))
      {
        if (keyValuePair.Value != null)
        {
          stringBuilder.Append(keyValuePair.Key);
          stringBuilder.Append(keyValuePair.Value);
        }
      }
      return stringBuilder.ToString();
    }

    private string CalculateStringToSignV2(IDictionary<string, string> parameters)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new SortedDictionary<string, string>(parameters, (IComparer<string>) StringComparer.Ordinal);
      stringBuilder.Append("POST");
      stringBuilder.Append("\n");
      Uri uri = new Uri(this.config.ServiceURL);
      stringBuilder.Append(uri.Host);
      if (uri.Port != 80 && uri.Port != 443)
      {
        stringBuilder.Append(":");
        stringBuilder.Append(uri.Port);
      }
      stringBuilder.Append("\n");
      string data = uri.AbsolutePath;
      if (string.IsNullOrEmpty(data))
        data = "/";
      stringBuilder.Append(this.UrlEncode(data, true));
      stringBuilder.Append("\n");
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) dictionary)
      {
        if (keyValuePair.Value != null)
        {
          stringBuilder.Append(this.UrlEncode(keyValuePair.Key, false));
          stringBuilder.Append("=");
          stringBuilder.Append(this.UrlEncode(keyValuePair.Value, false));
          stringBuilder.Append("&");
        }
      }
      string str = stringBuilder.ToString();
      return str.Remove(str.Length - 1);
    }

    private string UrlEncode(string data, bool path)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~" + (path ? "/" : "");
      foreach (char ch in Encoding.UTF8.GetBytes(data))
      {
        if (str.IndexOf(ch) != -1)
          stringBuilder.Append(ch);
        else
          stringBuilder.Append("%" + string.Format("{0:X2}", (object) (int) ch));
      }
      return stringBuilder.ToString();
    }

    private string Sign(string data, string key, KeyedHashAlgorithm algorithm)
    {
      Encoding encoding = (Encoding) new UTF8Encoding();
      algorithm.Key = encoding.GetBytes(key);
      return Convert.ToBase64String(algorithm.ComputeHash(encoding.GetBytes(data.ToCharArray())));
    }

    private string GetFormattedTimestamp()
    {
      DateTime now = DateTime.Now;
      DateTime dateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond, DateTimeKind.Local);
      dateTime = dateTime.ToUniversalTime();
      return dateTime.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    private IDictionary<string, string> ConvertCapture(CaptureRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "Capture");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonAuthorizationId())
        dictionary.Add("AmazonAuthorizationId", request.AmazonAuthorizationId ?? "");
      if (request.IsSetCaptureReferenceId())
        dictionary.Add("CaptureReferenceId", request.CaptureReferenceId ?? "");
      if (request.IsSetCaptureAmount())
      {
        Price captureAmount = request.CaptureAmount;
        if (captureAmount.IsSetAmount())
          dictionary.Add("CaptureAmount.Amount", captureAmount.Amount ?? "");
        if (captureAmount.IsSetCurrencyCode())
          dictionary.Add("CaptureAmount.CurrencyCode", captureAmount.CurrencyCode);
      }
      if (request.IsSetSellerCaptureNote())
        dictionary.Add("SellerCaptureNote", request.SellerCaptureNote ?? "");
      if (request.IsSetSoftDescriptor())
        dictionary.Add("SoftDescriptor", request.SoftDescriptor ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      if (request.IsSetProviderCreditList())
      {
        List<ProviderCredit> member = request.ProviderCreditList.member;
        int num = 1;
        foreach (ProviderCredit providerCredit in member)
        {
          if (providerCredit.IsSetProviderId())
            dictionary.Add("ProviderCreditList.member." + (object) num + ".ProviderId", providerCredit.ProviderId ?? "");
          if (providerCredit.IsSetCreditAmount())
          {
            Price creditAmount = providerCredit.CreditAmount;
            if (creditAmount.IsSetAmount())
              dictionary.Add("ProviderCreditList.member." + (object) num + ".CreditAmount.Amount", creditAmount.Amount ?? "");
            if (creditAmount.IsSetCurrencyCode())
              dictionary.Add("ProviderCreditList.member." + (object) num + ".CreditAmount.CurrencyCode", creditAmount.CurrencyCode);
          }
          ++num;
        }
      }
      return dictionary;
    }

    private IDictionary<string, string> ConvertRefund(RefundRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "Refund");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonCaptureId())
        dictionary.Add("AmazonCaptureId", request.AmazonCaptureId ?? "");
      if (request.IsSetRefundReferenceId())
        dictionary.Add("RefundReferenceId", request.RefundReferenceId ?? "");
      if (request.IsSetRefundAmount())
      {
        Price refundAmount = request.RefundAmount;
        if (refundAmount.IsSetAmount())
          dictionary.Add("RefundAmount.Amount", refundAmount.Amount ?? "");
        if (refundAmount.IsSetCurrencyCode())
          dictionary.Add("RefundAmount.CurrencyCode", refundAmount.CurrencyCode);
      }
      if (request.IsSetSellerRefundNote())
        dictionary.Add("SellerRefundNote", request.SellerRefundNote ?? "");
      if (request.IsSetSoftDescriptor())
        dictionary.Add("SoftDescriptor", request.SoftDescriptor ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      if (request.IsSetProviderCreditReversalList())
      {
        List<ProviderCreditReversal> member = request.ProviderCreditReversalList.member;
        int num = 1;
        foreach (ProviderCreditReversal providerCreditReversal in member)
        {
          if (providerCreditReversal.IsSetProviderId())
            dictionary.Add("ProviderCreditReversalList.member." + (object) num + ".ProviderId", providerCreditReversal.ProviderId ?? "");
          if (providerCreditReversal.IsSetCreditReversalAmount())
          {
            Price creditReversalAmount = providerCreditReversal.CreditReversalAmount;
            if (creditReversalAmount.IsSetAmount())
              dictionary.Add("ProviderCreditReversalList.member." + (object) num + ".CreditReversalAmount.Amount", creditReversalAmount.Amount ?? "");
            if (creditReversalAmount.IsSetCurrencyCode())
              dictionary.Add("ProviderCreditReversalList.member." + (object) num + ".CreditReversalAmount.CurrencyCode", creditReversalAmount.CurrencyCode);
          }
          ++num;
        }
      }
      return dictionary;
    }

    private IDictionary<string, string> ConvertCloseAuthorization(CloseAuthorizationRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "CloseAuthorization");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonAuthorizationId())
        dictionary.Add("AmazonAuthorizationId", request.AmazonAuthorizationId ?? "");
      if (request.IsSetClosureReason())
        dictionary.Add("ClosureReason", request.ClosureReason ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertGetRefundDetails(GetRefundDetailsRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "GetRefundDetails");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonRefundId())
        dictionary.Add("AmazonRefundId", request.AmazonRefundId ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertGetCaptureDetails(GetCaptureDetailsRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "GetCaptureDetails");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonCaptureId())
        dictionary.Add("AmazonCaptureId", request.AmazonCaptureId ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertCloseOrderReference(CloseOrderReferenceRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "CloseOrderReference");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonOrderReferenceId())
        dictionary.Add("AmazonOrderReferenceId", request.AmazonOrderReferenceId);
      if (request.IsSetClosureReason())
        dictionary.Add("ClosureReason", request.ClosureReason ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertConfirmOrderReference(ConfirmOrderReferenceRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "ConfirmOrderReference");
      if (request.IsSetAmazonOrderReferenceId())
        dictionary.Add("AmazonOrderReferenceId", request.AmazonOrderReferenceId);
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertGetOrderReferenceDetails(GetOrderReferenceDetailsRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "GetOrderReferenceDetails");
      if (request.IsSetAmazonOrderReferenceId())
        dictionary.Add("AmazonOrderReferenceId", request.AmazonOrderReferenceId);
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAddressConsentToken())
        dictionary.Add("AddressConsentToken", request.AddressConsentToken);
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertAuthorize(AuthorizeRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "Authorize");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonOrderReferenceId())
        dictionary.Add("AmazonOrderReferenceId", request.AmazonOrderReferenceId);
      if (request.IsSetAuthorizationReferenceId())
        dictionary.Add("AuthorizationReferenceId", request.AuthorizationReferenceId ?? "");
      if (request.IsSetAuthorizationAmount())
      {
        Price authorizationAmount = request.AuthorizationAmount;
        if (authorizationAmount.IsSetAmount())
          dictionary.Add("AuthorizationAmount.Amount", authorizationAmount.Amount ?? "");
        if (authorizationAmount.IsSetCurrencyCode())
          dictionary.Add("AuthorizationAmount.CurrencyCode", authorizationAmount.CurrencyCode);
      }
      if (request.IsSetSellerAuthorizationNote())
        dictionary.Add("SellerAuthorizationNote", request.SellerAuthorizationNote ?? "");
      if (request.IsSetOrderItemCategories())
      {
        List<string> orderItemCategory = request.OrderItemCategories.OrderItemCategory;
        int num = 1;
        foreach (string str in orderItemCategory)
        {
          dictionary.Add("OrderItemCategories.OrderItemCategory." + (object) num, str);
          ++num;
        }
      }
      if (request.IsSetTransactionTimeout())
        dictionary.Add("TransactionTimeout", string.Concat((object) request.TransactionTimeout));
      if (request.IsSetCaptureNow())
        dictionary.Add("CaptureNow", OffAmazonPaymentsServiceClient.ConvertBooleanToString(request.CaptureNow) ?? "");
      if (request.IsSetSoftDescriptor())
        dictionary.Add("SoftDescriptor", request.SoftDescriptor ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      if (request.IsSetProviderCreditList())
      {
        List<ProviderCredit> member = request.ProviderCreditList.member;
        int num = 1;
        foreach (ProviderCredit providerCredit in member)
        {
          if (providerCredit.IsSetProviderId())
            dictionary.Add("ProviderCreditList.member." + (object) num + ".ProviderId", providerCredit.ProviderId ?? "");
          if (providerCredit.IsSetCreditAmount())
          {
            Price creditAmount = providerCredit.CreditAmount;
            if (creditAmount.IsSetAmount())
              dictionary.Add("ProviderCreditList.member." + (object) num + ".CreditAmount.Amount", creditAmount.Amount ?? "");
            if (creditAmount.IsSetCurrencyCode())
              dictionary.Add("ProviderCreditList.member." + (object) num + ".CreditAmount.CurrencyCode", creditAmount.CurrencyCode);
          }
          ++num;
        }
      }
      return dictionary;
    }

    public static string ConvertBooleanToString(bool input)
    {
      return input.ToString().ToLower();
    }

    private IDictionary<string, string> ConvertSetOrderReferenceDetails(SetOrderReferenceDetailsRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "SetOrderReferenceDetails");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonOrderReferenceId())
        dictionary.Add("AmazonOrderReferenceId", request.AmazonOrderReferenceId);
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      if (request.IsSetOrderReferenceAttributes())
      {
        OrderReferenceAttributes referenceAttributes = request.OrderReferenceAttributes;
        if (referenceAttributes.IsSetOrderTotal())
        {
          OrderTotal orderTotal = referenceAttributes.OrderTotal;
          if (orderTotal.IsSetCurrencyCode())
            dictionary.Add("OrderReferenceAttributes.OrderTotal.CurrencyCode", orderTotal.CurrencyCode);
          if (orderTotal.IsSetAmount())
            dictionary.Add("OrderReferenceAttributes.OrderTotal.Amount", orderTotal.Amount);
        }
        if (referenceAttributes.IsSetPlatformId())
          dictionary.Add("OrderReferenceAttributes.PlatformId", referenceAttributes.PlatformId);
        if (referenceAttributes.IsSetSellerNote())
          dictionary.Add("OrderReferenceAttributes.SellerNote", referenceAttributes.SellerNote ?? "");
        if (referenceAttributes.IsSetSellerOrderAttributes())
        {
          SellerOrderAttributes sellerOrderAttributes = referenceAttributes.SellerOrderAttributes;
          if (sellerOrderAttributes.IsSetSellerOrderId())
            dictionary.Add("OrderReferenceAttributes.SellerOrderAttributes.SellerOrderId", sellerOrderAttributes.SellerOrderId);
          if (sellerOrderAttributes.IsSetStoreName())
            dictionary.Add("OrderReferenceAttributes.SellerOrderAttributes.StoreName", sellerOrderAttributes.StoreName);
          if (sellerOrderAttributes.IsSetOrderItemCategories())
          {
            List<string> orderItemCategory = sellerOrderAttributes.OrderItemCategories.OrderItemCategory;
            int num = 1;
            foreach (string str in orderItemCategory)
            {
              dictionary.Add("OrderReferenceAttributes.SellerOrderAttributes.OrderItemCategories.OrderItemCategory." + (object) num, str);
              ++num;
            }
          }
          if (sellerOrderAttributes.IsSetCustomInformation())
            dictionary.Add("OrderReferenceAttributes.SellerOrderAttributes.CustomInformation", sellerOrderAttributes.CustomInformation);
        }
      }
      return dictionary;
    }

    private IDictionary<string, string> ConvertGetAuthorizationDetails(GetAuthorizationDetailsRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "GetAuthorizationDetails");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonAuthorizationId())
        dictionary.Add("AmazonAuthorizationId", request.AmazonAuthorizationId ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertCancelOrderReference(CancelOrderReferenceRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "CancelOrderReference");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonOrderReferenceId())
        dictionary.Add("AmazonOrderReferenceId", request.AmazonOrderReferenceId);
      if (request.IsSetCancelationReason())
        dictionary.Add("CancelationReason", request.CancelationReason ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertCreateOrderReferenceForId(CreateOrderReferenceForIdRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "CreateOrderReferenceForId");
      if (request.IsSetId())
        dictionary.Add("Id", request.Id);
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId);
      if (request.IsSetIdType())
        dictionary.Add("IdType", request.IdType);
      if (request.IsSetInheritShippingAddress())
        dictionary.Add("InheritShippingAddress", string.Concat((object) request.InheritShippingAddress));
      if (request.IsSetConfirmNow())
        dictionary.Add("ConfirmNow", OffAmazonPaymentsServiceClient.ConvertBooleanToString(request.ConfirmNow) ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      if (request.IsSetOrderReferenceAttributes())
      {
        OrderReferenceAttributes referenceAttributes = request.OrderReferenceAttributes;
        if (referenceAttributes.IsSetOrderTotal())
        {
          OrderTotal orderTotal = referenceAttributes.OrderTotal;
          if (orderTotal.IsSetCurrencyCode())
            dictionary.Add("OrderReferenceAttributes.OrderTotal.CurrencyCode", orderTotal.CurrencyCode);
          if (orderTotal.IsSetAmount())
            dictionary.Add("OrderReferenceAttributes.OrderTotal.Amount", orderTotal.Amount);
        }
        if (referenceAttributes.IsSetPlatformId())
          dictionary.Add("OrderReferenceAttributes.PlatformId", referenceAttributes.PlatformId);
        if (referenceAttributes.IsSetSellerNote())
          dictionary.Add("OrderReferenceAttributes.SellerNote", referenceAttributes.SellerNote);
        if (referenceAttributes.IsSetSellerOrderAttributes())
        {
          SellerOrderAttributes sellerOrderAttributes = referenceAttributes.SellerOrderAttributes;
          if (sellerOrderAttributes.IsSetSellerOrderId())
            dictionary.Add("OrderReferenceAttributes.SellerOrderAttributes.SellerOrderId", sellerOrderAttributes.SellerOrderId);
          if (sellerOrderAttributes.IsSetStoreName())
            dictionary.Add("OrderReferenceAttributes.SellerOrderAttributes.StoreName", sellerOrderAttributes.StoreName);
          if (sellerOrderAttributes.IsSetOrderItemCategories())
          {
            List<string> orderItemCategory = sellerOrderAttributes.OrderItemCategories.OrderItemCategory;
            int num = 1;
            foreach (string str in orderItemCategory)
            {
              dictionary.Add("OrderReferenceAttributes.SellerOrderAttributes.OrderItemCategories.OrderItemCategory." + (object) num, str);
              ++num;
            }
          }
          if (sellerOrderAttributes.IsSetCustomInformation())
            dictionary.Add("OrderReferenceAttributes.SellerOrderAttributes.CustomInformation", sellerOrderAttributes.CustomInformation);
        }
      }
      return dictionary;
    }

    private IDictionary<string, string> ConvertGetBillingAgreementDetails(GetBillingAgreementDetailsRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "GetBillingAgreementDetails");
      if (request.IsSetAmazonBillingAgreementId())
        dictionary.Add("AmazonBillingAgreementId", request.AmazonBillingAgreementId);
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId);
      if (request.IsSetAddressConsentToken())
        dictionary.Add("AddressConsentToken", request.AddressConsentToken);
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertSetBillingAgreementDetails(SetBillingAgreementDetailsRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "SetBillingAgreementDetails");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId);
      if (request.IsSetAmazonBillingAgreementId())
        dictionary.Add("AmazonBillingAgreementId", request.AmazonBillingAgreementId);
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      if (request.IsSetBillingAgreementAttributes())
      {
        BillingAgreementAttributes agreementAttributes1 = request.BillingAgreementAttributes;
        if (agreementAttributes1.IsSetPlatformId())
          dictionary.Add("BillingAgreementAttributes.PlatformId", agreementAttributes1.PlatformId);
        if (agreementAttributes1.IsSetSellerNote())
          dictionary.Add("BillingAgreementAttributes.SellerNote", agreementAttributes1.SellerNote);
        if (agreementAttributes1.IsSetSellerBillingAgreementAttributes())
        {
          SellerBillingAgreementAttributes agreementAttributes2 = agreementAttributes1.SellerBillingAgreementAttributes;
          if (agreementAttributes2.IsSetSellerBillingAgreementId())
            dictionary.Add("BillingAgreementAttributes.SellerBillingAgreementAttributes.SellerBillingAgreementId", agreementAttributes2.SellerBillingAgreementId);
          if (agreementAttributes2.IsSetStoreName())
            dictionary.Add("BillingAgreementAttributes.SellerBillingAgreementAttributes.StoreName", agreementAttributes2.StoreName);
          if (agreementAttributes2.IsSetCustomInformation())
            dictionary.Add("BillingAgreementAttributes.SellerBillingAgreementAttributes.CustomInformation", agreementAttributes2.CustomInformation);
        }
      }
      return dictionary;
    }

    private IDictionary<string, string> ConvertConfirmBillingAgreement(ConfirmBillingAgreementRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "ConfirmBillingAgreement");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId);
      if (request.IsSetAmazonBillingAgreementId())
        dictionary.Add("AmazonBillingAgreementId", request.AmazonBillingAgreementId);
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertValidateBillingAgreement(ValidateBillingAgreementRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "ValidateBillingAgreement");
      if (request.IsSetAmazonBillingAgreementId())
        dictionary.Add("AmazonBillingAgreementId", request.AmazonBillingAgreementId);
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId);
      return dictionary;
    }

    private IDictionary<string, string> ConvertAuthorizeOnBillingAgreement(AuthorizeOnBillingAgreementRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "AuthorizeOnBillingAgreement");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId);
      if (request.IsSetAmazonBillingAgreementId())
        dictionary.Add("AmazonBillingAgreementId", request.AmazonBillingAgreementId);
      if (request.IsSetAuthorizationReferenceId())
        dictionary.Add("AuthorizationReferenceId", request.AuthorizationReferenceId);
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      if (request.IsSetAuthorizationAmount())
      {
        Price authorizationAmount = request.AuthorizationAmount;
        if (authorizationAmount.IsSetAmount())
          dictionary.Add("AuthorizationAmount.Amount", authorizationAmount.Amount);
        if (authorizationAmount.IsSetCurrencyCode())
          dictionary.Add("AuthorizationAmount.CurrencyCode", authorizationAmount.CurrencyCode);
      }
      if (request.IsSetSellerAuthorizationNote())
        dictionary.Add("SellerAuthorizationNote", request.SellerAuthorizationNote);
      if (request.IsSetTransactionTimeout())
        dictionary.Add("TransactionTimeout", string.Concat((object) request.TransactionTimeout));
      if (request.IsSetCaptureNow())
        dictionary.Add("CaptureNow", OffAmazonPaymentsServiceClient.ConvertBooleanToString(request.CaptureNow) ?? "");
      if (request.IsSetSoftDescriptor())
        dictionary.Add("SoftDescriptor", request.SoftDescriptor);
      if (request.IsSetSellerNote())
        dictionary.Add("SellerNote", request.SellerNote);
      if (request.IsSetPlatformId())
        dictionary.Add("PlatformId", request.PlatformId);
      if (request.IsSetSellerOrderAttributes())
      {
        SellerOrderAttributes sellerOrderAttributes = request.SellerOrderAttributes;
        if (sellerOrderAttributes.IsSetSellerOrderId())
          dictionary.Add("SellerOrderAttributes.SellerOrderId", sellerOrderAttributes.SellerOrderId);
        if (sellerOrderAttributes.IsSetStoreName())
          dictionary.Add("SellerOrderAttributes.StoreName", sellerOrderAttributes.StoreName);
        if (sellerOrderAttributes.IsSetOrderItemCategories())
        {
          List<string> orderItemCategory = sellerOrderAttributes.OrderItemCategories.OrderItemCategory;
          int num = 1;
          foreach (string str in orderItemCategory)
          {
            dictionary.Add("SellerOrderAttributes.OrderItemCategories.OrderItemCategory." + (object) num, str);
            ++num;
          }
        }
        if (sellerOrderAttributes.IsSetCustomInformation())
          dictionary.Add("SellerOrderAttributes.CustomInformation", sellerOrderAttributes.CustomInformation);
      }
      if (request.IsSetInheritShippingAddress())
        dictionary.Add("InheritShippingAddress", string.Concat((object) request.InheritShippingAddress));
      return dictionary;
    }

    private IDictionary<string, string> ConvertCloseBillingAgreement(CloseBillingAgreementRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "CloseBillingAgreement");
      if (request.IsSetAmazonBillingAgreementId())
        dictionary.Add("AmazonBillingAgreementId", request.AmazonBillingAgreementId);
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId);
      if (request.IsSetClosureReason())
        dictionary.Add("ClosureReason", request.ClosureReason);
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertGetProviderCreditDetails(GetProviderCreditDetailsRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "GetProviderCreditDetails");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonProviderCreditId())
        dictionary.Add("AmazonProviderCreditId", request.AmazonProviderCreditId ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertReverseProviderCredit(ReverseProviderCreditRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "ReverseProviderCredit");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonProviderCreditId())
        dictionary.Add("AmazonProviderCreditId", request.AmazonProviderCreditId ?? "");
      if (request.IsSetCreditReversalReferenceId())
        dictionary.Add("CreditReversalReferenceId", request.CreditReversalReferenceId ?? "");
      if (request.IsSetCreditReversalAmount())
      {
        Price creditReversalAmount = request.CreditReversalAmount;
        if (creditReversalAmount.IsSetAmount())
          dictionary.Add("CreditReversalAmount.Amount", creditReversalAmount.Amount ?? "");
        if (creditReversalAmount.IsSetCurrencyCode())
          dictionary.Add("CreditReversalAmount.CurrencyCode", creditReversalAmount.CurrencyCode);
      }
      if (request.IsSetCreditReversalNote())
        dictionary.Add("CreditReversalNote", request.CreditReversalNote);
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }

    private IDictionary<string, string> ConvertGetProviderCreditReversalDetails(GetProviderCreditReversalDetailsRequest request)
    {
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      dictionary.Add("Action", "GetProviderCreditReversalDetails");
      if (request.IsSetSellerId())
        dictionary.Add("SellerId", request.SellerId ?? "");
      if (request.IsSetAmazonProviderCreditReversalId())
        dictionary.Add("AmazonProviderCreditReversalId", request.AmazonProviderCreditReversalId ?? "");
      if (request.IsSetMWSAuthToken())
        dictionary.Add("MWSAuthToken", request.MWSAuthToken ?? "");
      return dictionary;
    }
  }
}
